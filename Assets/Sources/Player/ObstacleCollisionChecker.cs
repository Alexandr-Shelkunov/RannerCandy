using System;
using UnityEngine;
using VContainer;

namespace Alexander.RunnerCandy
{
    public class ObstacleCollisionChecker : IUpdatable
    {
        private readonly Transform playerTransform;
        public LayerMask obstacleLayer;
        public event Action CollidedWithObstacle;
        public int Priority => UpdatePriorityList.OBSTACLE_COLLISION_CHECKER;

        public ObstacleCollisionChecker(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        public void DoUpdate()
        {
            CheckObstacleCollision();
        }

        private void CheckObstacleCollision()
        {
            obstacleLayer = LayerMask.GetMask("Obstacle");
            float rayDistance = 0.5f;
            Vector3 rayOrigin = playerTransform.position + Vector3.up * 0.5f;

            Debug.DrawRay(rayOrigin, Vector3.forward * rayDistance, Color.cyan);

            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, rayDistance, obstacleLayer))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    CollidedWithObstacle?.Invoke();
                    Time.timeScale = 0;
                }
            }
        }
    }
}