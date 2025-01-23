//using TMPro;
//using UnityEngine;

//namespace Alexander.RunnerCandy
//{
//    public class CalculationOfResults : MonoBehaviour
//    {
//        [SerializeField] private TextMeshProUGUI candyScoreText;
//        [SerializeField] private TextMeshProUGUI distanceScoreText;

//        private PlayerModel model;

//        private void Awake()
//        {
//            model = player.Model;
//            player.CollidedWithObstacle += PlayerCollidedWithObstacleHandler;
//        }

//        private void Update()
//        {
//            candyScoreText.text = $"Candy score:\n{model.CandyCount}";
//            distanceScoreText.text = $"Distance score:\n{model.DistanceScore:0}";
//        }
//    }
//}
