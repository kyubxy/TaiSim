using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TaiSim.Framework.Graphics;

public class Container : Node
{
    public List<Node> children;
    private ContentManager cm;

    public Container()
    {
        children = new List<Node>();
    }

    public Container(List<Node> c)
    {
        children = c;
    }

    public void Add(Node n)
    {
        if (cm != null)
        {
            // already loaded
            if (!n.Loaded) 
                n.Load(cm);
        }
        children.Add(n);
    }

    public void Remove(Node n)
    {
        children.Remove(n);
    }
    
    public override void Load(ContentManager cm)
    {
        this.cm = cm;
        
        foreach(var c in children)
            c.Load(cm);
        
        base.Load(cm);
    }

    public override void Update(GameTime gt)
    {
        foreach (var c in children)
        {
            if (!ExecutionMode.HasFlag(Execution.NoUpdate))
                c.Update(gt);
        }
    }

    public override void Draw(GameTime gt, SpriteBatch sb)
    {
        foreach (var c in children)
        {
            if (!ExecutionMode.HasFlag(Execution.NoDraw))
                c.Draw(gt, sb);
        }
    }
}