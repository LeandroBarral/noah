namespace LobaApps.Architecture.Core
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    public abstract class AnimationManager : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        protected abstract float CrossFadeDuration { get; }

        public abstract IDictionary<int, float> AnimationDurations { get; }

        protected float CrossFadeAnimation(int animationHash, float? crossFadeDuration = null)
        {
            float duration = crossFadeDuration ?? CrossFadeDuration;
            animator.CrossFade(animationHash, duration);
            AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
            return clipInfo.clip.length;
        }
    }
}