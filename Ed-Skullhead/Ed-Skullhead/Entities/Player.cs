using Ed_Skullhead.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Ed_Skullhead.src
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
        public bool isThrowing = false;
        public float startY;

        public Animation[] animation;
        public CurrentAnimation currentAnimation;
        public SpriteEffects spriteEffect;

        public Player(Vector2 startPositon, List<Texture2D> sprites, float speed, float scale, float fallSpeed, float jumpSpeed)
        {
            velocity = startPositon;
            animation = new Animation[2];

            animation[0] = new Animation(sprites[0], 24, 32);
            animation[1] = new Animation(sprites[1], 22, 32);

            this.speed = speed;
            this.scale = scale;
            this.fallSpeed = fallSpeed;
            this.jumpSpeed = jumpSpeed;

            hitbox = new Rectangle((int)velocity.X, (int)velocity.Y, 24, 32);
            fallRect = new Rectangle((int)velocity.X, (int)velocity.Y + 24, 32, 7);
            jumpRect = new Rectangle((int)velocity.X, (int)velocity.Y, 32, 1);
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
            position = velocity;
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

            isThrowing = Keyboard.GetState().IsKeyDown(Keys.E);

            startY = position.Y;
            Jump(Keyboard.GetState());

            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            fallRect.X = (int)position.X;
            fallRect.Y = (int)velocity.Y + 32;
            jumpRect.X = (int)position.X;
            jumpRect.Y = (int)velocity.Y;
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
                    SoundManager.PlaySound("jump");
                    isJumping = true;
                    isFalling = false;
                    jumpSpeed = -10;
                }
            }
        }
    }
}
