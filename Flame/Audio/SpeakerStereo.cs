using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Audio
{
    public class SpeakerStereo : Speaker
    {

        public float VolumenLeft { get; set; } = 1f;
        public float VolumenRight { get; set; } = 1f;
        public Vector3 Position { get; set; } = Vector3.Zero;

        public SpeakerStereo(int X, int Y, int Z)
        {
            Position = new Vector3(X, Y, Z);
        }

        public SpeakerStereo(Vector3 Position)
        {
            this.Position = Position;
        }

        public virtual void Update()
        {

        }

    }
}
