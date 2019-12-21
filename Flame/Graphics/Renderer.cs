using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Flame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Components;

namespace Flame.Graphics
{
    public class Renderer
    {

        private SpriteBatch SpriteBatch { get; set; }
        private Texture2D Pixel = null;
        public SpriteFont Font { get; set; }
        public Camera Camera { get; set; } = null;
        public RenderTarget2D RenderTarget { get; private set; }
        public Vector2 RenderTargetPosition = Vector2.Zero;


        public bool IsRenderingToRenderTarget { get; set; } = false;

        public Renderer()
        {
            SpriteBatch = new SpriteBatch(Flame.GraphicsDevice);
            Camera = new Camera(Flame.GraphicsDevice.Viewport);
            Pixel = TextureFlame.TextureGenerator(1, 1, Color.White);
            RenderTarget = new RenderTarget2D(
                Flame.GraphicsDevice,
                Flame.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Flame.GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                Flame.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        public void Begin(SamplerState samplerState)
        {
            if (IsRenderingToRenderTarget)
            {
                Flame.GraphicsDevice.SetRenderTarget(RenderTarget);
                Flame.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                Flame.GraphicsDevice.Clear(Color.Transparent);
            }

            if (Camera != null)
                SpriteBatch.Begin(samplerState: samplerState, transformMatrix: Camera.Transform);
            else
                SpriteBatch.Begin(samplerState: samplerState);
        }

        public void Begin (SamplerState SamplerState, Camera Camera)
        {
            if (IsRenderingToRenderTarget)
            {
                RenderTargetPosition = Camera.Transform.Translation.ToVector2();
                Flame.GraphicsDevice.SetRenderTarget(RenderTarget);
                Flame.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                Flame.GraphicsDevice.Clear(Color.Transparent);
            }

            SpriteBatch.Begin(samplerState: SamplerState);
        }

        public void Begin(Camera Camera)
        {
            if (IsRenderingToRenderTarget)
            {
                RenderTargetPosition = -Camera.Transform.Translation.ToVector2();
                Flame.GraphicsDevice.SetRenderTarget(RenderTarget);
                Flame.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                Flame.GraphicsDevice.Clear(Color.Transparent);
            }

            SpriteBatch.Begin();
        }

        public void Begin()
        {
            if (IsRenderingToRenderTarget)
            {
                Flame.GraphicsDevice.SetRenderTarget(RenderTarget);
                Flame.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                Flame.GraphicsDevice.Clear(Color.Transparent);
            }

            if (Camera != null)
                SpriteBatch.Begin(transformMatrix: Camera.Transform);
            else
                SpriteBatch.Begin();
        }

        public void End()
        {
            SpriteBatch.End();
            if (IsRenderingToRenderTarget)
            {
                Flame.GraphicsDevice.SetRenderTarget(null);
            }
        }

        public void DrawText(string Text, int X, int Y)
        {
            if (Font != null)
            {
                SpriteBatch.DrawString(Font, Text, new Vector2(X, Y), Color.White);
            }
        }

        public void DrawScene(Scene Scene, Tileset Tileset)
        {
            SpriteBatch.Draw(Pixel, new Rectangle(Point.Zero, (Scene.Dimensions.ToVector2() * Tileset.TileSize.ToVector2()).ToPoint()), Scene.BackgroundColor);
            foreach (Layer Layer in Scene.Layers)
            {
                Draw2DTileArrayByID(Tileset, 0, 0, Layer.Tiles);
            }
        }

        /**    
               public void DrawModel(Model Model, Vector3 Position, Camera3D Camera)
               {
                   foreach (ModelMesh Mesh in Model.Meshes)
                   {
                       foreach (BasicEffect Effect in Mesh.Effects)
                       {
                           Effect.World = Matrix.Identity * Matrix.CreateTranslation(Position);
                           Effect.View = Camera.View;
                           Effect.Projection = Camera.Projection;
                           Effect.EnableDefaultLighting();
                           Effect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1); // a red light
                           Effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                           Effect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1); // with green highlights
                       }
                       Mesh.Draw();
                   }
               }

               public void DrawModel(Model Model, Vector3 Position, Effect Effect, Camera3D Camera)
               {
                   foreach (ModelMesh Mesh in Model.Meshes)
                   {
                       foreach (EffectPass Pass in Effect.CurrentTechnique.Passes)
                       {
                           Effect.Parameters["WorldViewProjection"].SetValue(Matrix.Identity * Matrix.CreateTranslation(Position) * Camera.View * Camera.Projection);
                           Effect.Parameters["World"].SetValue(Matrix.Identity * Matrix.CreateTranslation(Position));
                           Effect.Parameters["View"].SetValue(Camera.View);
                           Effect.Parameters["Projection"].SetValue(Camera.Projection);
                           Pass.Apply();
                       }
                       Mesh.Draw();
                   }
               }
       **/


        public void DrawText(SpriteFont Font, string Text, int X, int Y)
        {
            SpriteBatch.DrawString(Font, Text, new Vector2(X, Y), Color.White);
        }

        public void DrawTexture(Texture2D Texture, int X, int Y)
        {
            if (X > ((-Camera.Transform.Translation.X - (Texture.Width * Camera.Scale)) / Camera.Scale))
            {
                if (Y > ((-Camera.Transform.Translation.Y - (Texture.Height * Camera.Scale)) / Camera.Scale))
                {
                    if (X < (Camera.Viewport.Width + (-Camera.Transform.Translation.X + (Texture.Width * Camera.Scale)) / Camera.Scale))
                    {
                        if (Y < (Camera.Viewport.Height + (-Camera.Transform.Translation.Y + (Texture.Height * Camera.Scale)) / Camera.Scale))
                        {
                            SpriteBatch.Draw(Texture, new Vector2(X, Y), Color.White);
                        }
                    }
                }
            }

        }

        public void DrawTexture(Texture2D Texture, int X, int Y, int Width, int Height)
        {
            if (X > ((-Camera.Transform.Translation.X - (Width * Camera.Scale)) / Camera.Scale))
            {
                if (Y > ((-Camera.Transform.Translation.Y - (Height * Camera.Scale)) / Camera.Scale))
                {
                    if (X < (Camera.Viewport.Width + (-Camera.Transform.Translation.X + (Width * Camera.Scale)) / Camera.Scale))
                    {
                        if (Y < (Camera.Viewport.Height + (-Camera.Transform.Translation.Y + (Height * Camera.Scale)) / Camera.Scale))
                        {
                            SpriteBatch.Draw(Texture, new Rectangle(X, Y, Width, Height), Color.White);
                        }
                    }
                }
            }
        }

        public void DrawTexture(Texture2D Texture, int X, int Y, Viewport Viewport)
        {
            if (X > ((-Camera.Transform.Translation.X - (Viewport.Width * Camera.Scale)) / Camera.Scale))
            {
                if (Y > ((-Camera.Transform.Translation.Y - (Viewport.Height * Camera.Scale)) / Camera.Scale))
                {
                    if (X < (Camera.Viewport.Width + (-Camera.Transform.Translation.X + (Viewport.Width * Camera.Scale)) / Camera.Scale))
                    {
                        if (Y < (Camera.Viewport.Height + (-Camera.Transform.Translation.Y + (Viewport.Height * Camera.Scale)) / Camera.Scale))
                        {
                            SpriteBatch.Draw(Texture, new Rectangle(X, Y, Viewport.Width, Viewport.Height), Color.White);
                        }
                    }
                }
            }
        }

        public void DrawTextureCutOut(Texture2D Texture, int X, int Y, Rectangle CutOut)
        {
            SpriteBatch.Draw(Texture, new Rectangle(X, Y, Texture.Width, Texture.Height), CutOut, Color.White);
        }

        public void Draw2DTileArrayByID(Tileset Tileset, int X, int Y, int[,] Tiles)
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (Tiles[x, y] == -1)
                    {
                        continue;
                    }
                    if (X + (x * Tileset.TileSize.X) > ((-Camera.Transform.Translation.X - (Tileset.TileSize.X * Camera.Scale)) / Camera.Scale))
                    {
                        if (Y + (y * Tileset.TileSize.Y) > ((-Camera.Transform.Translation.Y - (Tileset.TileSize.Y * Camera.Scale)) / Camera.Scale))
                        {
                            if (X + (x * Tileset.TileSize.X) < (Camera.Viewport.Width + (-Camera.Transform.Translation.X + (Tileset.TileSize.X * Camera.Scale)) / Camera.Scale))
                            {
                                if (Y + (y * Tileset.TileSize.Y) < (Camera.Viewport.Height + (-Camera.Transform.Translation.Y + (Tileset.TileSize.Y * Camera.Scale)) / Camera.Scale))
                                {
                                    DrawTexture(Tileset.GetTileByID(Tiles[x, y]).Texture, X + (x * Tileset.TileSize.X), Y + (y * Tileset.TileSize.Y));
                                }
                            }
                        }
                    }

                }
            }
        }

