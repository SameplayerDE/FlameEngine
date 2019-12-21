using Flame.Eventsystem;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Eventsystem
{

    public class PreMadeEvents
    {
        public static readonly EventType KEYBOARD_RELEASE_KEY = new EventType("KeyBoardKeyReleasedEvent");
        public static readonly EventType KEYBOARD_DOWN_KEY = new EventType("KeyBoardKeyDownEvent");
        public static readonly EventType KEYBOARD_PRESS_KEY = new EventType("KeyBoardKeyPressedEvent");
    }

    public class EventType {

        private string EventName;

        public EventType(string EventName)
        {
            this.EventName = EventName;
        }

        public override string ToString()
        {
            return EventName;
        }

    }

    public enum Priority
    {
        HIGH,
        NORMAL,
        LOW
    }

    public class Event
    {

        //public EventInfo Info;
        public Priority Priority = Priority.NORMAL;
        public string EventName = "";

        public static bool operator ==(Event a, object b)
        {
            if (ReferenceEquals(b, null))
                return ReferenceEquals(a, null);

            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);

            if (b is EventType)
            {
                EventType c = b as EventType;
                if (a.EventName == c.ToString())
                {
                    return true;
                }
            }
            if (b is string)
            {
                string c = b as string;
                if (a.EventName == c)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool operator !=(Event a, object b)
        {

            if (ReferenceEquals(b, null))
                return !ReferenceEquals(a, null);

            if (ReferenceEquals(a, null))
                return !ReferenceEquals(b, null);

            if (b is EventType)
            {
                EventType c = b as EventType;
                if (a.EventName != c.ToString())
                {
                    return true;
                }
            }
            if (b is string)
            {
                string c = b as string;
                if (a.EventName != c)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return EventName;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class KeyBoardKeyDownEvent : Event
    {
        public Keys[] Keys;
        public int Amount;

        public KeyBoardKeyDownEvent(Keys[] Keys)
        {
            this.Keys = Keys;
            Amount = Keys.Length;
            Priority = Priority.HIGH;
            EventName = "KeyBoardKeyDownEvent";
        }
    }

    public class KeyBoardKeyReleasedEvent : Event
    {
        public Keys[] Keys;
        public int Amount;

        public KeyBoardKeyReleasedEvent(Keys[] Keys)
        {
            this.Keys = Keys;
            Amount = Keys.Length;
            Priority = Priority.HIGH;
            EventName = "KeyBoardKeyReleasedEvent";
        }
    }

    public class KeyBoardKeyPressedEvent : Event
    {
        public Keys[] Keys;
        public int Amount;

        public KeyBoardKeyPressedEvent(Keys[] Keys)
        {
            this.Keys = Keys;
            Amount = Keys.Length;
            Priority = Priority.HIGH;
            EventName = "KeyBoardKeyPressedEvent";
        }
    }

}
