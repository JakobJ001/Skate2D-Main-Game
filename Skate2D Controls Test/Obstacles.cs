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
    public class Obstacles
    {
            public Texture2D boxTexture;
            public Vector2 position;
            
                    
            public Obstacles(Texture2D boxTexture)
            {
                this.boxTexture = boxTexture;
                position = new Vector2(500, 480);
            }

            public void Update()
            {

            }

            public void Draw(SpriteBatch obstacleSpriteBatch)
            {
                obstacleSpriteBatch.Draw(boxTexture,position,Color.White);
            }

    }

}

