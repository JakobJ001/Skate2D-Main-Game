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
    //---Nästab hela klassen är Jakobs Del

    //NOTES:

    //02-22:
    //Vinklar i C# fungerar mellan 180 och -180, 0-180 i normalla vinklar är 0 till -180 och 180-360 är 180 till 0




    public class Player : BaseObjects
    {
        public Vector2 position, velocity, acceleration, friction, center;
        float rotation;
        KeyboardState nowButton, lastButton;

        int playerGround;

        float accelerationfloat = 0.1F;
        float frictionfloat = 0;
        float maxspeed = 20;
        float startspeed = 0;
        float restSpeed = 0;

        //Constructor, assigns the important variables to the player, like everything it needs to move/how it looks. Basically creates the player.
        public Player(Texture2D texture) :base(183)
        {
            this.texture = texture;
            position = new Vector2(50, 100);
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            friction = new Vector2(0, 0);
            frictionfloat = accelerationfloat / 2;
            restSpeed = accelerationfloat;

            center = new Vector2(texture.Width / 2, texture.Height / 2);

        }

        public void Update()
        {
            //
            nowButton = Keyboard.GetState();
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

            // --- ARVIDS DEL ---

            //så man kan styra höger/vänster, igenom att ändra på acceleration
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                acceleration.X = accelerationfloat;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                acceleration.X = -accelerationfloat;
            }
            //sätter acceleration till 0 om ingen knapp är nedtryckt
            else
            {
                acceleration.X = 0;
            }

            //Spelaren får hastighet igenom att addera velocity med acceleration
            velocity += acceleration;

            //Om velocity råkar vara mindre än restspeed så stängs all hastighet och friktion av. Detta är för att inte få den att skaka när den står stil
            if (System.Math.Abs(velocity.X) < restSpeed)
            {
                velocity.X = 0;
                friction.X = 0;
            }
            else
            {
                //Skapar friktion mot spelaren. Helt enkelt kraft som alltid åker mot spelarens håll.
                if (velocity.X > 0)
                {
                    friction.X = -frictionfloat;
                }
                else if (velocity.X < 0)
                {
                    friction.X = frictionfloat;
                }
            }

            //Spelaren får friktion
            velocity += friction;

            //Om hastigheten råkar gå över maxhastigheten, så sätts den tillbaks till maxhastigheten. Detta fixar en massa buggar
            if (velocity.X > maxspeed)
            {
                velocity.X = maxspeed;
            }

            //Spelaren rör på sig och ändrar position
            position += velocity;

            // --- END ---

            //--- E and Q are assigned to rotate the character, so that in the final game you can do flips in the air for extra points.
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                    rotation += MathHelper.ToRadians(10);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                    rotation -= MathHelper.ToRadians(10);
            }

            //--- W and S are assigned to change the layer that the character is on so that he can avoid obstacles.
            if (Keyboard.GetState().IsKeyDown(Keys.W) && layer != 3 && lastButton != nowButton)
            {
                layer += 1;
                position.Y -= 40;

                //if (position.Y > 800 - texture.Height + 4 * 40)
                //{
                //    position.Y -= 800 - texture.Height + layer + 1 *40;
                //}
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && layer != 0 && lastButton != nowButton)
            {
                layer -= 1;
                position.Y += 40;

                //if (position.Y > 800 - texture.Height + 4 * 40)
                //{
                //    position.Y = 800 - texture.Height - layer * 40 ;
                //}
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

            playerGround = ground - layer * 40;

            if (position.Y > playerGround && velocity.Y > 1)
            {
                velocity.Y *= -1;
                velocity.Y /= 2;
                position.Y = playerGround - 1;
                
                //--- This makes it so that if youre rotated a certain way, in where the player textures wheels wouldnt hit the ground when you land,
                //-- you stop and basically crash. Gives a bit more risk to rotating in the air.
                if (rotation < MathHelper.ToRadians(-90) && velocity.X != 0 || rotation > MathHelper.ToRadians(90) && velocity.X != 0) 
                {
                    velocity.X *= 0.70F;

                }
            }

            lastButton = nowButton;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            // spriteBatch.Draw(texture,position,Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 1);

        }
    }
}
