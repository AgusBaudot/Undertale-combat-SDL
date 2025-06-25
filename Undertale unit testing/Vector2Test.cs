using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyGame;

namespace Undertale_unit_testing
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void Vector2Creation()
        {
            //Arrange
            Vector2 a = new Vector2(2, 3);
            Vector2 b = new Vector2(a.y, 0);

            //Assert
            Assert.AreEqual(2, a.x);
            Assert.AreEqual(3, a.y);
            Assert.AreEqual(b.x, a.y);
            Assert.AreEqual(0, b.y);
        }

        [TestMethod]
        public void Vector2Addition()
        {
            //Arrange
            Vector2 a = new Vector2(3, 4);
            Vector2 b = new Vector2(1, 2);

            //Act
            Vector2 result = a + b;

            //Assert
            Assert.AreEqual(4, result.x);
            Assert.AreEqual(6, result.y);
        }

        [TestMethod]
        public void Vector2Magnitude()
        {
            //Arrange
            Vector2 a = new Vector2(3, 4);
            
            //Act
            float magnitude = a.magnitude;

            //Assert
            Assert.AreEqual(5f, magnitude, 0.001f); //3, 4, 5 triangle.
        }

        [TestMethod]
        public void Vector2Normalize()
        {
            //Arrange
            Vector2 a = new Vector2(10, 0);
            Vector2 b = new Vector2(3, 4);

            //Act
            Vector2 normalized = a.normalized;
            Vector2 normalized2 = b.normalized;

            //Assert
            Assert.AreEqual(1f, normalized.x, 0.0001f);
            Assert.AreEqual(0f, normalized.y, 0.0001f);
            Assert.AreEqual(new Vector2(3f /5f, 4f / 5f), normalized2);
        }

        [TestMethod]
        public void StaticVectors()
        {
            //Assert
            Assert.AreEqual(new Vector2(0, -1), Vector2.up);
            Assert.AreEqual(new Vector2(0, 1), Vector2.down);
            Assert.AreEqual(new Vector2(1, 0), Vector2.right);
            Assert.AreEqual(new Vector2(-1, 0), Vector2.left);
            Assert.AreEqual(new Vector2(1, 1), Vector2.one);
            Assert.AreEqual(new Vector2(0, 0), Vector2.zero);
        }

        [TestMethod]
        public void Vector2Substraction()
        {
            //Arrange
            var result = new Vector2(5, 7) - new Vector2(2, 3);

            //Assert
            Assert.AreEqual(new Vector2(3, 4), result);
        }

        [TestMethod]
        public void Vector2Multiplication()
        {
            //Arrange
            var result = new Vector2(2, 3) * 2;

            //Assert
            Assert.AreEqual(new Vector2(4, 6), result);
        }

        [TestMethod]
        public void Vector2Division()
        {
            //Arrange
            var result = new Vector2(4, 6) / 2;

            //Assert
            Assert.AreEqual(new Vector2(2, 3), result);
        }

        [TestMethod]
        public void Vector2Equals()
        {
            //Assert
            Assert.IsTrue(new Vector2(1, 1) == Vector2.one);
            Assert.IsTrue(Vector2.left != Vector2.zero);
        }

        [TestMethod]
        public void Vector2Distance()
        {
            //Arrange
            Vector2 a = Vector2.zero;
            Vector2 b = new Vector2(3, 4);
            Assert.AreEqual(5, Vector2.Distance(a, b));
        }

        [TestMethod]
        public void Vector2ToString()
        {
            Vector2 a = new Vector2(2.5f, 3.5f);
            Assert.AreEqual("(2.5, 3.5)", a.ToString());
        }
    }
}
