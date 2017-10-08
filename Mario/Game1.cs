using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mario
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D character;

        private State _currentState;

        private State _nextState;

        int characterX;
        int characterY;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public void GameWindow()
        {
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = "Mordecai game";

            IsMouseVisible = true;

            GameWindow();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            character = Content.Load<Texture2D>("Others/character");

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                characterX -= 10;
                
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                characterX += 10;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                characterY -= 10;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                characterY += 10;
            }

            if (characterX < 0)
            {
                characterX = 0;
            }
            else if (characterX + character.Width > graphics.PreferredBackBufferWidth)
            {
                characterX = graphics.PreferredBackBufferWidth - character.Width;
            }
            if (characterY < 0)
            {
                characterY = 0;
            }
            else if (characterY + character.Height > graphics.PreferredBackBufferHeight)
            {
                characterY = graphics.PreferredBackBufferHeight - character.Height;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _currentState.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            spriteBatch.Draw(character, new Vector2(characterX, characterY), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
