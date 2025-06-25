using System.Collections.Generic;
using System;

namespace MyGame
{
    //Generic class for pooling any IPoolable object with parameterless constructor.
    public class ObjectPool<T> where T: class, IPoolable, new()
    {
        //Store inactive objects in stack.
        private readonly Stack<T> pool = new Stack<T>();

        //Dinamic pool: Returns object from the pool, or creates a new one if the pool is empty.
        public T Get() => pool.Count > 0 ? pool.Pop() : new T();

        //Return object back to pool after resetting state.
        public void Return(T obj)
        {
            obj.Reset();
            pool.Push(obj);
        }
    }
}
