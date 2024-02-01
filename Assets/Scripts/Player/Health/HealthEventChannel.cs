namespace LobaApps
{
    using LobaApps.Architecture.Events;
    using UnityEngine;

    [CreateAssetMenu(fileName = "HealthEventChannel", menuName = "Architecture/Events/Channels/HealthEventChannel")]
    public class HealthEventChannel : EventChannel<HealthEventChannel.HealthEventData>
    {
        public struct HealthEventData
        {
            public float Current;
            public float Max;
            public float Percentage;

            public HealthEventData(
                float current,
                float max,
                float percentage
            )
            {
                Current = current;
                Max = max;
                Percentage = percentage;
            }
        }
    }
}