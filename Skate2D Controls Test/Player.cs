using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skate2D_Controls_Test
{
    //---Hela klassen är Jakobs Del

    //NOTES:

    //02-22:
    //Vinklar i C# fungerar mellan 180 och -180, 0-180 i normalla vinklar är 0 till -180 och 180-360 är 180 till 0




    public class Player
    {
        public Texture2D texture;
        public Vector2 position, velocity;
        float rotation;

        //Konstruktor
        public Player(Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(50, 100);
            velocity = new Vector2(1, -5);
        }

        public void Update()
        {
            //--- These are the 
            velocity += Game1.gravity;
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                velocity.Y = -3;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                    rotation += MathHelper.ToRadians(5);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                    rotation -= MathHelper.ToRadians(5);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                while (rotation < MathHelper.ToRadians(0))
                { rotation += MathHelper.ToRadians(1); }
                while (rotation > MathHelper.ToRadians(0))
                { rotation -= MathHelper.ToRadians(1); }
            }

            if (rotation > MathHelper.ToRadians(360) || rotation < MathHelper.ToRadians(-360))
            {
                rotation = 0;
            }

            if (rotation > MathHelper.ToRadians(180))
            {
                rotation = MathHelper.ToRadians(-180);
            }

            if (rotation < MathHelper.ToRadians(-180))
            {
                rotation = MathHelper.ToRadians(180);
            }


            if (position.Y > 800 - texture.Height && velocity.Y > 1)
            {
                velocity.Y *= -1;
                velocity.Y /= 2;
                position.Y = 799 - texture.Height;

                if (rotation < MathHelper.ToRadians(-90) || rotation > MathHelper.ToRadians(90)) 
                {
                    velocity.X = 0;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            // spriteBatch.Draw(texture,position,Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 1);

        }
    }
}
