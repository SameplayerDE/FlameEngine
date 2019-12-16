using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Graphics
{
    public static class TextureFlame
    {

        public static Texture2D TextureGenerator(int Width, int Height, Color Color)
        {
            Texture2D Texture = new Texture2D(Flame.GraphicsDevice, Width, Height);
            var colors = new Color[Width * Height];
            for (int i = 0; i < Width * Height; i++)
                colors[i] = Color;
            Texture.SetData<Color>(colors);
            return Texture;
        }

    }
}
