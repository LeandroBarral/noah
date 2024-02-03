
namespace Architecture.GamePlay
{
    using LobaApps;
    using LobaApps.Architecture.Core;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AnimationManager))]
    public class Openable : MonoBehaviour, IOpenable
    {
        OpenableAnimation animationManager;

        void Awake()
        {
            TryGetComponent(out animationManager);
        }

        private void Start() {
            animationManager.Idle();
        }

        void Update()
        {

        }

        public void Open()
        {
            Debug.Log("Open " + gameObject.name);
            animationManager.Open();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPlayer _))
                Open();
        }
    }
}