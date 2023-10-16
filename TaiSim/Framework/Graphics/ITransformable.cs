
using Microsoft.Xna.Framework;

namespace TaiSim.Framework.Graphics;

public interface ITransformable
{
    Point Position { get; set; }
    Point Size { get; set; }
    Rectangle BoundingBox { get; }
}