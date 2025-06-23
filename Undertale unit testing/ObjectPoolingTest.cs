using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using System;

namespace Undertale_unit_testing
{
    [TestClass]
    public class ObjectPoolingTest
    {
        [TestMethod]
        public void GetNotNull()
        {
            var pool = new ObjectPool<MockBone>();
            var obj = pool.Get();
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ReusingReturnedObject()
        {
            //Arrange
            var pool = new ObjectPool<MockBone>();

            //Act
            var first = pool.Get();
            pool.Return(first);
            var second = pool.Get();

            //Assert
            Assert.AreSame(first, second); //Test same instance is reused.
        }

        [TestMethod]
        public void ResetIsDone()
        {
            //Arrange
            var pool = new ObjectPool<MockBone>();
            
            //Act
            var obj = pool.Get();
            obj.WasReset = false;
            pool.Return(obj);
            
            //Assert
            Assert.IsTrue(obj.WasReset);
        }

        [TestMethod]
        public void DinamicallyGettingObjects()
        {
            //Arrange
            var pool = new ObjectPool<MockBone>();

            //Act
            var obj1 = pool.Get();
            var obj2 = pool.Get();
            var obj3 = pool.Get();

            //Assert
            Assert.AreNotSame(obj1, obj2); //Comparing value types.
            Assert.AreNotSame(obj1, obj3);
            Assert.AreNotSame(obj2, obj3);
        }
    }
    //Mock class for testing.
    public class MockBone : IPoolable
    {
        public bool WasReset { get; set; } = false;
        public void Reset() => WasReset = true;   
    }
}
