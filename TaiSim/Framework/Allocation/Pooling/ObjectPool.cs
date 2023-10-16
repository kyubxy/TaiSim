using System;
using System.Collections.Generic;

namespace TaiSim.Framework.Allocation.Pooling;

public class PoolingException : Exception
{
    public PoolingException(string msg) : base (msg) {}
}
    
public abstract class ObjectPool<T> 
    where T : IPooledObject
{
    private const double GROWTH_FACTOR = 0.3;
    
    private Stack<T> pool;
    private List<T> inUse;
    private int size;
    
    public ObjectPool()
    {
        // initialise the pool
        pool = new Stack<T>();
        inUse = new List<T>();
    }

    /// <summary>
    /// Populate the pool with premade objects
    /// </summary>
    /// <param name="initialSize">initial size of the pool</param>
    /// <exception cref="PoolingException">
    /// throws when either the pool has already been initialised or
    /// the initial size is negative or zero
    /// </exception>
    public void InitialisePool(int initialSize)
    {
        if (initialSize <= 0)
            throw new PoolingException("specify a non zero positive number for the initial pool size");
        if (size > 0)
            throw new PoolingException("pool has already been initialised");
        size = initialSize;
        GrowPool(size);
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns>A pooled object</returns>
    public T GetAnother()
    {
        T ret;
        if (!pool.TryPop(out ret))
            ret = GrowPool((int)(GROWTH_FACTOR*size));
        inUse.Add(ret);
        ret.Prepare();
        return ret;
    }

    // resizes the pool by the delta, never adjust the size manually
    T GrowPool(int delta)
    {
        if (delta <= 0)
            throw new Exception("delta needs to be greater than 0");
        size += delta;
        for (int i = 0; i < delta; i++)
            pool.Push(CreateNewPooledObject());
        return pool.Pop();
    }

    /// <summary>
    /// Release an object back into the pool
    /// </summary>
    /// <param name="obj">the object to free</param>
    /// <exception cref="PoolingException">throws when a foreign object attempts to enter the pool</exception>
    public void Release(T obj)
    {
        // check the item belongs to this pool
        if (!inUse.Contains(obj))
            throw new PoolingException($"cannot release object to this pool, it wasn't created here");
        obj.CleanUp();
        inUse.Remove(obj);
        pool.Push(obj);
    }

    /// <summary>
    /// Instantiate the pooled object
    /// </summary>
    /// <returns>The new object</returns>
    protected abstract T CreateNewPooledObject();
}