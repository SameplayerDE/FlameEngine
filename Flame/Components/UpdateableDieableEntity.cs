using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Flame.Components
{
    public abstract class UpdateableDieableEntity : Entity, IUpdateable, IDieable
    {
        public int Health { get; protected set; }
        public bool IsDead { get; protected set; } = false;

        public event UpdateDelegate OnUpdate;
        public event DeathDelegate OnDeath;
        public event HealthChangeDelegate OnHealthChange;

        public void DecreaseHealth(int Amount)
        {
            if (Amount != 0)
            {
                SetHealth(Health - Amount);
            }
        }

        public void IncreaseHealth(int Amount)
        {
            if (Amount != 0)
            {
                SetHealth(Health + Amount);
            }
        }

        public void SetHealth(int Health)
        {
            if (Health != this.Health)
            {
                OnHealthChange?.Invoke(this, this.Health, Health, Health - this.Health);
                this.Health = Health;
            }
        }

        public virtual void Update(GameTime GameTime)
        {
            if (Health <= 0)
            {
                if (!IsDead)
                {
                    IsDead = true;
                    OnDeath?.Invoke(this);
                }
            }
            OnUpdate?.Invoke(this);
        }
    }
}
