using System;
using UnityEngine;
using VContainer;

namespace Alexander.RunnerCandy
{
    public class Player : IUpdatable
    {
        private readonly Transform playerTransform;
        private readonly PlayerModel playerModel;

        public int Priority => UpdatePriorityList.PLAYER;
        public event Action CollidedWithObstacle;

        public LayerMask candyLayer;
        public LayerMask obstacleLayer;
        public GameObject LosePanel;

        [Inject]
        public Player(Transform playerTransform,
            PlayerModel playerModel)
        {
            this.playerTransform = playerTransform;
            this.playerModel = playerModel;
        }

        public void DoUpdate()
        {
            CheckCandyPickup();
            CheckObstacleCollision();
        }

        private void CheckCandyPickup()
        {
            float pickupRadius = 2.0f;
            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, pickupRadius, candyLayer);

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Candy"))
                {
                    playerModel.CandyCount++;
                    UnityEngine.Object.Destroy(collider.gameObject);
                    //weightBar.AddWeightForCandy();
                }
            }
        }

        private void CheckObstacleCollision()
        {
            float rayDistance = 1.5f;
            Vector3 rayOrigin = playerTransform.position + Vector3.up * 0.5f;

            Debug.DrawRay(rayOrigin, Vector3.forward * rayDistance, Color.cyan);

            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, rayDistance, obstacleLayer))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    CollidedWithObstacle?.Invoke();
                    //playerMovement.StopMovement();
                    Time.timeScale = 0;
                }
            }
        }
    }
}