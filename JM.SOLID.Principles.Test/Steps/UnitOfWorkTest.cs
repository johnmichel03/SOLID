using Microsoft.VisualStudio.TestTools.UnitTesting;
using JM.SOLID.Principles.Config;
using JM.SOLID.Principles.Data;
using Moq;

namespace JM.SOLID.Principles.Tests
{
    /// <summary>
    /// Summary description for UnitOfWorkTest
    /// </summary>
    [TestClass]
    public class UnitOfWorkTest
    {
        private static Mock<IConfigSettings> settings;

        [ClassInitialize()]
        public static void MyUnitOfWorkTest(TestContext testContext)
        {
            settings = new Mock<IConfigSettings>();
        }

        [TestMethod]
        public void ShouldReturnBackupDataStoreRepository()
        {
            //Arrange
            settings.Setup(s => s.StoreType).Returns("BACKUP");
            //Act
            IUnitOfWork uow = new UnitOfWork(settings.Object);
            //Assert
            Assert.IsNotNull(uow.AccountDataStore as BackupAccountDataStore);
        }

        [TestMethod]
        public void ShouldReturnAccountDataStoreRepository()
        {
            //Arrange
            settings.Setup(s => s.StoreType).Returns("");
            //Act
            IUnitOfWork uow = new UnitOfWork(settings.Object);
            //Assert
            Assert.IsNotNull(uow.AccountDataStore as AccountDataStore);
        }

        [TestMethod]
        public void ShouldReturnAccountDataStoreRepositoryForAnyThingOtherThanBACKUP()
        {
            //Arrange
            settings.Setup(s => s.StoreType).Returns("anything");
            //Act
            IUnitOfWork uow = new UnitOfWork(settings.Object);
            //Assert
            Assert.IsNotNull(uow.AccountDataStore as AccountDataStore);
        }
    }
}
