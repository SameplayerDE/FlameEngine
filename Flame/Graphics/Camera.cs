using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public void SetFocus(Point target)
        {
            var position = Matrix.CreateTranslation(-target.X, -target.Y, 0);
            var offset = Matrix.CreateTranslation(Viewport.Width / 2, Viewport.Height / 2, 0);

            Transform = position * Matrix.CreateScale(Scale) * offset;
        }

    }

    public class Camera3D
    {
        public 
            Vector3 Position = new Vector3(5, 20, 0),
            Focus = Vector3.Zero,
            Rotation = Vector3.Zero,
            Velocity = Vector3.Zero;

        protected float FOV = 70f;
        protected float RotationSpeed = 0.005f;
        protected float MovementSpeed = 0.1f;

        protected int MaxMinSpeed = 80;

        public bool inGame = false;

        public Matrix View;
        public Matrix Projection;

        private KeyboardState previouseState;
        private MouseState previouseMouseState;
        private KeyboardState currentState;
        private MouseState currentMouseState;

        protected GraphicsDevice device;
        protected Vector2 ScreenCenter;

        public Camera3D()
        {
            this.device = Flame.GraphicsDevice;
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), device.Viewport.AspectRatio, 0.001f, 1000f);

            View = Matrix.CreateLookAt(Position, Focus, Vector3.Up);

            ScreenCenter = new Vector2(device.PresentationParameters.BackBufferWidth / 2, device.PresentationParameters.BackBufferHeight / 2);
        }

        public void Update(GameTime gameTime)
        {

           
            currentState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            //

            Position += Velocity;

            if (inGame)
            {
                Mouse.SetPosition(device.PresentationParameters.BackBufferWidth / 2, device.PresentationParameters.BackBufferHeight / 2);
                Vector2 MouseCenterDiff = Vector2.Subtract(currentMouseState.Position.ToVector2(), ScreenCenter);

                if (MouseCenterDiff.X < 0)
                {
                    Rotation.Y -= Math.Max(MouseCenterDiff.X, -MaxMinSpeed) * RotationSpeed;
                    if (Rotation.Y > MathHelper.ToRadians(360))
                    {
                        Rotation.Y -= MathHelper.ToRadians(360);
                    }
                }

                if (MouseCenterDiff.X > 0)
                {
                    Rotation.Y -= Math.Min(MouseCenterDiff.X, MaxMinSpeed) * RotationSpeed;
                    if (Rotation.Y < MathHelper.ToRadians(0))
                    {
                        Rotation.Y += MathHelper.ToRadians(360);
                    }
                }

                if (MouseCenterDiff.Y < 0)
                {
                    Rotation.X += Math.Max(MouseCenterDiff.Y, -MaxMinSpeed) * RotationSpeed;
                    Rotation.X = Math.Max(Rotation.X, MathHelper.ToRadians(-89.9f));
                }

                if (MouseCenterDiff.Y > 0)
                {
                    Rotation.X += Math.Min(MouseCenterDiff.Y, MaxMinSpeed) * RotationSpeed;
                    Rotation.X = Math.Min(Rotation.X, MathHelper.ToRadians(89.9f));
                }

            }

            if (isKeyPressed(Keys.Escape))
            {
                inGame = !inGame;
            }

            if (isKeyDown(Keys.OemPlus))
            {
                FOV += 2f;
                FOV = Math.Min(FOV, 90);
            }

            if (isKeyDown(Keys.OemMinus))
            {
                FOV -= 2f;
                FOV = Math.Max(FOV, 10);
            }

            if (isKeyDown(Keys.W))
            {
                Move(Vector3.Backward * MovementSpeed);
            }

            if (isKeyDown(Keys.D))
            {
                Move(Vector3.Left * MovementSpeed);
            }

            if (isKeyDown(Keys.S))
            {
                Move(Vector3.Forward * MovementSpeed);
            }

            if (isKeyDown(Keys.A))
            {
                Move(Vector3.Right * MovementSpeed);
            }

            if (isKeyDown(Keys.Space))
            {
                Move(Vector3.Up * MovementSpeed);
            }

            if (isKeyDown(Keys.LeftShift))
            {
                Move(Vector3.Down * MovementSpeed);
            }

            //


            previouseState = currentState;
            previouseMouseState = currentMouseState;


            UpdateFocus();

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), device.Viewport.AspectRatio, 0.001f, 1000f);
            
        }

        public void Move(Vector3 amount)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(Rotation.Y);
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotationMatrix);
            Position += movement;
        }

        protected void UpdateFocus()
        {
            Matrix rotationMatrix = Matrix.CreateRotationX(Rotation.X) * Matrix.CreateRotationY(Rotation.Y);
            Vector3 focusOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
            Focus = Position + focusOffset;
            //Focus = Position;
            View = Matrix.CreateLookAt(Position, Focus, Vector3.Up);
        }

        private bool isKeyPressed(Keys key)
        {

            return (currentState.IsKeyDown(key) && previouseState.IsKeyUp(key));

        }

        private bool isKeyDown(Keys key)
        {

            return (currentState.IsKeyDown(key));

        }

    }

}
