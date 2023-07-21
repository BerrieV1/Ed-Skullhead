using Apos.Gui;
using Ed_Skullhead.Sound;
using Ed_Skullhead.src;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace Ed_Skullhead.Levels
{
    public class Menu : GameScreen
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private new Game1 Game => (Game1)base.Game;
        IMGUI ui;
        public Menu(Game game) : base(game)
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundManager.Initialize(Content);
            SoundManager.PlayBackgroundMusic("start_menu");
            CreateUI();
        }
        protected virtual void CreateUI()
        {
            FontSystem fontSystem = FontSystemFactory.Create(base.Game.GraphicsDevice, 2048, 2048);
            fontSystem.AddFont(TitleContainer.OpenStream("Content/dysin4mation.ttf"));
            GuiHelper.Setup(base.Game, fontSystem);
            ui = new IMGUI();
        }
        public override void Update(GameTime gameTime)
        {
            DrawUI(gameTime);
        }
        protected virtual void DrawUI(GameTime gameTime)
        {
            GuiHelper.UpdateSetup(gameTime);
            ui.UpdateAll(gameTime);
            Panel.Push().XY = new Vector2(190, 80);
            Label.Put("Ed Skullhead", 50, Color.Red);
            Panel.Pop();

            Panel.Push().XY = new Vector2(250, 170);
            if (Button.Put("Start Game").Clicked)
            {
                SoundManager.PlaySound("click");
                Game.LoadLevel1();
                SoundManager.StopBackgroundMusic();
            }
            Panel.Pop();

            Panel.Push().XY = new Vector2(255, 240);
            if (Button.Put(" Level 2 ").Clicked)
            {
                SoundManager.PlaySound("click");
                Game.LoadLevel2();
                SoundManager.StopBackgroundMusic();
            }
            Panel.Pop();

            Panel.Push().XY = new Vector2(250, 310);
            if (Button.Put("   Quit   ").Clicked)
            {
                SoundManager.PlaySound("click");
                Game.Exit();
            }
            Panel.Pop();
            GuiHelper.UpdateCleanup();
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            ui.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
