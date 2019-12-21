using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Eventsystem
{

    public class EventInfo
    {
        public object[] Object = new object[10];
    }

    public class FlameEventHandler
    {

        public delegate void EventDelegate(Event e);
        public Dictionary<string, List<EventDelegate>> Events = new Dictionary<string, List<EventDelegate>>();
        public Event LastCalledEvent = null;

        public void Add(EventDelegate eventDelegate, EventType e)
        {
            if (Events.ContainsKey(e.ToString()))
            {
                List<EventDelegate> List;
                Events.TryGetValue(e.ToString(), out List);
                List.Add(eventDelegate);
            }
            else
            {
                List<EventDelegate> List = new List<EventDelegate>();
                List.Add(eventDelegate);
                Events.Add(e.ToString(), List);
            }
        }

        public void Remove(EventDelegate eventDelegate, EventType e)
        {
            if (Events.ContainsKey(e.ToString()))
            {
                List<EventDelegate> List;
                Events.TryGetValue(e.ToString(), out List);
                List.Remove(eventDelegate);

                //Re-adding the list
                Events.Remove(e.ToString());
                Events.Add(e.ToString(), List);
            }
        }

        public void CallEvent(Event e)
        {
            LastCalledEvent = e;
            if (Events.ContainsKey(e.EventName))
            {
                List<EventDelegate> List;
                Events.TryGetValue(e.EventName, out List);

                foreach (EventDelegate eventDelegate in List)
                {
                    eventDelegate?.Invoke(e);
                }

            }
        }

    }
}
