using Microsoft.Xna.Framework.Graphics;


namespace Skate2D_Controls_Test
{
    //---Hela klassen är Jakobs Del

    public abstract class BaseObjects
    {
        public Texture2D texture;
        protected int layer;
        protected int ground;

        public BaseObjects(int height)
        {
            DefineGround(height);
        }

        protected void DefineGround(int height)
        {
            //--- This  simply creates the ground, so you dont fall forever
            ground = 800 - height;
        }
    }
}
