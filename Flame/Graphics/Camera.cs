using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Flame.Utils;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Graphics
{
    public class Camera
    {

        public Vector2 Focus = Vector2.Zero;
        public Matrix Transform { get; private set; }
        public Viewport Viewport { get; set; }
        public float Rotation = 0f;
        public float Scale { get; set; } = 1f;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
            SetFocus(Viewport.Bounds.Center);
        }

        public void MoveFocus(Point target)
        {
            var position = Matrix.CreateTranslation(Focus.X + target.X, Focus.Y + target.Y, 0);
            var offset = Matrix.CreateTranslation(Viewport.Width / 2, Viewport.Height / 2, 0);

            Transform = position * Matrix.CreateScale(Scale) * offset;
        }

        public void SetFocus(Point target)
        {
            var position = Matrix.CreateTranslation(-target.X, -target.Y, 0);
            Focus = position.Translation.ToVector2();
            var offset = Matrix.CreateTranslation(Viewport.Width / 2, Viewport.Height / 2, 0);

            Transform = position * Matrix.CreateScale(Scale) * offset;
        }

    }

}
