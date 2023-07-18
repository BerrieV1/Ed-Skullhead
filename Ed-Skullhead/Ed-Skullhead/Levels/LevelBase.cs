using Ed_Skullhead.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using TiledSharp;

namespace Ed_Skullhead.src
{
    public class LevelBase : GameScreen
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        #region Player
        public Player player;
        public List<Texture2D> sprites = new List<Texture2D>();
        public Vector2 startPosition = new Vector2();
        public float playerSpeed = 2.5f;
        public float playerScale = 1.2f;
        public float playerFallSpeed = 3f;
        public float playerJumpSpeed = 1f;
        #endregion

        #region Map
        public TileMapManager tileMapManager;
        public List<Rectangle> collisions;
        public TmxMap map;
        #endregion

        #region Enemy
        public List<Enemy> enemies = new List<Enemy>();
        public List<Rectangle> enemyPath;
        public Rectangle startRect;
        public Rectangle endRect;
        #endregion

        public bool isGameOver = false;
        private new Game1 Game => (Game1)base.Game;
        public LevelBase(Game1 game) : base(game)
        {

        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadTileMap();
            LoadCollisions();
            CreateEnemyPaths();
        }
        protected virtual void CreateEnemyPaths()
        {
            enemyPath = new List<Rectangle>();
            foreach (var objects in map.ObjectGroups["EnemyPath"].Objects)
            {
                enemyPath.Add(new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height));
            }
        }
        public Enemy CreateEnemy(Texture2D spritesheet, Rectangle path, float speed, float scale)
        {
            enemies.Add(new Enemy(spritesheet, path, speed, scale));
            return new Enemy(spritesheet, path, speed, scale);
        }
        public virtual Player CreatePlayer(Vector2 startPosition, List<Texture2D> sprites, float playerSpeed, float playerScale, float playerFallSpeed, float playerJumpSpeed)
        {
            return new Player(startPosition, sprites, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
        }
        public virtual void LoadTileMap()
        {
            Texture2D tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int tilesetTilesWide = tileset.Width / tileWidth;
            tileMapManager = new TileMapManager(tileset, map, tileWidth, tileHeight, tilesetTilesWide);
        }
        protected virtual void LoadCollisions()
        {
            collisions = new List<Rectangle>();
            foreach (var objects in map.ObjectGroups["Collisions"].Objects)
            {
                if (objects.Name == "")
                    collisions.Add(new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height));
                if (objects.Name == "Start")
                    startRect = new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height);
                if (objects.Name == "End")
                    endRect = new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            foreach (var enemy in enemies)
            {
                enemy.Update();
                isGameOver = enemy.HasHit(player.hitbox);
                if (isGameOver)
                    Game.Exit();
            }
            Vector2 position = player.position;
            player.Update();
            HandleCollisions(position);
        }
        private void HandleCollisions(Vector2 position)
        {
            HandleCollisionsXAxis(position);
            HandleCollisionsYAxis(position);
        }
        private void HandleCollisionsXAxis(Vector2 position)
        {
            foreach (var rect in collisions)
            {
                if (rect.Intersects(player.hitbox))
                {
                    player.position.X = position.X;
                    player.velocity.X = position.X;
                    break;
                }
            }
        }
        private void HandleCollisionsYAxis(Vector2 position)
        {
            foreach (var rect in collisions)
            {
                if (!player.isJumping)
                    player.isFalling = true;
                if (rect.Intersects(player.fallRect))
                {
                    player.isFalling = false;
                    break;
                }
                if (rect.Intersects(player.jumpRect))
                {
                    player.isFalling = false;
                    player.position.Y = position.Y;
                    player.velocity.Y = position.Y;
                    break;
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            tileMapManager.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }
            player.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}