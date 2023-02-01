using System;
using FSG.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;

namespace FSG.UI
{
    public class MapLocation
    {
        private readonly Vector2 _position;

        private readonly EntityId<Region> _regionId;

        private bool _selected = false;

        private bool _hovered = false;

        private readonly bool _hasEmpire = false;

        private readonly UIEventManager _eventManager;

        private readonly SpriteBatch _spriteBatch;

        private readonly OrthographicCamera _camera;

        private readonly Texture2D _texture;

        private readonly Color _defaultColor = Color.Olive;

        private readonly Color _selectedColor = Color.DodgerBlue;

        private readonly Color _conqueredColor = Color.Crimson;

        private readonly Color _hoveredColor = Color.CornflowerBlue;

        private readonly Color _heroColor = Color.DarkGreen;

        private const int REGION_SIZE = 20;

        public MapLocation(
            Region region,
            UIEventManager eventManager,
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch,
            OrthographicCamera camera
        )
        {
            _position = new Vector2(region.X, region.Y);
            _regionId = region.Id;
            _hasEmpire = region.Empire != null;

            _eventManager = eventManager;
            _spriteBatch = spriteBatch;
            _camera = camera;

            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            _eventManager.OnRegionSelected += HandleRegionSelected;
        }

        private void HandleRegionSelected(object sender, Region e)
        {
            _selected = _eventManager.SelectedRegion.Id == _regionId;
        }

        private Color GetColor()
        {
            var color = _hasEmpire ? _conqueredColor : _defaultColor;

            if (_hovered)
            {
                color = _hoveredColor;
            }

            if (_selected)
            {
                color = _selectedColor;
            }

            if (_eventManager.SelectedAgent?.Region.Id == _regionId)
            {
                color = _heroColor;
            }

            return color;
        }

        public void Draw()
        {
            var rectangle = new Rectangle(
                (int)_position.X - REGION_SIZE / 2,
                (int)_position.Y - REGION_SIZE / 2,
                REGION_SIZE,
                REGION_SIZE
            );

            _spriteBatch.Draw(_texture, rectangle, GetColor());
        }

        private void CheckInput()
        {
            var mouseState = MouseExtended.GetState();

            var worldPosition = _camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));

            _hovered = worldPosition.X  > _position.X - REGION_SIZE / 2 &&
                    worldPosition.X < _position.X + REGION_SIZE / 2 &&
                    worldPosition.Y > _position.Y - REGION_SIZE / 2 &&
                    worldPosition.Y < _position.Y + REGION_SIZE / 2;

            if (_hovered && mouseState.IsButtonDown(MouseButton.Left))
            {
                _selected = _hovered;
                _eventManager.SelectRegion(_regionId);
            }

            if (_hovered && mouseState.IsButtonDown(MouseButton.Right))
            {
                _eventManager.SecondarySelectRegion(_regionId);
            }
        }

        public void Update()
        {
            CheckInput();
        }
    }
}