        /**       public void DrawCircle(int X, int Y, int Radius, int Quality, Color Color, float Alpha)
               {
                   Circle(new Vector2(X, Y), Radius, Color * Alpha, Quality);
               }

               public void DrawCircle(int X, int Y, int Radius, int Quality, Color Color)
               {
                   Circle(new Vector2(X, Y), Radius, Color * 1f, Quality);
               }

               public void DrawCircle(int X, int Y, int Radius, float Width, int Quality, Color Color, float Alpha)
               {
                   Circle(new Vector2(X, Y), Radius, Color * Alpha, Width, Quality);
               }

               public void DrawCircle(int X, int Y, int Radius, float Width, int Quality, Color Color)
               {
                   Circle(new Vector2(X, Y), Radius, Color * 1f, Width, Quality);
               }

               private void Circle(Vector2 position, float radius, Color color, float thickness, int resolution)
               {
                   Vector2 last = Vector2.UnitX * radius;
                   Vector2 lastP = last.Perpendicular();
                   for (int i = 1; i <= resolution; i++)
                   {
                       Vector2 at = FlameMath.AngleToVector(i * MathHelper.PiOver2 / resolution, radius);
                       Vector2 atP = at.Perpendicular();

                       Line(position + last, position + at, color, thickness);
                       Line(position - last, position - at, color, thickness);
                       Line(position + lastP, position + atP, color, thickness);
                       Line(position - lastP, position - atP, color, thickness);

                       last = at;
                       lastP = atP;
                   }
               }

               private void Circle(Vector2 position, float radius, Color color, int resolution)
               {
                   Vector2 last = Vector2.UnitX * radius;
                   Vector2 lastP = last.Perpendicular();
                   for (int i = 1; i <= resolution; i++)
                   {
                       Vector2 at = FlameMath.AngleToVector(i * MathHelper.PiOver2 / resolution, radius);
                       Vector2 atP = at.Perpendicular();

                       Line(position + last, position + at, color);
                       Line(position - last, position - at, color);
                       Line(position + lastP, position + atP, color);
                       Line(position - lastP, position - atP, color);

                       last = at;
                       lastP = atP;
                   }
               }

               private void Line(Vector2 start, Vector2 end, Color color)
               {
                   LineAngle(start, FlameMath.Angle(start, end), Vector2.Distance(start, end), color);
               }

               private void Line(Vector2 start, Vector2 end, Color color, float thickness)
               {
                   LineAngle(start, FlameMath.Angle(start, end), Vector2.Distance(start, end), color, thickness);
               }

               private void LineAngle(Vector2 start, float angle, float length, Color color, float thickness)
               {
                   SpriteBatch.Draw(Pixel, start, Pixel.Bounds, color, angle, new Vector2(0, .5f), new Vector2(length, thickness), SpriteEffects.None, 0);
               }

               private void LineAngle(Vector2 start, float angle, float length, Color color)
               {
                   SpriteBatch.Draw(Pixel, start, Pixel.Bounds, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
               }

           }
           **/
    }
}
