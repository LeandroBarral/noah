namespace LobaApps
{
    using System.Collections;
    using LobaApps.UI;
    using UnityEngine;

    public class HealthSlider : MonoBehaviour
    {
        private SmoothSlider slider;
        private HealthEventChannel.HealthEventData data;

        private void Awake()
        {
            slider = GetComponent<SmoothSlider>();
        }

        public void SetHealth(HealthEventChannel.HealthEventData healthEventData)
        {
            data = healthEventData;
            StartCoroutine(nameof(SetHealthRoutine));
        }

        public IEnumerator SetHealthRoutine()
        {
            yield return new WaitForSeconds(0.05f);
            slider.SetValue(data.Percentage);
        }
    }
}
