namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.Core;
    using UnityEngine;

    public class PlayerAnimation : AnimationManager
    {
        static readonly int IdleHash = Animator.StringToHash("Idle");
        static readonly int WalkHash = Animator.StringToHash("Walk");
        static readonly int RunHash = Animator.StringToHash("Run");
        static readonly int JumpStartHash = Animator.StringToHash("Jump.JumpStart");
        static readonly int FallingHash = Animator.StringToHash("Jump.Falling");
        static readonly int LandingHash = Animator.StringToHash("Landing");
        static readonly int Attack01Hash = Animator.StringToHash("Attack01");

        protected override float CrossFadeDuration => 0.1f;

        public override IDictionary<int, float> AnimationDurations { get; } = new Dictionary<int, float>()
        {
            { IdleHash, .5f },
            { Attack01Hash, .5f },
            { WalkHash, 0.5f },
            { RunHash, 0.5f },
            { JumpStartHash, 0.25f },
            { FallingHash, 0.25f },
            { LandingHash, 1.2f },
        };

        public float Attack01(float? crossFadeDuration = null) => CrossFadeAnimation(Attack01Hash, crossFadeDuration);
        public float Idle(float? crossFadeDuration = null) => CrossFadeAnimation(IdleHash, crossFadeDuration);
        public float Walk(float? crossFadeDuration = null) => CrossFadeAnimation(WalkHash, crossFadeDuration);
        public float Run(float? crossFadeDuration = null) => CrossFadeAnimation(RunHash, crossFadeDuration);
        public float JumpStart(float? crossFadeDuration = null) => CrossFadeAnimation(JumpStartHash, crossFadeDuration);
        public float Falling(float? crossFadeDuration = null) => CrossFadeAnimation(FallingHash, crossFadeDuration);
        public float Landing(float? crossFadeDuration = null) => CrossFadeAnimation(LandingHash, crossFadeDuration);
    }
}