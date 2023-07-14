using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TiledSharp;

namespace Ed_Skullhead
{
    public class Game1 : Game
    {
        #region Game
        GraphicsDeviceManager graphics;
        #endregion

        #region Player
        private SpriteBatch _spriteBatch;
        private Texture2D idleSprite;
        private Texture2D runSprite;
        private Player player;
        private float playerSpeed = 3f;
        private float playerScale = 1.2f;
        private float playerFallSpeed = 3f;
        private float playerJumpSpeed = 1f;
        #endregion

        #region TileMap
        private TmxMap map;
        private TileMapManager tileMapManager;
        private Texture2D tileset;
        private List<Rectangle> collisions;
        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region TileMap
            map = new TmxMap("Content/level1.tmx");
            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int tilesetTilesWide = tileset.Width / tileWidth;
            tileMapManager = new TileMapManager(tileset, map, tileWidth, tileHeight, tilesetTilesWide);
            #endregion

            #region Player
            idleSprite = Content.Load<Texture2D>("Skeleton Idle");
            runSprite = Content.Load<Texture2D>("Skeleton Walk");
            player = new Player(idleSprite, runSprite, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
            #endregion

            #region Collisions
            collisions = new List<Rectangle>();
            foreach (var objects in map.ObjectGroups["Collisions"].Objects)
            {
                if (objects.Name == "")
                {
                    collisions.Add(new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height));
                }
            }
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var position = player.position;
            player.Update();
            #region Collisions Y-Axis
            foreach (var rect in collisions)
            {
                if (!player.isJumping)
                player.isFalling = true;
                if (rect.Intersects(player.fallRect))
                {
                    player.isFalling = false;
                    break;
                }
            }
            #endregion

            #region Collisions X-Axis
            foreach (var rect in collisions)
            {
                if (rect.Intersects(player.hitbox))
                {
                    player.position.X = position.X;
                    player.velocity.X = position.X;
                    break;
                }
            }
            #endregion
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            tileMapManager.Draw(_spriteBatch);
            player.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}