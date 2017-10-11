using Mario.Controls;
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
        internal GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch; 
        Texture2D[] groundTextures = new Texture2D[10];
        internal static Game1 Instance { get; private set; }
        private State _currentState;        
        private State _nextState;

        //Pause
        bool paused = false;
        Texture2D pausedTexture;
        Rectangle pausedRectangle;
        ButtonPause btnPlay, btnQuit;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public void GameWindow()
        {
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 680;
            graphics.ApplyChanges();
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
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

            Mordecai.character = Content.Load<Texture2D>("Others/character");
            Mordecai.mordecai_down = Content.Load<Texture2D>("Others/mordecai_down");
            Mordecai.mordecai_jump = Content.Load<Texture2D>("Others/mordecai_jump");
            Mordecai.character_right = Content.Load<Texture2D>("Others/character_right");
            
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            IsMouseVisible = true;

            //PAUSE
            pausedTexture = Content.Load<Texture2D>("Others/pause");
            pausedRectangle = new Rectangle(0, 0, pausedTexture.Width, pausedTexture.Height);
            btnPlay = new ButtonPause();
            btnPlay.Load(Content.Load<Texture2D>("Controls/btnNG"), new Vector2(465, 260));
            btnQuit = new ButtonPause();
            btnQuit.Load(Content.Load<Texture2D>("Controls/btnExit"), new Vector2(465, 360));

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

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

            MouseState mouse = Mouse.GetState();

            if(!paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    paused = true;
                    btnPlay.isClicked = false;
                }

                //enemy.Update();
                //State.Update();
            }
            else if (paused)
            {
                if(btnPlay.isClicked)
                {
                    paused = false;
                }
                if(btnQuit.isClicked)
                {
                    Exit();
                }

                btnPlay.Update(mouse);
                btnQuit.Update(mouse);

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
 
            spriteBatch.Begin();

            _currentState.Draw(gameTime, spriteBatch);

            if(paused)
            {
                spriteBatch.Draw(pausedTexture, pausedRectangle, Color.White);
                btnPlay.Draw(spriteBatch);
                btnQuit.Draw(spriteBatch);
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
