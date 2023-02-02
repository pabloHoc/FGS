using System;
using FSG.Core;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Myra.Assets;

namespace FSG.UI
{
	public class UIServiceProvider
	{
        public ServiceProvider GameServiceProvider { get; init; }

		public UIEventManager EventManager { get; init; }

		public GraphicsDevice GraphicsDevice { get; init; }

		public SpriteBatch SpriteBatch { get; init; }

		public OrthographicCamera Camera { get; init; }

        public AssetManager AssetManager { get; init; }

        public UIServiceProvider(
			ServiceProvider serviceProvider,
			UIEventManager eventManager,
			GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch,
            OrthographicCamera camera,
            AssetManager assetManager
        )
		{
            GameServiceProvider = serviceProvider;
			EventManager = eventManager;
			GraphicsDevice = graphicsDevice;
			SpriteBatch = spriteBatch;
			Camera = camera;
			AssetManager = assetManager;
		}
	}
}

