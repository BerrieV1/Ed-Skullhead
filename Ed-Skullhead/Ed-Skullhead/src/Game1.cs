﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Ed_Skullhead.Sound;
using Ed_Skullhead.Levels;

namespace Ed_Skullhead.src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager screenManager;

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
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void LoadLevel1()
        {
            screenManager.LoadScreen(new Level1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel2()
        {
            screenManager.LoadScreen(new Level2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadStartMenu()
        {
            screenManager.LoadScreen(new Menu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
