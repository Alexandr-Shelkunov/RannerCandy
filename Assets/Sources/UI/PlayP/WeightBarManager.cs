using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Alexander.RunnerCandy
{
    public class WeightBarManager : MonoBehaviour
    {
        [SerializeField] private Slider weightSlider;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Transform playerTransform;


        private WeightBar weightBar;

        [Inject]
        private void Construct()
        {
            weightBar = new WeightBar(weightSlider, losePanel, playerTransform);
        }

        private void Update()
        {
            weightBar.DoUpdate();
        }
    }
}
