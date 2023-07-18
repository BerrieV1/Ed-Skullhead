using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TiledSharp;

namespace Ed_Skullhead.src
{
    public class Level1 : LevelBase
    {
        public Level1(Game1 game) : base(game)
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            CreateEnemy(Content.Load<Texture2D>("10 - Blankey_Floating (32 x 32)"), enemyPath[0], 2f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("3 - Hermie_Crawling (32 x 32)"), enemyPath[1], 1f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("8 - Roach_Running (32 x 32)"), enemyPath[2], 3f, 1.2f);
            CreateEnemy(Content.Load<Texture2D>("7 - Orchid_Owl_Flying (32 x 32)"), enemyPath[3], 2.5f, 1.2f);
            player = CreatePlayer(new Vector2(startRect.X + 9, startRect.Y - 12), new List<Texture2D>() { Content.Load<Texture2D>("Skeleton Idle"), Content.Load<Texture2D>("Skeleton Walk") }, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
        }
        public override void LoadTileMap()
        {
            map = new TmxMap("Content/level1.tmx");
            base.LoadTileMap();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (endRect.Intersects(player.hitbox))
            {
                var game1 = (Game1)Game;
                game1.LoadLevel2();
            }
        }
    }
}