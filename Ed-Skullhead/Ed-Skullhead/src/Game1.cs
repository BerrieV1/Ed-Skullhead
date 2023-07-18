using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Ed_Skullhead.Sound;
using Apos.Gui;
using FontStashSharp;

namespace Ed_Skullhead.src
{
    public class Game1 : Game
    {
        public int points = 0;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager screenManager;

        #region UI
        IMGUI ui;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenManager = new ScreenManager();
            Components.Add(screenManager);
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
            base.Initialize();
            LoadLevel1();
            SoundManager.Initialize(Content);
            SoundManager.PlaySound("background", true);
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CreateUI();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            #region UI
            GuiHelper.UpdateSetup(gameTime);
            ui.UpdateAll(gameTime);
            Panel.Push().XY = new Vector2(0, 0);
            Label.Put($"Points: {points}");
            Panel.Pop();
            GuiHelper.UpdateCleanup();
            #endregion
        }
        public void LoadLevel1()
        {
            screenManager.LoadScreen(new Level1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel2()
        {
            screenManager.LoadScreen(new Level2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        protected virtual void CreateUI()
        {
            FontSystem fontSystem = FontSystemFactory.Create(GraphicsDevice, 2048, 2048);
            fontSystem.AddFont(TitleContainer.OpenStream("Content/Wizard's Manse.otf"));
            GuiHelper.Setup(this, fontSystem);
            ui = new IMGUI();
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            #region UI
            ui.Draw(gameTime);
            #endregion
        }
    }
}
