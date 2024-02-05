namespace LobaApps.Architecture.Rx
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public interface IObserver<T>
    {
        public T Value { get; set; }
        public void Set(T value);
        public void AddListener(UnityAction<T> callback);
        public void RemoveListener(UnityAction<T> callback);
        public void RemoveAllListeners();
        public void Dispose();
    }

    public class Observer<T> : IObserver<T>
    {
        [SerializeField] private T value;
        [SerializeField] UnityEvent<T> onValueChanged;

        public T Value
        {
            get => value;
            set => Set(value);
        }

        public static implicit operator T(Observer<T> observer) => observer.value;

        public Observer(T value, UnityAction<T> callback = null)
        {
            this.value = value;

            onValueChanged = new UnityEvent<T>();

            if (callback != null)
            {
                onValueChanged.AddListener(callback);
            }
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            
            onValueChanged ??= new UnityEvent<T>();
            onValueChanged.AddListener(callback);
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            
            onValueChanged?.RemoveListener(callback);
        }

        public void RemoveAllListeners()
        {
            onValueChanged?.RemoveAllListeners();
        }

        public void Dispose()
        {
            onValueChanged?.RemoveAllListeners();
            onValueChanged = null;
            value = default;
        }

        public void Set(T value)
        {
            if (Equals(this.value, value)) return;

            this.value = value;
            Invoke();
        }

        public void Set(Func<T, T> action)
        {
            Set(action.Invoke(value));
        }

        public void Invoke()
        {
            // Debug.Log($"Invoking {onValueChanged.GetPersistentEventCount()} listeners.");
            onValueChanged.Invoke(value);
        }
    }
}