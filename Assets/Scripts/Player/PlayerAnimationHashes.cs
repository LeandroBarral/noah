namespace LobaApps
{
    using UnityEngine;

    internal class PlayerAnimationHashes
    {
        public static int Idle = Animator.StringToHash("Idle");
        public static int Walk = Animator.StringToHash("Walk");
        public static int Run = Animator.StringToHash("Run");
        public static int JumpStart = Animator.StringToHash("Jump.JumpStart");
        public static int Landing = Animator.StringToHash("Landing");
    }
}