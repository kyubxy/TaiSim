using TaiSim.Framework;
using TaiSim.Framework.Graphics;

namespace TaiSim.Ruleset.Drawables.HitObjects;

public class Roll : Container
{
    public Roll()
    {
        Sprite head = new Sprite(@"textures/head");
        Sprite tail = new Sprite(@"textures/tail");
        Sprite body = new Sprite(@"textures/body");
    }
}