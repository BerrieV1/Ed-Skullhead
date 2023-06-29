using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace Ed_Skullhead
{
    public class Player : Entity
    {
        public Vector2 velocity;
        public float speed = 5;
        private float scale = 2f;
        public Animation[] animation;
        public CurrentAnimation currentAnimation;
        public SpriteEffects spriteEffect;
        public Player(Texture2D idleSprite, Texture2D runSprite)
        {
            velocity = new Vector2();
            animation = new Animation[2];
            animation[0] = new Animation(idleSprite, 24, 32);
            animation[1] = new Animation(runSprite, 22, 32);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (currentAnimation)
            {
                case CurrentAnimation.Idle:
                    animation[0].Draw(spriteBatch, position, gameTime, scale, spriteEffect);
                    break;
                case CurrentAnimation.Run:
                    animation[1].Draw(spriteBatch, position, gameTime, scale, spriteEffect);
                    break;
            }
        }
        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            currentAnimation = CurrentAnimation.Idle;

            if (state.IsKeyDown(Keys.Q))
            {
                velocity.X -= speed;
                currentAnimation = CurrentAnimation.Run;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if(state.IsKeyDown(Keys.D))
            {
                velocity.X += speed;
                currentAnimation = CurrentAnimation.Run;
                spriteEffect = SpriteEffects.None;
            }
            position = velocity;
        }
    }
}
