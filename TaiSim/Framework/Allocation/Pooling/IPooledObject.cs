namespace TaiSim.Framework.Allocation.Pooling;

public interface IPooledObject
{
    /// <summary>
    /// Modify the object before it is sent to the user. Called when the object is retried from
    /// the object pool
    /// </summary>
    void Prepare();
    
    /// <summary>
    /// Deinitialise the object. Called when the object is released to the object pool
    /// </summary>
    void CleanUp();
}