using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Alexander.RunnerCandy
{
    public class WeightBar : IUpdatable
    {
        private readonly Slider weightSlider;
        private readonly GameObject losePanel;
        private readonly Transform playerTransform;

        private const float InitialWeight = 40f;
        private const float WeightDecrement = 1f;
        private const float CandyBonus = 1f;
        private const float MinWeight = 0f;
        private const float MaxWeight = 100f;

        private const float PickupRadius = 1.0f;

        private float currentWeight;

        public float CurrentWeight => currentWeight;

        public int Priority => 0;

        public WeightBar(Slider weightSlider, GameObject losePanel, Transform playerTransform)
        {
            this.weightSlider = weightSlider;
            this.losePanel = losePanel;
            this.playerTransform = playerTransform;
            Initialize();
        }

        private void Initialize()
        {
            currentWeight = InitialWeight;
            if (weightSlider != null)
            {
                weightSlider.value = currentWeight;
            }
        }

        public void DoUpdate()
        {
            currentWeight -= WeightDecrement * Time.deltaTime;
            currentWeight = Mathf.Max(currentWeight, MinWeight);

            if (weightSlider != null)
            {
                weightSlider.value = currentWeight / MaxWeight;
            }

            CheckCandyPickup();

            if (currentWeight <= MinWeight)
            {
                ShowLosePanel();
            }
        }

        private void CheckCandyPickup()
        {
            LayerMask candyLayer = LayerMask.GetMask("Candy");
            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, PickupRadius, candyLayer);

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Candy"))
                {
                    AddWeightForCandy(); 
                }
            }
        }

        public void AddWeightForCandy()
        {
            currentWeight += CandyBonus;
            currentWeight = Mathf.Min(currentWeight, MaxWeight);

            if (weightSlider != null)
            {
                weightSlider.value = currentWeight / MaxWeight;
            }
        }

        private void ShowLosePanel()
        {
            if (losePanel != null)
            {
                losePanel.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("Game Over! Show LosePanel.");
            }
        }
    }
}