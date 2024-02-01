namespace LobaApps.Architecture.Events
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Architecture/Events/Channels/IntEventChannel", fileName = "IntEventChannel")]
    public class IntEventChannel : EventChannel<int>
    {
    }
}