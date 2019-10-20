using GameOfLife;
using GameOfLife.BoardGenerationStrategies;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameOfLifeTests
{
    [TestFixture]
    public class ApplicationTests
    {
        private Mock<IConsole> _fakeConsole;
        private Mock<IFile> _fakeFileWrapper;
        private Mock<IIntroScreenPrinter> _fakeIntroScreenPrinter;
        private Mock<IFileNamesProvider> _fakeFileNamesProvider;
        private Mock<IOptionKeyReader> _fakeOptionKeyReader;
        private Mock<IBoardEvolver> _fakeBoardEvolver;
        private Mock<IBoardGenerator> _fakeBoardGenerator;
        private Mock<IBoardPrinter> _fakeBoardPrinter;
        private Application _application;
        
        [SetUp]
        public void SetUp()
        {
            _fakeConsole = new Mock<IConsole>();
            _fakeFileWrapper = new Mock<IFile>();
            _fakeIntroScreenPrinter = new Mock<IIntroScreenPrinter>();
            _fakeFileNamesProvider = new Mock<IFileNamesProvider>();
            _fakeOptionKeyReader = new Mock<IOptionKeyReader>();
            _fakeBoardEvolver = new Mock<IBoardEvolver>();
            _fakeBoardGenerator = new Mock<IBoardGenerator>();
            _fakeBoardPrinter = new Mock<IBoardPrinter>();

            _fakeFileNamesProvider
                .Setup(_ => _.GetFileNamesForPatternFiles()).Returns(new[] { "a.txt", "b.txt" });

            _application = new Application(
                _fakeConsole.Object,
                _fakeFileWrapper.Object,
                _fakeIntroScreenPrinter.Object,
                _fakeFileNamesProvider.Object,
                _fakeOptionKeyReader.Object,
                _fakeBoardEvolver.Object,
                _fakeBoardGenerator.Object,
                _fakeBoardPrinter.Object
            );
        }

        [Test]
        public void Run_OptionTypeExit_PrintNewGameScreenCalledOnly()
        {
            //Arrange
            _fakeOptionKeyReader.SetupSequence(_ => _.GetOptionFromKeyPress(It.IsAny<int>()))
                .Returns(new Option {OptionType = OptionType.Exit});

            //Act
            _application.Run();

            //Assert                        
            _fakeIntroScreenPrinter.Verify(_ => _.PrintNewGameScreen(It.IsAny<IReadOnlyList<string>>()), Times.Once);
            _fakeBoardGenerator.Verify(_ => _.GenerateBoard(It.IsAny<IBoardGenerationStrategy>()), Times.Never);
            _fakeBoardEvolver.Verify(_ => _.EvolveBoard(It.IsAny<IBoard>()), Times.Never);
            _fakeBoardPrinter.Verify(_ => _.PrintBoard(It.IsAny<IBoard>()), Times.Never);
        }

        [Test]
        [TestCase(OptionType.Random, typeof(BoardGenerationStrategyRandom))]
        [TestCase(OptionType.FromFile, typeof(BoardGenerationStrategyFromFile))]
        public void Run_SelectOptionThenEscapeThenExit_GenerateBoardUsingCorrectStrategyPrintEvolvePrint(OptionType optionType, Type strategyType)
        {
            //Arrange
            _fakeConsole.SetupSequence(_ => _.KeyAvailable)
                .Returns(false)
                .Returns(true);

            _fakeConsole.Setup(_ => _.ReadKey(It.IsAny<bool>())).Returns(ConsoleKey.Escape);
            
            _fakeOptionKeyReader.SetupSequence(_ => _.GetOptionFromKeyPress(It.IsAny<int>()))
                .Returns(new Option { OptionType = optionType })
                .Returns(new Option { OptionType = OptionType.Exit });

            //Act
            _application.Run();

            //Assert                        
            _fakeIntroScreenPrinter.Verify(_ => _.PrintNewGameScreen(It.IsAny<IReadOnlyList<string>>()), Times.Exactly(2));
            _fakeBoardGenerator.Verify(_ => _.GenerateBoard(It.Is<IBoardGenerationStrategy>(s => s.GetType() == strategyType)), Times.Once);
            _fakeBoardPrinter.Verify(_ => _.PrintBoard(It.IsAny<IBoard>()), Times.Exactly(2));
            _fakeBoardEvolver.Verify(_ => _.EvolveBoard(It.IsAny<IBoard>()), Times.Once);
            
        }

        [Test]
        public void Run_OptionTypeUndefined_ThrowsNotSupportedException()
        {
            _fakeOptionKeyReader.Setup(_ => _.GetOptionFromKeyPress(It.IsAny<int>())).Returns(new Option());

            Assert.Throws<NotSupportedException>(() => _application.Run());
        }

    }
}

