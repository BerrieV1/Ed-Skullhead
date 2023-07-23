using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead.Throwable
{
    public class ThrowBone
    {
        private Texture2D texture;
        private Rectangle hitbox;
        private float speed;
        private float scale;
        public Rectangle scaledHitbox;

        public ThrowBone(Texture2D texture, float speed, Rectangle hitbox, float scale)
        {
            this.texture = texture;
            this.speed = speed;
            this.hitbox = hitbox;
            this.scale = scale;
            scaledHitbox = new Rectangle(hitbox.X, hitbox.Y, (int)(hitbox.Width * scale), (int)(hitbox.Height * scale));
        }
        public void Update()
        {
            scaledHitbox.X += (int)speed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, scaledHitbox, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
