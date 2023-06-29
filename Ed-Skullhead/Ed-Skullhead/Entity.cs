﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead
{
    public abstract class Entity
    {
        public enum CurrentAnimation
        {
            Idle,
            Run
        }
        public Texture2D texture;
        public Vector2 position;
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Update();
    }
}
