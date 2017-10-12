using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Temp
{
    public class Character
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        bool hasJumped;

        /*
        public static Texture2D character;
        public static Texture2D mordecai_down;
        public static Texture2D mordecai_jump;
        public static Texture2D character_right;
        private int direction;
        */

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
        }

        public void Update(GameTime gameTime, SoundEffect effect)
        {
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) velocity.X = 10f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) velocity.X = -10;
            else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 10f;
                //-8f: az ugrás magassága
                velocity.Y = -8f;
                hasJumped = true;
                effect.Play();
            }

            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }

            if (position.Y + texture.Height >= 580)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            //Mordecai.cs
            /*KeyboardState keyboardState = Keyboard.GetState();
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
            */
            //-------
        }
    }
}
