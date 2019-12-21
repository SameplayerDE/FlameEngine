using Flame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Utils
{
    public static class FlameUtils
    {

        public static Vector2 ToVector2(this Vector3 Vector)
        {
            return new Vector2(Vector.X, Vector.Y);
        }

        public static void Begin(this SpriteBatch SpriteBatch, Camera Camera)
        {
            SpriteBatch.Begin(transformMatrix: Camera.Transform);
        }

        public static void Begin(this SpriteBatch SpriteBatch, SamplerState SamplerState, Camera Camera)
        {
            SpriteBatch.Begin(samplerState: SamplerState, transformMatrix: Camera.Transform);
        }

        public static void Draw(this SpriteBatch SpriteBatch, Renderer Renderer)
        {
            if (Renderer.IsRenderingToRenderTarget)
            {
                SpriteBatch.Draw(Renderer.RenderTarget, Renderer.RenderTargetPosition, Color.White);
            }
        }

    }
}
