using Ed_Skullhead.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead.Entities
{
    public class Coin
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Vector2 position;
        public Animation collectibleAnimation;
        public Coin(Texture2D texture, Rectangle coinRect)
        {
            this.texture = texture;
            position = new Vector2(coinRect.X, coinRect.Y);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 9, 9);
            collectibleAnimation = new Animation(texture, 9, 9);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            collectibleAnimation.Draw(spriteBatch, position, gameTime, 2f, SpriteEffects.None);
        }
    }
}
