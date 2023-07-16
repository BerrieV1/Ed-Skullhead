using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TiledSharp;

namespace Ed_Skullhead.src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private TileMapManager tileMapManager;
        private List<Rectangle> collisions;
        private TmxMap map;

        private Enemy enemy1, enemy2, enemy3;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Rectangle> enemyPath;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = CreatePlayer();
            LoadTileMap();
            LoadCollisions();

            enemyPath = new List<Rectangle>();
            foreach (var objects in map.ObjectGroups["EnemyPath"].Objects)
            {
                enemyPath.Add(new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height));
            }
            CreateEnemy(Content.Load<Texture2D>("10 - Blankey_Floating (32 x 32)"), enemyPath[0], 2f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("3 - Hermie_Crawling (32 x 32)"), enemyPath[1], 1f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("8 - Roach_Running (32 x 32)"), enemyPath[2], 3f, 1.2f);
        }
        private Enemy CreateEnemy(Texture2D spritesheet, Rectangle path, float speed, float scale)
        {
            enemies.Add(new Enemy(spritesheet, path, speed, scale));
            return new Enemy(spritesheet, path, speed, scale);

        }
        private Player CreatePlayer()
        {
            List<Texture2D> sprites = new List<Texture2D>() { Content.Load<Texture2D>("Skeleton Idle"), Content.Load<Texture2D>("Skeleton Walk") }; // 0 = idle, 1 = run
            Vector2 startPosition = new Vector2(40, 340);
            float playerSpeed = 2.5f;
            float playerScale = 1.2f;
            float playerFallSpeed = 3f;
            float playerJumpSpeed = 1f;
            return new Player(startPosition, sprites, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
        }
        private void LoadTileMap()
        {
            map = new TmxMap("Content/level1.tmx");
            Texture2D tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int tilesetTilesWide = tileset.Width / tileWidth;
            tileMapManager = new TileMapManager(tileset, map, tileWidth, tileHeight, tilesetTilesWide);
        }
        private void LoadCollisions()
        {
            collisions = new List<Rectangle>();
            foreach (var objects in map.ObjectGroups["Collisions"].Objects)
            {
                if (objects.Name == "")
                    collisions.Add(new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height));
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (var enemy in enemies)
            {
                enemy.Update();
            }
            Vector2 position = player.position;
            player.Update();
            HandleCollisions(position);
            base.Update(gameTime);
        }
        private void HandleCollisions(Vector2 position)
        {
            HandleCollisionsXAxis(position);
            HandleCollisionsYAxis();
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
        private void HandleCollisionsYAxis()
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
            }
        }
        protected override void Draw(GameTime gameTime)
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
            base.Draw(gameTime);
        }
    }
}