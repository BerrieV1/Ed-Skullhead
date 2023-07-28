using Ed_Skullhead.Entities;
using Ed_Skullhead.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ed_Skullhead.Factory
{
    public class EntityFactory
    {
        public EntityFactory()
        {
        }
        public Player CreatePlayer(Vector2 startPosition, List<Texture2D> sprites, float playerSpeed, float playerScale, float playerFallSpeed, float playerJumpSpeed)
        {
            return new Player(startPosition, sprites, playerSpeed, playerScale, playerFallSpeed, playerJumpSpeed);
        }
        public Enemy CreateEnemy(Texture2D enemyTexture, Rectangle path, float speed, float scale)
        {
            return new Enemy(enemyTexture, path, speed, scale);
        }
    }
}
