using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;

namespace FSG;

public class GameApp : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Desktop _desktop;

    public GameApp()
    {
        _graphics = new GraphicsDeviceManager(this);
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
        CreateUI();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _desktop.Render();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }

    private void CreateUI()
    {
        MyraEnvironment.Game = this;

        var grid = new Grid
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };

        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        // Label
        var helloWorld = new Label
        {
            Id = "label",
            Text = "Hello, World!"
        };

        grid.Widgets.Add(helloWorld);

        // Button
        var button = new TextButton
        {
            GridColumn = 0,
            GridRow = 1,
            Text = "Show"
        };

        button.Click += (s, a) =>
        {
            var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
            messageBox.ShowModal(_desktop);
        };

        grid.Widgets.Add(button);

        // Add it to the desktop
        _desktop = new Desktop();
        _desktop.Root = grid;
    }
}

