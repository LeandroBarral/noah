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
            OnDash?.Invoke();
        }

        void GenericPlayerInputActions.IGameplayActions.OnInteract(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke();
        }

        void GenericPlayerInputActions.IGameplayActions.OnJump(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
                OnJump?.Invoke(context.ReadValueAsButton());
        }

        void GenericPlayerInputActions.IGameplayActions.OnLook(InputAction.CallbackContext context)
        {
            OnLook?.Invoke(context.ReadValue<Vector2>());
        }

        void GenericPlayerInputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        void GenericPlayerInputActions.IGameplayActions.OnPause(InputAction.CallbackContext context)
        {
            OnPause?.Invoke();
        }

        void GenericPlayerInputActions.IGameplayActions.OnWalk(InputAction.CallbackContext context)
        {
            OnWalk?.Invoke(context.ReadValueAsButton());
        }
    }
}