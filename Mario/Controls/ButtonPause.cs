using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Controls
{
    public class ButtonPause
    {
        Texture2D texture_pause;
        Vector2 position_pause;
        Rectangle rectangle_pause;

        Color colour = new Color(255, 255, 255, 255);

        bool down;
        public bool isClicked;

        public ButtonPause()
        {

        }

        public void Load(Texture2D newTexture, Vector2 newPosition)
        {
            texture_pause = newTexture;
            position_pause = newPosition;
        }

        public void Update(MouseState mouse)
        {
            mouse = Mouse.GetState();

            rectangle_pause = new Rectangle((int)position_pause.X, (int)position_pause.Y, texture_pause.Width, texture_pause.Height);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if(mouseRectangle.Intersects(rectangle_pause))
            {
                if (colour.A == 255)
                {
                    down = false;
                }
                if (colour.A == 0)
                {
                    down = true;
                }
                if (down)
                {
                    colour.A += 3;
                }
                else
                {
                    colour.A -= 3;
                }
                if(mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    colour.A = 255;
                }
            }
            else if(colour.A < 255)
            {
                colour.A += 3;
            }
        }

        public void Draw(SpriteBatch spriteBatch_pause)
        {
            spriteBatch_pause.Draw(texture_pause, rectangle_pause, colour);
        }
    }
}
