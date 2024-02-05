
namespace LobaApps
{
    using LobaApps.Architecture.Core;
    using LobaApps.Architecture.GamePlay;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AnimationManager))]
    public class Openable : MonoBehaviour, IOpenable
    {
        OpenableAnimation animationManager;
        State state;

        void Awake()
        {
            TryGetComponent(out animationManager);
        }

        private void Start()
        {
            state = State.Closed;
            animationManager.Idle();
        }

        void Update()
        {

        }

        public void Open()
        {
            state = State.Opened;
            animationManager.Open();
        }

        void OnCollisionEnter(Collision other)
        {
            if (state == State.Closed && other.gameObject.TryGetComponent(out IPlayer _))
                Open();
        }

        enum State
        {
            Closed,
            Opened
        }
    }
}