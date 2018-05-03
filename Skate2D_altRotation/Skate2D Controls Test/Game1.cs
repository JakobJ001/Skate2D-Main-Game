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

            //---This is simply gravity, makes it so everything that has velocity is slowly pulled downwards --Jakobs Del
            gravity = new Vector2(0, 0.4f);

            currentGameState = GameStates.StartScreen;

            //---These are the variables that are used for the background sprite --Simons Del
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            //--- spriteBatch is loaded here to draw the player and the background, the camera and the
            // actuall player and all its controls are loaded aswell --Jakobs Del

            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera();

            Texture2D playerSprite = Content.Load<Texture2D>("flappybird");

            backgroundSprite = Content.Load<Texture2D>("background");

            player = new Player(playerSprite);

            //-----------------------------------------------------------------------------------------

        }


        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {

            //--- Makes it so pressing escape closes the game, this wouldnt be in the final product it just makes it easier when coding --Jakobs Del
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //--- camera.Follow calls on the Camera object to use its Follow method, which simply makes camera follow whatever you tell it to --Simons Del
            camera.Follow(player);

            //--- Uses the Player objects update method, which checks for button presses and does what they are bound to, like move forward and jump --Jakobs Del
            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            //--Starts Sprite Batch to draw everything thats needed, like the background wallpaper and the player sprite --Jakobs Del
                spriteBatch.Begin(transformMatrix: camera.Transform);
            spriteBatch.Draw(backgroundSprite, new Vector2(-500,-100), Color.White);
            for(int i = 1; i < 10000;)
            {
                spriteBatch.Draw(backgroundSprite, new Vector2(-500+backgroundSprite.Width*i, -100), Color.White);
                i++;
            }
            player.Draw(spriteBatch);
                spriteBatch.End();

                base.Draw(gameTime);

        }
    }
}
