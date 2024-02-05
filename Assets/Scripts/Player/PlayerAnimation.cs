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

        public void Attack01(float? crossFadeDuration = null) => CrossFadeAndWait(Attack01Hash, crossFadeDuration);
        public void Idle(float? crossFadeDuration = null) => CrossFade(IdleHash, crossFadeDuration);
        public void Walk(float? crossFadeDuration = null) => CrossFadeAndWait(WalkHash, crossFadeDuration);
        public void Run(float? crossFadeDuration = null) => CrossFadeAndWait(RunHash, crossFadeDuration);
        public void JumpStart(float? crossFadeDuration = null) => CrossFadeAndWait(JumpStartHash, crossFadeDuration);
        public void Falling(float? crossFadeDuration = null) => CrossFadeAndWait(FallingHash, crossFadeDuration);
        public void Landing(float? crossFadeDuration = null) => CrossFadeAndWait(LandingHash, crossFadeDuration);
    }
}