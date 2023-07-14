using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead
{
    public class Animation
    {
        Texture2D spritesheet;
        int frames;
        int rows = 0;
        int c = 0;
        float lastFrame = 0;
        public int frameWidth;
        public int frameHeight;
        public Animation(Texture2D spritesheet, int frameWidth, int frameHeight)
        {
            this.spritesheet = spritesheet;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            frames = spritesheet.Width / (int)frameWidth;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, float scale, SpriteEffects spriteEffects, float time = 100)
        {
            if (c < frames)
            {
                var rectangle = new Rectangle(frameWidth * c, rows, frameWidth, frameHeight);
                spriteBatch.Draw(spritesheet, position, rectangle, Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0f);
                lastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (lastFrame > time)
                {
                    lastFrame -= time;
                    c++;
                    if (c == frames)
                    {
                        c = 0;
                    }
                }
            }
            else
            {
                c = 0;
            }
        }
    }
}
