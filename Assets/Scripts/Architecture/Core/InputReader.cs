namespace LobaApps.Architecture.Core
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "InputReader", menuName = "Architecture/Core/InputReader")]
    public class InputReader : ScriptableObject, GenericPlayerInputActions.IGameplayActions
    {
        public Action OnDash;
        public Action OnInteract;
        public Action<bool> OnJump;
        public Action<Vector2> OnLook;
        public Action<Vector2> OnMove;
        public Action OnPause;
        public Action<bool> OnWalk;

        private GenericPlayerInputActions inputActions;

        private Features features = Features.All;

        public void WithFeatureToggle(Features features) => this.features = features;

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new GenericPlayerInputActions();

                inputActions.Gameplay.SetCallbacks(this);
            }
        }

        public void Disable()
        {
            inputActions.Disable();
        }

        public void EnableGameplay()
        {
            inputActions.Gameplay.Enable();
        }

        void GenericPlayerInputActions.IGameplayActions.OnDash(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Dash))
                OnDash?.Invoke();
        }

        void GenericPlayerInputActions.IGameplayActions.OnInteract(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Interact))
                OnInteract?.Invoke();
        }

        void GenericPlayerInputActions.IGameplayActions.OnJump(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Jump))
                if (context.performed || context.canceled)
                    OnJump?.Invoke(context.ReadValueAsButton());
        }

        void GenericPlayerInputActions.IGameplayActions.OnLook(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Look))
                OnLook?.Invoke(context.ReadValue<Vector2>());
        }

        void GenericPlayerInputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Move))
                OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        void GenericPlayerInputActions.IGameplayActions.OnPause(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Pause))
                OnPause?.Invoke();
        }

        void GenericPlayerInputActions.IGameplayActions.OnWalk(InputAction.CallbackContext context)
        {
            if (features.HasFlag(Features.Walk))
                OnWalk?.Invoke(context.ReadValueAsButton());
        }

        [Flags]
        public enum Features
        {
            None = 0,
            Jump = 1 << 0,
            Dash = 1 << 1,
            Interact = 1 << 2,
            Walk = 1 << 3,
            Look = 1 << 4,
            Move = 1 << 5,
            Pause = 1 << 6,
            Grounded = Dash | Interact | Walk | Look | Move | Pause,
            Air = Jump | Look | Move | Pause,
            All = Jump | Dash | Interact | Walk | Look | Move | Pause
        }
    }
}