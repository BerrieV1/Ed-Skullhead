using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Ed_Skullhead.Collectibles
{
    public class Bone : Collectible
    {
        public Bone(Texture2D texture, Rectangle collectibleRect) : base(texture, collectibleRect)
        {
            value = 3;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 1f);
        }
    }
}
