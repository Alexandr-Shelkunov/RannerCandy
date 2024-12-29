using VContainer;

namespace Alexander.RunnerCandy
{
    public class PlayerModel
    {
        public float DistanceScore { get; set; }
        public int CandyCount { get; set; }
        public float Weight { get; set; }

        [Inject]
        public PlayerModel()
        {
            ResetStats();
        }

        public void ResetStats()
        {
            DistanceScore = 0f;
            CandyCount = 0;
            Weight = 40f;
        }
    }
}
