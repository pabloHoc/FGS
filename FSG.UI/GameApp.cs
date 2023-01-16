using System;
using System.IO;
using FSG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.ViewportAdapters;
using Myra;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Myra.Utility;

namespace FSG.UI;

public class GameApp : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    public readonly FSG.Core.Game _game;

    private FSG.UI.UI _ui;

    private Map _map;

    private readonly UIEventManager _eventManager;

    private OrthographicCamera _camera;

    private Viewport _viewportLeft;

    private Viewport _viewportRight;

    public GameApp()
    {
        _game = new FSG.Core.Game();

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 800;

        _viewportLeft = new Viewport(0, 0, 400, 800);
        _viewportRight = new Viewport(400, 0, 880, 800);

        _eventManager = new UIEventManager(_game.ServiceProvider);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        // GraphicsDevice is instantiated here
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Initialize Camera
        var viewportAdapter = new DefaultViewportAdapter(GraphicsDevice);
        _camera = new OrthographicCamera(viewportAdapter);

        // Initialize UI
        MyraEnvironment.Game = this;
        _ui = new FSG.UI.UI(_game.ServiceProvider, _eventManager, GraphicsDevice, _spriteBatch);
        _ui.Initialize();

        // The game needs to be initialized after the UI is created so the UI
        // can subscribe to game events
        _game.Initialize();

        // Maps need to created and initialized after game so it can fetch
        // regions
        _map = new Map(_game.ServiceProvider, _eventManager, GraphicsDevice, _spriteBatch, _camera);
        _map.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
        _map.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.PaleTurquoise);

        var originalViewport = GraphicsDevice.Viewport;

        // Draw UI
        GraphicsDevice.Viewport = _viewportLeft;
        _spriteBatch.Begin();
        _ui.Draw();
        _spriteBatch.End();

        // Draw Map
        GraphicsDevice.Viewport = _viewportRight;
        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix);
        _map.Draw();
        _spriteBatch.End();

        GraphicsDevice.Viewport = originalViewport;
    }
}

