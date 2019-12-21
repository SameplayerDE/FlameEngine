using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Graphics
{
    public class Parallax
    {

        public List<Camera> Cameras = new List<Camera>();
        public List<Renderer> Renderers = new List<Renderer>();
        
        public void AddCamera(ref Renderer Renderer)
        {
            Cameras.Add(Renderer.Camera);
            Renderers.Add(Renderer);
        }

        public void Move(Vector2 Direction)
        {
            float Count = 0.5f;
            for (int i = 1; i <= Cameras.Count; i++)
            {
                //Cameras[i - 1].SetFocus((Direction / i).ToPoint());
                Renderers[i - 1].RenderTargetPosition = -(Direction * (Count));
                Count -= 0.25f;
            }
        }

    }
}
