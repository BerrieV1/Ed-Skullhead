using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ed_Skullhead
{
    public class Player : Entity
    {
        public Vector2 velocity;
        public Rectangle fallRect;
        public Rectangle jumpRect;

        private float speed;
        private float scale;
        private float fallSpeed;
        private float jumpSpeed;

        public bool isFalling = true;
        public bool isJumping;
        public bool isRunning = false;
        public float startY;

        public Animation[] animation;
        public CurrentAnimation currentAnimation;
        public SpriteEffects spriteEffect;

        public Player(Texture2D idleSprite, Texture2D runSprite, float speed, float scale, float fallSpeed, float jumpSpeed)
        {
            velocity = new Vector2(40, 340);
            animation = new Animation[2];
            position = new Vector2();

            animation[0] = new Animation(idleSprite, 24, 32);
            animation[1] = new Animation(runSprite, 22, 32);

            this.speed = speed;
            this.scale = scale;
            this.fallSpeed = fallSpeed;
            this.jumpSpeed = jumpSpeed;

            hitbox = new Rectangle((int)position.X, (int)position.Y, 25, 32);
            fallRect = new Rectangle((int)position.X, (int)position.Y + 25, 32, 5);
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
            if (isRunning)
            {
                currentAnimation = CurrentAnimation.Run;
                isRunning = false;
            }
            else
                currentAnimation = CurrentAnimation.Idle;

            Move(Keyboard.GetState());
            if (isFalling)
                velocity.Y += fallSpeed;

            startY = position.Y;
            Jump(Keyboard.GetState());

            position = velocity;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            fallRect.X = (int)position.X;
            fallRect.Y = (int)velocity.Y + 32;
        }
        private void Move(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Q))
            {
                velocity.X -= speed;
                isRunning = true;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (state.IsKeyDown(Keys.D))
            {
                velocity.X += speed;
                isRunning = true;
                spriteEffect = SpriteEffects.None;
            }
        }
        private void Jump(KeyboardState state)
        {
            if (isJumping)
            {
                velocity.Y += jumpSpeed;
                jumpSpeed += 1;
                if (velocity.Y >= startY)
                {
                    velocity.Y = startY;
                    isJumping = false;
                }
            }
            else
            {
                if (state.IsKeyDown(Keys.Space) && !isFalling)
                {
                    isJumping = true;
                    isFalling = false;
                    jumpSpeed = -10;
                }
            }
        }
    }
}
