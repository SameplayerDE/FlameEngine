using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Eventsystem;

namespace Flame
{
    public static class Flame
    {

        public static readonly string VERSION = "0.0.1";
        public static Random Random = new Random();
        public static GraphicsDevice GraphicsDevice;
        public static FlameEventHandler EventHandler = new FlameEventHandler();

        

    }
}
