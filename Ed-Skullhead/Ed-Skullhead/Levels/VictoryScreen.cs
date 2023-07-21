using Apos.Gui;
using Ed_Skullhead.Sound;
using Ed_Skullhead.src;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Ed_Skullhead.Levels
{
    public class VictoryScreen : Screen
    {
        private new Game1 Game => (Game1)base.Game;
        IMGUI ui;
        public VictoryScreen(Game game) : base(game)
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            SoundManager.PlayBackgroundMusic("end");
            ui = new IMGUI();
        }
        public override void Update(GameTime gameTime)
        {
            GuiHelper.UpdateSetup(gameTime);
            ui.UpdateAll(gameTime);
            Panel.Push().XY = new Vector2(220, 80);
            Label.Put("You Won!", 50, Color.LimeGreen);
            Panel.Pop();

            Panel.Push().XY = new Vector2(230, 170);
            if (Button.Put("Restart Game").Clicked)
            {
                SoundManager.PlaySound("click");
                Game.LoadLevel1();
                SoundManager.StopBackgroundMusic();
            }
            Panel.Pop();

            Panel.Push().XY = new Vector2(280, 240);
            if (Button.Put("Quit").Clicked)
            {
                SoundManager.PlaySound("click");
                Game.Exit();
            }
            Panel.Pop();
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
