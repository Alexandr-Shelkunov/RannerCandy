using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class LevelGenerator : IInitializable, IUpdatable
    {
        private readonly GameObject[] platformsPrefabs;
        private readonly Transform player;
        private readonly Transform spawnRoot;

        private const float LEVEL_PART_LENGTH = 24.0F;
        private const float DISTNACE_OFFSET = LEVEL_PART_LENGTH * 6;

        private List<GameObject> activeLevelParts;

        public int Priority => UpdatePriorityList.LEVEL_GENERATOR;

        [Inject]
        public LevelGenerator(GameObject[] platformsPrefabs,
            Transform player,
            Transform spawnRoot)
        {
            this.platformsPrefabs = platformsPrefabs;
            this.player = player;
            this.spawnRoot = spawnRoot;

            activeLevelParts = new List<GameObject>();
        }

        public void Initialize()
        {
            DoUpdate();
        }

        public void DoUpdate()
        {
            float length = CalcCurrentSpawnedLength();

            while (player.position.z + DISTNACE_OFFSET >= length)
            {
                int randomPlatformIndex = Random.Range(0, platformsPrefabs.Length);
                SpawnLevelPart(randomPlatformIndex, length);
                length = CalcCurrentSpawnedLength();
            }
        }

        private float CalcCurrentSpawnedLength()
        {
            int currentSpawnedParts = activeLevelParts.Count;
            return currentSpawnedParts * LEVEL_PART_LENGTH;
        }

        private void SpawnLevelPart(int levelPartIndex, float spawnZ)
        {
            GameObject prefab = platformsPrefabs[levelPartIndex];
            Vector3 spawnPosition = Vector3.forward * spawnZ;

            GameObject nextLevelPart = Object.Instantiate(prefab,
                spawnPosition,
                Quaternion.identity,
                spawnRoot);

            activeLevelParts.Add(nextLevelPart);
        }
    }
}
