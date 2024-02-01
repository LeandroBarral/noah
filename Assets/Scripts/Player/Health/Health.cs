namespace LobaApps
{
    using UnityEngine;

    public partial class Health : MonoBehaviour
    {
        [SerializeField] float max = 100;
        [SerializeField] HealthEventChannel healthChanged;

        public float Max
        {
            get => max; set
            {
                max = value;
                RaiseHealthChanged();
            }
        }

        float current;
        public float Current
        {
            get => current; set
            {
                current = value;
                RaiseHealthChanged();
            }
        }

        public float Percentage => current / max * 100f;

        public bool IsDead => current <= 0;

        void Awake()
        {
            Current = Max;
        }

        void Start()
        {
            RaiseHealthChanged();
        }

        public void SetMaxHealth(float newMaxHealth)
        {
            Max = newMaxHealth;
            Current = Max;
        }

        public void Damage(float amount)
        {
            Current -= amount;
            Current = Mathf.Clamp(Current, 0, Max);
        }

        public void Heal(float amount)
        {
            Current += amount;
            Current = Mathf.Clamp(Current, 0, Max);
        }

        public void FullHeal()
        {
            Current = Max;
        }

        private void RaiseHealthChanged()
        {
            if (healthChanged != null)
                healthChanged.Invoke(
                    new HealthEventChannel.HealthEventData(
                        Current,
                        Max,
                        Percentage
                    )
                );
        }
    }
}