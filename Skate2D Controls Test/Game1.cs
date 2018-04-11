using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Skate2D_Controls_Test
{

    enum GameStates { StartScreen, Ingame }

    public class Game1 : Game
    {
        Player player;

        Camera camera;

        public static int ScreenHeight;
        public static int ScreenWidth;

        GameStates currentGameState;

        public static Vector2 gravity;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D backgroundSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();

            gravity = new Vector2(0, 0.4f);

            currentGameState = GameStates.StartScreen;

            ScreenHeight = graphics.PreferredBackBufferHeight;

            ScreenWidth = graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera();

            Texture2D playerSprite = Content.Load<Texture2D>("flappybird");

            backgroundSprite = Content.Load<Texture2D>("background");

            player = new Player(playerSprite);

        }


        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.Follow(player);

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

                spriteBatch.Begin(transformMatrix: camera.Transform);
                player.Draw(spriteBatch);
                spriteBatch.Draw(backgroundSprite, new Vector2(0), Color.White);
                spriteBatch.End();

                base.Draw(gameTime);

        }
    }
}
