using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Eventsystem;

namespace Flame.Components
{

    public delegate void DeathDelegate(object obj);
    public delegate void HealthChangeDelegate(object obj, int OldHealth, int NewHealth, int Change);
    public delegate void UpdateDelegate(object obj);

    public interface IUpdateable
    {
        event UpdateDelegate OnUpdate;
        void Update(GameTime GameTime);

    }
    public interface IHealth
    {

        int Health
        {
            get;
        }

        event HealthChangeDelegate OnHealthChange;
        void SetHealth(int Health);
        void DecreaseHealth(int Amount);
        void IncreaseHealth(int Amount);

    }
    public interface IDieable : IHealth
    {

        bool IsDead { get; }
        event DeathDelegate OnDeath;

    }


}
