using Mario.Controls;
using Mario.States;
using Mario.Temp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

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

        //gravitációhoz
        public Character player;

        //Platformokhoz
        List<Platform> platforms = new List<Platform>();

        //hangok
        SoundEffect effect;
        Song song;

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

            //gravitációhoz
            player = new Character(Content.Load<Texture2D>("Others/character"), new Vector2(50, 50));

            //platformokhoz
            platforms.Add(new Platform(Content.Load<Texture2D>("Others/block"), new Vector2(30, 400)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Others/block"), new Vector2(350, 300)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Others/block"), new Vector2(700, 350)));

            //hangok
            effect = Content.Load<SoundEffect>("Jump");
            song = Content.Load<Song>("Pyro");
            MediaPlayer.Play(song);

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


            MouseState mouse = Mouse.GetState();

            if (!paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    paused = true;
                    btnPlay.isClicked = false;
                }

                //pause menu
                //enemy.Update();

                //gravitációhoz
                player.Update(gameTime, effect);
            }
            else if (paused)
            {
                if (btnPlay.isClicked)
                {
                    paused = false;
                }
                if (btnQuit.isClicked)
                {
                    Exit();
                }

                btnPlay.Update(mouse);
                btnQuit.Update(mouse);

            }

            //platformhoz
            /*foreach (Platform platform in platforms)
                if (player.rectangle.isOnTopOf(platform.rectangle))
                {
                    player.velocity = 0f;
                    player.hasJumped = false;
                }
            */
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

            //gravitációhoz
            player.Draw(spriteBatch);

            //platformokhoz
            foreach (Platform platform in platforms)
                platform.Draw(spriteBatch);

            _currentState.Draw(gameTime, spriteBatch);

            if (paused)
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

//platformokhoz
static class RectangleHelper
{
    const int penetrationMargin = 5;
    public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
    {
        return (r1.Bottom >= r2.Top - penetrationMargin && 
            r1.Bottom <= r2.Top + 1 && 
            r1.Right >= r2.Left + 5 && 
            r1.Left <= r2.Right - 5);
    }
}
