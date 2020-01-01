using GameOfLife;
using GameOfLife.BoardArrayGeneration;
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
        private Mock<IFileNamesProvider> _fakeFileNamesProvider;
        private Mock<IOptionKeyReader> _fakeOptionKeyReader;
        private Application _application;
        private Mock<IConsolePrinter> _fakeConsolePrinter;
        private Mock<IBoardFactory> _fakeBoardFactory;
        private Mock<IBoard> _fakeBoard;
        private Mock<IFile> _fakeFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeConsole = new Mock<IConsole>();
            _fakeConsolePrinter = new Mock<IConsolePrinter>();
            _fakeFileNamesProvider = new Mock<IFileNamesProvider>();
            _fakeOptionKeyReader = new Mock<IOptionKeyReader>();
            _fakeBoardFactory = new Mock<IBoardFactory>();
            _fakeFileWrapper = new Mock<IFile>();
            _fakeBoard = new Mock<IBoard>();

            _fakeFileNamesProvider
                .Setup(_ => _.GetFileNamesForPatternFiles()).Returns(new[] { "a.txt", "b.txt" });

            _application = new Application(
                _fakeConsole.Object,
                _fakeConsolePrinter.Object,
                _fakeFileNamesProvider.Object,
                _fakeOptionKeyReader.Object,
                _fakeBoardFactory.Object,
                _fakeFileWrapper.Object
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
            _fakeConsolePrinter.Verify(_ => _.PrintNewGameScreen(It.IsAny<IReadOnlyList<string>>()), Times.Once);
            _fakeConsolePrinter.VerifyNoOtherCalls();
            _fakeBoardFactory.Verify(_ => _.CreateBoard(It.IsAny<IBoardArrayStrategy>()), Times.Never);
            _fakeConsolePrinter.Verify(_ => _.PrintBoard(It.IsAny<Board>()), Times.Never);
        }

        [Test]
        [TestCase(OptionType.Random, typeof(BoardArrayStrategyRandom))]
        [TestCase(OptionType.FromFile, typeof(BoardArrayStrategyFromFile))]
        public void Run_SelectOptionThenEscapeThenExit_GenerateBoardUsingCorrectStrategyPrintEvolvePrint(
            OptionType optionType, Type strategyType)
        {
            //Arrange
            _fakeConsole.SetupSequence(_ => _.KeyAvailable)
                .Returns(false)
                .Returns(true);

            _fakeConsole.Setup(_ => _.ReadKey(It.IsAny<bool>())).Returns(ConsoleKey.Escape);

            _fakeOptionKeyReader.SetupSequence(_ => _.GetOptionFromKeyPress(It.IsAny<int>()))
                .Returns(new Option { OptionType = optionType })
                .Returns(new Option { OptionType = OptionType.Exit });

            _fakeBoardFactory.Setup(_ => _.CreateBoard(It.IsAny<IBoardArrayStrategy>())).Returns(_fakeBoard.Object);

            //Act
            _application.Run();

            //Assert                        
            _fakeConsolePrinter.Verify(_ => _.PrintNewGameScreen(It.IsAny<IReadOnlyList<string>>()), Times.Exactly(2));
            _fakeBoardFactory.Verify(_ => _.CreateBoard(It.Is<IBoardArrayStrategy>(s => s.GetType() == strategyType)), Times.Once);
            _fakeBoard.Verify(_ => _.Evolve(), Times.Once);
            _fakeConsolePrinter.Verify(_ => _.PrintBoard(_fakeBoard.Object), Times.Exactly(2));
            _fakeConsolePrinter.VerifyNoOtherCalls();
        }

        [Test]
        public void Run_OptionTypeUndefined_ThrowsNotSupportedException()
        {
            //Arrange
            _fakeOptionKeyReader.Setup(_ => _.GetOptionFromKeyPress(It.IsAny<int>())).Returns(new Option());

            //Act & Assert
            Assert.Throws<NotSupportedException>(() => _application.Run());
        }
    }
}

