using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mario;

namespace Mario.States
{
    public class GameState : State
    {
        public Mordecai player;


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            player = new Mordecai();
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }
    }
}
