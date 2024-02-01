namespace LobaApps.Architecture.Events
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class EventChannel<TEvent> : ScriptableObject
    {
        readonly HashSet<EventListener<TEvent>> observers = new();

        public void Invoke(TEvent value)
        {
            foreach (var observer in observers)
            {
                observer.Raise(value);
            }
        }

        public void Register(EventListener<TEvent> observer)
        {
            observers.Add(observer);
        }

        public void Unregister(EventListener<TEvent> observer)
        {
            observers.Remove(observer);
        }
    }

    [CreateAssetMenu(menuName = "Architecture/Events/Channels/EventChannel", fileName = "EventChannel")]
    public class EventChannel : EventChannel<Event>
    {
    }

    public readonly struct Empty { }

    public class EmptyEventChannel : EventChannel<Empty>
    {
    }
}