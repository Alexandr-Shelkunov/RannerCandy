using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class LevelGenerator : IInitializable, IUpdatable
    {
        private readonly GameObject[] platformsPrefabs;
        private readonly Transform playerT;
        private readonly Transform spawnRootT;

        private const float LEVEL_PART_LENGTH = 24.0F;
        private const float DISTNACE_OFFSET = LEVEL_PART_LENGTH * 6;

        private List<GameObject> activeLevelParts;

        public int Priority => UpdatePriorityList.LEVEL_GENERATOR;

        [Inject]
        public LevelGenerator(GameObject[] platformsPrefabs,
            Transform playerT,
            Transform spawnRootT)
        {
            this.platformsPrefabs = platformsPrefabs;
            this.playerT = playerT;
            this.spawnRootT = spawnRootT;

            activeLevelParts = new List<GameObject>();
        }

        public void Initialize()
        {
            DoUpdate();
        }

        public void DoUpdate()
        {
            float length = CalcCurrentSpawnedLength();

            while (playerT.position.z + DISTNACE_OFFSET >= length)
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
                spawnRootT);

            activeLevelParts.Add(nextLevelPart);
        }
    }
}
