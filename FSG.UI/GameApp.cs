using System;
using System.IO;
using FSG.Commands;
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

    private double _elapsedTime = 0;

    public GameApp()
    {
        _game = new FSG.Core.Game();

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.GraphicsProfile = GraphicsProfile.HiDef;

        //_viewportLeft = new Viewport(0, 0, 400, 800);
        //_viewportRight = new Viewport(400, 0, 880, 800);

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

        var assetResolver = new FileAssetResolver("../../../UI");
        var assetManager = new AssetManager(assetResolver);

        var serviceProvider = new UIServiceProvider(_game.ServiceProvider, _eventManager, GraphicsDevice, _spriteBatch, _camera, assetManager);

        _ui = new UI(serviceProvider);
        _ui.Initialize();
        _ui.OnUIMouseEntered += HandleUIMouseEnter;
        _ui.OnUIMouseLeft += HandleUIMouseLeave;

        // The game needs to be initialized after the UI is created so the UI
        // can subscribe to game events
        _game.Initialize();

        // Maps need to created and initialized after game so it can fetch
        // regions
        _map = new Map(_game.ServiceProvider, _eventManager, GraphicsDevice, _spriteBatch, _camera);
        _map.Initialize();
    }

    private void HandleUIMouseLeave(object sender, EventArgs e)
    {
        _map.HandleInput = true;
    }

    private void HandleUIMouseEnter(object sender, EventArgs e)
    {
        _map.HandleInput = false;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
        _map.Update(gameTime);
        _ui.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.PaleTurquoise);

        // Draw UI
        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix);
        _map.Draw();
        _spriteBatch.End();

        _spriteBatch.Begin();
        _ui.Draw();
        _spriteBatch.End();


        // FPS
        //System.Console.WriteLine($"FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds}");
    }
}

