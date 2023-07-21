using Apos.Gui;
using Ed_Skullhead.Sound;
using Ed_Skullhead.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead.Levels
{
    public class StartScreen : Screen
    {
        private new Game1 Game => (Game1)base.Game;
        IMGUI ui;
        public StartScreen(Game game) : base(game)
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            SoundManager.PlayBackgroundMusic("start_menu");
            ui = new IMGUI();
        }
        public override void Update(GameTime gameTime)
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
