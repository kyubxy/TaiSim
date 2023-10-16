using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TaiSim.Framework.Graphics;

public abstract class Node
{
    [Flags]
    public enum Execution
    {
        RunAll = 0,
        NoUpdate = 1,
        NoDraw = 2,
    }

    public Execution ExecutionMode;
    
    public bool Loaded { get; protected set; }
    
    public virtual void Load(ContentManager cm)
    {
        Loaded = true;
    }
    public virtual void Update(GameTime gt) {}
    public virtual void Draw(GameTime gt, SpriteBatch sb) {}
}