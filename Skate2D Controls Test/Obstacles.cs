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
    public class Obstacles : BaseObjects
    {
        //---Hela klassen är Jakobs Del
        public Vector2 position;
        public int boxX;
        

        public Obstacles(Texture2D texture, int layer, int boxX) : base(280)
        {
                this.texture = texture;
                this.boxX = boxX;
                this.layer = layer;
                position = new Vector2(boxX, ground - layer * 40);
            }
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
            public void Update()
            {
            Console.WriteLine(position.Y);
            }

            public void Draw(SpriteBatch obstacleSpriteBatch)
            {
                obstacleSpriteBatch.Draw(texture,position,Color.White);
            }

    }

}

