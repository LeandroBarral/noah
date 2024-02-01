namespace LobaApps.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class SmoothSlider : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] float smoothingSpeed = 5f;

        float targetValue;

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        void Start()
        {
            targetValue = slider.value;
        }

        void Update()
        {
            // Calculate the interpolation factor based on the difference between current and target values
            float interpolationFactor = 1 - Mathf.Exp(-smoothingSpeed * Time.deltaTime);

            // Smoothly interpolate towards the target value
            slider.value = Mathf.Lerp(slider.value, targetValue, interpolationFactor);
        }

        public void SetValue(float newValue)
        {
            targetValue = Mathf.Clamp(newValue, slider.minValue, slider.maxValue);
        }
    }
}