namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.Core;
    using UnityEngine;

    public class OpenableAnimation : AnimationManager
    {
        static readonly int IdleHash = Animator.StringToHash("Idle");
        static readonly int OpenHash = Animator.StringToHash("Open");

        protected override float CrossFadeDuration => 0.1f;

        public override IDictionary<int, float> AnimationDurations { get; } = new Dictionary<int, float>()
        {
            { IdleHash, 3f },
            { OpenHash, 3.5f },
        };

        public float Idle() => CrossFadeAnimation(IdleHash);
        public float Open() => CrossFadeAnimation(OpenHash);
    }
}