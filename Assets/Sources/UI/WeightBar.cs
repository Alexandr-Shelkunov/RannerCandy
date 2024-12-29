using System;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public class WeightBar : IUpdatable
    {
        // Константы для шкалы веса
        private const float InitialWeight = 40f;  
        private const float WeightDecrement = 1f; 
        private const float CandyBonus = 0.5f;   
        private const float MinWeight = 0f;      
        private const float MaxWeight = 100f;    

        private float currentWeight;

        private readonly UnityEngine.UI.Slider weightSlider;
        public event Action GameOver; 

        public float CurrentWeight => currentWeight;

        public WeightBar(UnityEngine.UI.Slider slider)
        {
            currentWeight = InitialWeight;  
            weightSlider = slider ?? throw new ArgumentNullException(nameof(slider));
        }

        public void DoUpdate()
        {
            currentWeight -= WeightDecrement * Time.deltaTime;
            currentWeight = Mathf.Max(currentWeight, MinWeight);  

            if (currentWeight <= MinWeight || currentWeight >= MaxWeight)
            {
                GameOver?.Invoke(); 
            }

            weightSlider.value = currentWeight;

            Debug.Log("Current Weight: " + currentWeight);
        }

        public void AddWeightForCandy()
        {
            currentWeight += CandyBonus;
            currentWeight = Mathf.Min(currentWeight, MaxWeight);  

            weightSlider.value = currentWeight;
        }
    }
}