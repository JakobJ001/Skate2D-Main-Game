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

        //Constructor, assigns the important variables to the player, like everything it needs to move/how it looks. Basically creates the player.
        public Player(Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(50, 100);
            velocity = new Vector2(1, -5);
        }

        public void Update()
        {
            //--- These are the two vektors related to the player, and is what changes if the player moves. 
            velocity += Game1.gravity;
            position += velocity;
            int JumpTime = 1;

            //--- Space A and D are assigned to be basic movements, moving right and left and jumping
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && JumpTime < 2)
            {
                velocity.Y = -3;
                JumpTime++;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            { 
                    velocity.X = 100;

            }

            //--- S and D are assigned to rotate the character, so that in the final game you can do flips in the air for extra points.
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                    rotation += MathHelper.ToRadians(10);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                    rotation -= MathHelper.ToRadians(10);
            }

            //--- This is simply a key to return to rotation 0, but i didnt want it to just teleport into position so i used
            //-- "while" to rotate it back to 0, one radian at a time.
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                while (rotation < MathHelper.ToRadians(0))
                { rotation += MathHelper.ToRadians(1); }
                while (rotation > MathHelper.ToRadians(0))
                { rotation -= MathHelper.ToRadians(1); }
            }

            //if (rotation > MathHelper.ToRadians(360) || rotation < MathHelper.ToRadians(-360))
            //{
            //    rotation = 0;
            //}

            //--- This makes it so that the rotation in Radians will never go over 180 or under -180, this fixes a lot of
            //-- issues with the rotation and makes it easier to understand where the player is rotated 

            if (rotation > MathHelper.ToRadians(180))
            {
                rotation = MathHelper.ToRadians(-180);
            }

            if (rotation < MathHelper.ToRadians(-180))
            {
                rotation = MathHelper.ToRadians(180);
            }


            //--- This  simply creates the ground, so you dont fall forever
            if (position.Y > 800 - texture.Height && velocity.Y > 1)
            {
                velocity.Y *= -1;
                velocity.Y /= 2;
                position.Y = 799 - texture.Height;
                
                //--- This makes it so that if youre rotated a certain way, in where the player textures wheels wouldnt hit the ground when you land,
                //-- you stop and basically crash. Gives a bit more risk to rotating in the air.
                if (rotation < MathHelper.ToRadians(-90) || rotation > MathHelper.ToRadians(90)) 
                {
                    velocity.X = 0;
                    rotation = 0;
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
