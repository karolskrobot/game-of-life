using GameOfLife;
using Moq;
using NUnit.Framework;
using System;
using GameOfLife.IO;
using GameOfLife.IO.Wrappers;

namespace GameOfLifeTests
{
    [TestFixture]
    public class OptionKeyReaderTests
    {
        private Mock<IConsole> _fakeConsole;
        private OptionKeyReader _optionKeyReader;

        [SetUp]
        public void SetUp()
        {
            _fakeConsole = new Mock<IConsole>();

            _optionKeyReader = new OptionKeyReader(_fakeConsole.Object);
        }

        [Test]
        public void GetOptionFromKeyPress_ExitCharacterPressed_ReturnsOptionWithOptionTypeExit()
        {
            //Arrange
            _fakeConsole.Setup(_ => _.GetConsoleKey(It.IsAny<ConsoleKeyInfo>())).Returns(ConsoleKey.Escape);

            //Act
            var result = _optionKeyReader.GetOptionFromKeyPress(It.IsAny<int>());

            //Assert
            Assert.That(result.OptionType.Equals(OptionType.Exit));
        }

        [Test]
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "3")]
        public void GetOptionFromKeyPress_RandomOptionChosen_ReturnsOptionWithOptionTypeRandom(
            int randomOptionPosition, 
            string inputToString)
        {
            //Arrange
            _fakeConsole.Setup(_ => _.GetConsoleKeyToString(It.IsAny<ConsoleKeyInfo>())).Returns(inputToString);

            //Act
            var result = _optionKeyReader.GetOptionFromKeyPress(randomOptionPosition);

            //Assert
            Assert.That(result.OptionType.Equals(OptionType.Random));
        }

        [Test]
        [TestCase(3, "1")]
        [TestCase(4, "2")]
        [TestCase(5, "1")]
        [TestCase(5, "2")]
        [TestCase(5, "3")]
        [TestCase(5, "4")]
        public void GetOptionFromKeyPress_FromFileOptionChosen_ReturnsOptionWithOptionTypeFromFile(
            int randomOptionPosition,
            string inputToString)
        {
            //Arrange
            _fakeConsole.Setup(_ => _.GetConsoleKeyToString(It.IsAny<ConsoleKeyInfo>())).Returns(inputToString);

            //Act
            var result = _optionKeyReader.GetOptionFromKeyPress(randomOptionPosition);

            //Assert
            Assert.That(result.OptionType.Equals(OptionType.FromFile));
        }

        [Test]
        [TestCase(1, "2", "1", OptionType.Random )]
        [TestCase(3, "4", "2", OptionType.FromFile)]
        [TestCase(1, "0", "1", OptionType.Random)]
        public void GetOptionFromKeyPress_2IncorrectInputsUntilCorrect_WritesErrorMessage2TimesThenReturnsOptionWithCorrectOptionType(
            int randomOptionPosition, 
            string secondInputToString,
            string thirdInputToString,
            OptionType optionType)
        {
            //Arrange
            _fakeConsole.SetupSequence(_ => _.GetConsoleKeyToString(It.IsAny<ConsoleKeyInfo>()))
                .Returns("A")
                .Returns(secondInputToString)
                .Returns(thirdInputToString);

            //Act
            var result = _optionKeyReader.GetOptionFromKeyPress(randomOptionPosition);

            //Assert
            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(output => output.ToString() == "Wrong input. Try again.")), Times.Exactly(2));
            Assert.That(result.OptionType.Equals(optionType));
        }
    }
}
