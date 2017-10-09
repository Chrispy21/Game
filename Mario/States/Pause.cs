using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Controls;
using Mario.States;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Mario.States
{
    public class Pause : State
    {
        private State _currentState;
        private List<Component> _components;

        public Pause(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base (game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var resumeGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 150),
                Text = "Resume Game",
            };
            resumeGameButton.Click += resumeGameButton_Click;

            var mainmenuGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Main Menu",
            };
            mainmenuGameButton.Click += mainmenuGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Quit Game",
            };
            quitGameButton.Click += quitGameButton_Click;

            _components = new List<Component>()
            {
                resumeGameButton,
                mainmenuGameButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch);
            }
        }

        private void resumeGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void mainmenuGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

    }
}
