using Apos.Gui;
using Ed_Skullhead.Collectibles;
using Ed_Skullhead.Entities;
using Ed_Skullhead.Factories;
using Ed_Skullhead.Factory;
using Ed_Skullhead.Sound;
using Ed_Skullhead.Throwable;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public bool isGameOver = false;
        public bool isDying = false;
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

        #region Coins
        private Texture2D coinTexture;
        public List<Coin> coins = new List<Coin>();
        #endregion

        #region Bones
        private Texture2D boneTexture;
        public List<Bone> boneList = new List<Bone>();
        public List<ThrowBone> throwBones = new List<ThrowBone>();
        private int timeBetweenThrows;
        #endregion

        #region UI
        IMGUI ui;
        #endregion

        #region Factories
        private EntityFactory entityFactory;
        private CollectibleFactory collectibleFactory;
        #endregion

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
            InitializeFactories();
            LoadTileMap();
            LoadCollisions();
            CreateEnemyPaths();
            CreateCoins();
            CreateBones();
            CreateUI();
        }
        private void InitializeFactories()
        {
            entityFactory = new EntityFactory();
            collectibleFactory = new CollectibleFactory();
        }
        private void CreateUI()
        {
            FontSystem fontSystem = FontSystemFactory.Create(base.Game.GraphicsDevice, 2048, 2048);
            fontSystem.AddFont(TitleContainer.OpenStream("Content/dysin4mation.ttf"));
            GuiHelper.Setup(base.Game, fontSystem);
            ui = new IMGUI();
        }
        private void CreateCoins()
        {
            coinTexture = Content.Load<Texture2D>("coin");
            foreach(var objects in map.ObjectGroups["Coins"].Objects)
            {
                coins.Add(collectibleFactory.CreateCoin(coinTexture, new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height)));
            }
        }
        private void CreateBones()
        {
            boneTexture = Content.Load<Texture2D>("bone");
            foreach (var objects in map.ObjectGroups["Bones"].Objects)
            {
                boneList.Add(collectibleFactory.CreateBone(boneTexture, new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height)));
            }
        }
        private void CreateEnemyPaths()
        {
            enemyPath = new List<Rectangle>();
            foreach (var objects in map.ObjectGroups["EnemyPath"].Objects)
            {
                enemyPath.Add(new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height));
            }
        }
        public Enemy CreateEnemy(Texture2D enemyTexture, Rectangle path, float speed, float scale)
        {
            enemies.Add(entityFactory.CreateEnemy(enemyTexture, path, speed, scale));
            return enemies.Last();
        }
        public Player CreatePlayer(Vector2 startPosition, List<Texture2D> sprites, float playerSpeed, float playerScale, float playerFallSpeed, float playerJumpSpeed)
        {
            return entityFactory.CreatePlayer(startPosition, sprites, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
        }
        public virtual void LoadTileMap()
        {
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

            #region Enemies
            foreach (var enemy in enemies)
            {
                enemy.Update();
                isGameOver = enemy.Hit(player.hitbox);
                if (isGameOver)
                {
                    if (!isDying)
                    {
                        isDying = true;
                        SoundManager.StopBackgroundMusic();
                        SoundManager.PlaySound("death");
                        Thread.Sleep(2000);
                        Game.LoadGameOver();
                    }
                }
            }
            #endregion

            #region Coins
            foreach (var coin in coins.ToArray())
            {
                if (coin.hitbox.Intersects(player.hitbox))
                {
                    SoundManager.PlaySound("collect");
                    Game.points += coin.value;
                    coins.Remove(coin);
                    break;
                }
            }
            #endregion

            #region Bones
            foreach (var bone in boneList.ToArray())
            {
                if (bone.hitbox.Intersects(player.hitbox))
                {
                    SoundManager.PlaySound("collect");
                    Game.bones += bone.value;
                    boneList.Remove(bone);
                    break;
                }
            }
            #endregion

            #region Player
            Vector2 position = player.position;
            if (player.isThrowing)
            {
                if (timeBetweenThrows > 7 && Game.bones > 0)
                {
                    var temp_hitbox = new Rectangle((int)player.position.X, (int)player.position.Y + 10, boneTexture.Width, boneTexture.Height);
                    if (player.spriteEffect == SpriteEffects.None)
                    {
                        throwBones.Add(new ThrowBone(boneTexture, 4f, temp_hitbox, 0.3f));
                    }
                    if (player.spriteEffect == SpriteEffects.FlipHorizontally)
                    {
                        throwBones.Add(new ThrowBone(boneTexture, -4f, temp_hitbox, 0.3f));
                    }
                    SoundManager.PlaySound("throw");
                    timeBetweenThrows = 0;
                    Game.bones--;
                }
                else
                {
                    timeBetweenThrows++;
                }
            }
            player.Update();
            #endregion

            #region ThrowBones
            foreach (var throwBone in throwBones.ToArray())
            {
                throwBone.Update();
                foreach (var rect in collisions)
                {
                    if (rect.Intersects(throwBone.scaledHitbox))
                    {
                        throwBones.Remove(throwBone);
                        break;
                    }
                }
                foreach (var enemy in enemies.ToArray())
                {
                    if (throwBone.scaledHitbox.Intersects(enemy.hitbox))
                    {
                        SoundManager.PlaySound("enemy_death");
                        enemies.Remove(enemy);
                        throwBones.Remove(throwBone);
                        Game.points += 5;
                        break;
                    }
                }
            }
            #endregion

            #region Collisions
            HandleCollisions(position);
            #endregion

            #region UI
            GuiHelper.UpdateSetup(gameTime);
            ui.UpdateAll(gameTime);
            Panel.Push().XY = new Vector2(0, 0);
            Label.Put($"Points: {Game.points}");
            Panel.Pop();
            Panel.Push().XY = new Vector2(520, 0);
            Label.Put($"Bones: {Game.bones}");
            Panel.Pop();
            GuiHelper.UpdateCleanup();
            #endregion
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

            #region Map
            tileMapManager.Draw(spriteBatch);
            #endregion

            #region Enemies
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }
            #endregion

            #region Coins
            foreach (var coin in coins)
            {
                coin.Draw(spriteBatch, gameTime);
            }
            #endregion

            #region Bones
            foreach (var bone in boneList)
            {
                bone.Draw(spriteBatch, gameTime);
            }
            #endregion

            #region ThrowBones
            foreach (var throwBone in throwBones.ToArray())
            {
                throwBone.Draw(spriteBatch);
            }
            #endregion

            #region Player
            player.Draw(spriteBatch, gameTime);
            #endregion

            #region UI
            spriteBatch.End();
            spriteBatch.Begin();
            ui.Draw(gameTime);
            #endregion

            spriteBatch.End();
        }
    }
}