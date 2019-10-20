using GameOfLife;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;
using System;

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
            _fakeConsole.Setup(_ => _.GetConsoleKey(It.IsAny<ConsoleKeyInfo>())).Returns(ConsoleKey.Escape);

            var result = _optionKeyReader.GetOptionFromKeyPress(It.IsAny<int>());

            Assert.That(result.OptionType.Equals(OptionType.Exit));
        }

        [Test]
        [TestCase(0, "1")]
        [TestCase(1, "2")]
        [TestCase(2, "3")]
        public void GetOptionFromKeyPress_RandomOptionChosen_ReturnsOptionWithOptionTypeRandom(
            int fileNamesCount, 
            string inputToString)
        {
            _fakeConsole.Setup(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>())).Returns(inputToString);

            var result = _optionKeyReader.GetOptionFromKeyPress(fileNamesCount);

            Assert.That(result.OptionType.Equals(OptionType.Random));
        }

        [Test]
        [TestCase(2, "1")]
        [TestCase(2, "2")]
        [TestCase(4, "1")]
        [TestCase(4, "2")]
        [TestCase(4, "3")]
        [TestCase(4, "4")]
        public void GetOptionFromKeyPress_FromFileOptionChosen_ReturnsOptionWithOptionTypeFromFile(
            int fileNamesCount,
            string inputToString)
        {
            _fakeConsole.Setup(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>())).Returns(inputToString);

            var result = _optionKeyReader.GetOptionFromKeyPress(fileNamesCount);

            Assert.That(result.OptionType.Equals(OptionType.FromFile));
        }

        [Test]
        [TestCase(0, "2", "1", OptionType.Random )]
        [TestCase(2, "4", "2", OptionType.FromFile)]
        [TestCase(0, "0", "1", OptionType.Random)]
        public void GetOptionFromKeyPress_2IncorrectInputsUntilCorrect_WritesErrorMessage2TimesThenReturnsOptionWithCorrectOptionType(
            int fileNamesCount, 
            string secondInputToString, 
            string thirdInputToString,
            OptionType optionType)
        {
            _fakeConsole.SetupSequence(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>()))
                .Returns("A")
                .Returns(secondInputToString)
                .Returns(thirdInputToString);

            var result = _optionKeyReader.GetOptionFromKeyPress(fileNamesCount);

            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(c => c.ToString() == "Wrong input. Try again.")), Times.Exactly(2));
            Assert.That(result.OptionType.Equals(optionType));
        }
    }
}
