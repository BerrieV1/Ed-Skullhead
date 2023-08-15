using Apos.Gui;
using Ed_Skullhead.Sound;
using Ed_Skullhead.src;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Ed_Skullhead.Screens
{
    public class Screen : GameScreen
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private new Game1 Game => (Game1)base.Game;
        IMGUI ui;
        public Screen(Game game) : base(game)
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
        public virtual void CreateUI()
        {
            FontSystem fontSystem = FontSystemFactory.Create(base.Game.GraphicsDevice, 2048, 2048);
            fontSystem.AddFont(TitleContainer.OpenStream("Content/dysin4mation.ttf"));
            GuiHelper.Setup(base.Game, fontSystem);
            ui = new IMGUI();
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
        }
    }
}
