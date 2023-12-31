﻿using Ed_Skullhead.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading;
using TiledSharp;

namespace Ed_Skullhead.src
{
    public class Level2 : LevelBase
    {
        private bool isEnded = false;
        public Level2(Game1 game) : base(game)
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            CreateEnemy(Content.Load<Texture2D>("3 - Hermie_Crawling (32 x 32)"), enemyPath[0], 1f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("5 - Robot_Walky_Movement (32 x 32)"), enemyPath[1], 2f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("2 - Martian_Red_Running (32 x 32)"), enemyPath[2], 3f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("7 - Orchid_Owl_Flying (32 x 32)"), enemyPath[3], 2f, 1.2f);
            player = CreatePlayer(new Vector2(577, 0), new List<Texture2D>() { Content.Load<Texture2D>("Skeleton Idle"), Content.Load<Texture2D>("Skeleton Walk") }, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
            SoundManager.Initialize(Content);
            SoundManager.PlayBackgroundMusic("background_level2");
        }
        public override void LoadTileMap()
        {
            map = new TmxMap("Content/level2.tmx");
            base.LoadTileMap();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (endRect.Intersects(player.hitbox))
            {
                if (!isEnded)
                {
                    isEnded = true;
                    var game1 = (Game1)Game;
                    SoundManager.StopBackgroundMusic();
                    SoundManager.PlaySound("victory");
                    Thread.Sleep(5000);
                    game1.LoadVictory();
                }
            }
        }
    }
}