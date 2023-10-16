using TaiSim.Framework;
using TaiSim.Screens;

namespace TaiSim;

public class TaiSimGame : GameBase
{
    public TaiSimGame()
    {
        screenStack.Push(new PrototypeScreen());
    }

    protected override void Initialize()
    {
        base.Initialize();
        _graphics.PreferredBackBufferWidth = 1366;
        _graphics.PreferredBackBufferHeight = 768;
        _graphics.ApplyChanges();

        Window.Title = "TaiSim test";
    }
}