namespace LobaApps.Architecture.Core
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using System.Collections;

    public abstract class AnimationManager : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        protected abstract float CrossFadeDuration { get; }

        public abstract IDictionary<int, float> AnimationDurations { get; }

        protected float CrossFade(int animationHash, float? crossFadeDuration = null)
        {
            float duration = crossFadeDuration ?? CrossFadeDuration;
            animator.CrossFade(animationHash, duration);
            AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfos.Length == 0) return 0;

            return clipInfos[0].clip.length;
        }

        protected void CrossFadeAndWait(int animationHash, float? crossFadeDuration = null)
        {
            StartCoroutine(CrossFadeAndWaitRoutine(animationHash, crossFadeDuration));
        }

        private IEnumerator CrossFadeAndWaitRoutine(int animationHash, float? crossFadeDuration = null){
            float duration = CrossFade(animationHash, crossFadeDuration);
            yield return new WaitForSeconds(duration);
        }
    }
}