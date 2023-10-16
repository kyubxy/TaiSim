using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TaiSim.Framework.Graphics;

public class Sprite : Node, ITransformable
{
    private Texture2D tex;
    private string name;
    
    public Color Colour = Color.White;
    
    private Point pos = Point.Zero;
    public Point Position
    {
        get => pos;
        set => pos = value;
    }

    private Point size = new (120);
    public Point Size
    {
        get => size;
        set => size = value;
    }
    
    public Rectangle BoundingBox => new (Position, Size);
    
    public Sprite(string name)
    {
        this.name = name;
    }

    public override void Load(ContentManager cm)
    {
        tex = cm.Load<Texture2D>(name);
        base.Load(cm);
    }

    public override void Draw(GameTime gt, SpriteBatch sb)
    {
        base.Draw(gt, sb);
        sb.Draw(tex, BoundingBox, Colour);
    }

}