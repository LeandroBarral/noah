namespace LobaApps.Architecture.Events
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class EventListener<TEvent> : MonoBehaviour
    {
        [SerializeField] EventChannel<TEvent> eventChannel;
        [SerializeField] UnityEvent<TEvent> unityEvent;

        protected void Awake() {
            eventChannel.Register(this);
        }

        protected void OnDestroy() {
            eventChannel.Unregister(this);
        }

        public void Raise(TEvent value)
        {
            unityEvent?.Invoke(value);
        }
    }

    public class EventListener : EventListener<Empty>
    {
    }

    public class EmptyEventListener : EventListener<Empty>
    {
    }
}