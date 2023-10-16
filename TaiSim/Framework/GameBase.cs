using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TaiSim.Framework.Graphics;

namespace TaiSim.Framework;

public class GameBase : Game
{
    
    private SpriteBatch _spriteBatch;
    
    protected GraphicsDeviceManager _graphics;
    protected ScreenStack screenStack;

    public GameBase()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        screenStack = new ScreenStack();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        screenStack.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        screenStack.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        screenStack.Draw(gameTime, _spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
