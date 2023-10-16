#nullable enable
namespace TaiSim.Framework.Structures;

/// <summary>
/// Basically a stack you can move around in
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITimeline<T>
{
    T? GetNext();
    /// <summary>
    /// moves the playhead
    /// </summary>
    /// <param name="t"></param>
    /// <returns>true if the time is in bounds</returns>
    bool Seek(Time t);
}