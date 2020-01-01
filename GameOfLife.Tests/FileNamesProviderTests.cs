using GameOfLife;
using GameOfLife.IO;
using GameOfLife.IO.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class FileNamesProviderTests
    {
        private Mock<IDirectory> _fakeDirectoryWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeDirectoryWrapper = new Mock<IDirectory>();

            _fakeDirectoryWrapper.Setup(_ => _.GetFileNames(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new[] { "a.txt", "b.txt" });
        }

        [Test]
        public void GetFileNamesForPatternFiles_ReturnExpectedFileNames()
        {
            //Arrange
            var fileNamesProvider = new FileNamesProvider(_fakeDirectoryWrapper.Object);
            
            //Act
            var result = fileNamesProvider.GetFileNamesForPatternFiles();

            //Assert
            Assert.AreEqual(new[] { "a.txt", "b.txt" }, result);
        }
    }
}
