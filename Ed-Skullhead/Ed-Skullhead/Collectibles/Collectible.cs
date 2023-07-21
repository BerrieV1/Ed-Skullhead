using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Ed_Skullhead.src;

namespace Ed_Skullhead.Collectibles
{
    public class Collectible
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Vector2 position;
        public Animation collectibleAnimation;
        public int value;
        public Collectible(Texture2D texture, Rectangle collectibleRect)
        {
            this.texture = texture;
            position = new Vector2(collectibleRect.X, collectibleRect.Y);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 9, 9);
            collectibleAnimation = new Animation(texture, 9, 9);
            value = 0;
        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            collectibleAnimation.Draw(spriteBatch, position, gameTime, 2f, SpriteEffects.None);
        }
    }
}
