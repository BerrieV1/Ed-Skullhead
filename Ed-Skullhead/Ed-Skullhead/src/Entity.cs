using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead.src
{
    public abstract class Entity
    {
        public enum CurrentAnimation
        {
            Idle,
            Run,
            Die
        }
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Update();
    }
}
