using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ed_Skullhead.src
{
    public class Enemy : Entity
    {
        private Animation enemyAnimation;
        private Rectangle path;
        private float speed;
        private float scale;
        private bool isFacingRight = true;
        public Enemy(Texture2D spritesheet, Rectangle path, float speed, float scale)
        {
            this.path = path;
            enemyAnimation = new Animation(spritesheet, 32, 32);
            position = new Vector2(path.X, path.Y);
            //hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            this.speed = speed;
            this.scale = scale;
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isFacingRight)
                enemyAnimation.Draw(spriteBatch, position, gameTime, scale, SpriteEffects.None);
            else
                enemyAnimation.Draw(spriteBatch, position, gameTime, scale, SpriteEffects.FlipHorizontally);
        }
        public override void Update()
        {
            if (!path.Contains(hitbox))
            {
                speed = -speed;
                isFacingRight = !isFacingRight;
            }
            position.X += speed;

            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }
    }
}
