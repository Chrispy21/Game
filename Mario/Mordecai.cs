using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mario
{
    public class Mordecai
    {
        public static Texture2D character;
        public static Texture2D mordecai_down;
        public static Texture2D mordecai_jump;
        public static Texture2D character_right;
        private int direction;
        public Vector2 position;

        public Mordecai()
        {
            this.position = new Vector2(0, 0);
            this.direction = -1;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                spriteBatch.Draw(mordecai_down, new Vector2(position.X, position.Y), Color.White);

            }

            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                spriteBatch.Draw(mordecai_jump, new Vector2(position.X, position.Y), Color.White);

            }

            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                spriteBatch.Draw(character_right, new Vector2(position.X, position.Y), Color.White);
                direction = 1;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                spriteBatch.Draw(character, new Vector2(position.X, position.Y), Color.White);
                direction = -1;
            }
            else
            {
                if (direction == 1)
                {
                    spriteBatch.Draw(character_right, new Vector2(position.X, position.Y), Color.White);
                }
                else
                {
                    spriteBatch.Draw(character, new Vector2(position.X, position.Y), Color.White);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -= 10;

            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += 10;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                position.Y -= 10;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += 10;
            }

            if (position.X < 0)
            {
                position.X = 0;
            }
            else if (position.X + character.Width > Game1.Instance.graphics.PreferredBackBufferWidth)
            {
                position.X = Game1.Instance.graphics.PreferredBackBufferWidth - character.Width;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y + character.Height > Game1.Instance.graphics.PreferredBackBufferHeight)
            {
                position.Y = Game1.Instance.graphics.PreferredBackBufferHeight - character.Height;
            }

        }
    }
}
