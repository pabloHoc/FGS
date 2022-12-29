using System;
using System.IO;
using FSG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Myra.Utility;

namespace FSG;

public class GameApp : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private readonly FSG.Core.Game _game;
    private FSG.UI.UI _ui;

    public GameApp()
    {
        _game = new FSG.Core.Game();
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferHeight = 768;
        _graphics.PreferredBackBufferWidth = 1024;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        MyraEnvironment.Game = this;

        _ui = new FSG.UI.UI(_game.ServiceProvider);
        _ui.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightGray);
        // TODO: Add your drawing code here

        base.Draw(gameTime);
        _ui.Draw();
    }
}

