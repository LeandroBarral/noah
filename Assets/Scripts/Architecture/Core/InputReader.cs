namespace LobaApps.Architecture.Core
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "InputReader", menuName = "Architecture/Core/InputReader")]
    public class InputReader : ScriptableObject, PlayerInputActions.IGameplayActions
    {
        public Action OnDash;
        public Action OnInteract;
        public Action<bool> OnJump;
        public Action<Vector2> OnLook;
        public Action<Vector2> OnMove;
        public Action OnPause;
        public Action<bool> OnWalk;

        private PlayerInputActions inputActions;

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();

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

        void PlayerInputActions.IGameplayActions.OnDash(InputAction.CallbackContext context)
        {
            OnDash?.Invoke();
        }

        void PlayerInputActions.IGameplayActions.OnInteract(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke();
        }

        void PlayerInputActions.IGameplayActions.OnJump(InputAction.CallbackContext context)
        {
            OnJump?.Invoke(context.ReadValueAsButton());
        }

        void PlayerInputActions.IGameplayActions.OnLook(InputAction.CallbackContext context)
        {
            OnLook?.Invoke(context.ReadValue<Vector2>());
        }

        void PlayerInputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        void PlayerInputActions.IGameplayActions.OnPause(InputAction.CallbackContext context)
        {
            OnPause?.Invoke();
        }

        void PlayerInputActions.IGameplayActions.OnWalk(InputAction.CallbackContext context)
        {
            OnWalk?.Invoke(context.ReadValueAsButton());
        }
    }
}