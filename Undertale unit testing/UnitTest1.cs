using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Undertale_unit_testing
{
    [TestClass]
    public class HealthControllerTest
    {
        private HealthController hc;

        [TestInitialize]
        public void Setup()
        {
            //Arrange
            hc = new HealthController(100, 0.5f);
        }

        [TestMethod]
        public void TakeDamage()
        {
            //Act
            hc.TakeDamage(20);

            //Assert
            Assert.AreEqual(80, hc.health);
        }

        [TestMethod]
        public void ClampedHealth()
        {
            //Act
            hc.TakeDamage(200);

            //Assert
            Assert.AreEqual(0, hc.health);
        }

        [TestMethod]
        public void InvicibilityWorks()
        {
            //Act
            hc.TakeDamage(1);

            //Assert
            Assert.IsTrue(hc.isInvencible);
        }

        [TestMethod]
        public void InvincibilityPreventsDamage()
        {
            //Act
            hc.TakeDamage(10);
            hc.TakeDamage(40);

            //Assert
            Assert.AreEqual(90, hc.health);
        }

        [TestMethod]
        public void RecoverWorks()
        {
            //Act
            hc.TakeDamage(40);
            hc.Recover(20);

            //Assert
            Assert.AreEqual(80, hc.health);
        }

        [TestMethod]
        public void RecoverClampsAtMaxHealth()
        {
            //Act
            hc.Recover(2000);

            //Assert
            Assert.AreEqual(100, hc.health);
        }

        [TestMethod]  
        public void ResetWorks()
        {
            //Act
            hc.TakeDamage(40);
            hc.Reset();

            //Assert
            Assert.AreEqual(100, hc.health);
        }
    }
}
