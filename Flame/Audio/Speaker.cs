using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace Flame.Audio
{

    public abstract class Speaker
    {

        protected delegate void SpeakerEvent();
        protected event SpeakerEvent OnSpeakerPlayerEvent;

        public float Volumen { get; set; } = 1f;

        public virtual void Play()
        {
            OnSpeakerPlayerEvent();
        }

    }
}
