namespace TaiSim.Framework.Graphics;

public abstract class Screen : Container
{
    /// <summary>
    /// Called once when screen is first pushed to the stack
    /// </summary>
    public virtual void OnEnter() {}
    
    /// <summary>
    /// called when the screen returns to the first position
    /// </summary>
    public virtual void OnResume() {}
    
    /// <summary>
    /// called when the screen is pushed over
    /// </summary>
    public virtual void OnSuspend() {}
    
    /// <summary>
    /// called when the screen is popped
    /// </summary>
    public virtual void OnExit(){}
}