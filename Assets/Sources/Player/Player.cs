using System;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public class Player : MonoBehaviour, IUpdatable
    {
        public event Action CollidedWithObstacle;

        public PlayerModel Model { get; private set; }
        private Transform playerTransform;

        private PlayerMovement playerMovement;

        public int Priority => UpdatePriorityList.PLAYER;

        public Player(Transform playerT, PlayerModel model)
        {
            playerTransform = playerT;
            Model = model;
        }

        private void Start()
        {
            Debug.Log("Player Initialized with PlayerModel. CandyCount: " + Model.CandyCount);
        }

        public void DoUpdate()
        {
            playerMovement.DoUpdate();
            CheckCandyPickup();
            CheckObstacleCollision();
        }

        private void CheckCandyPickup()
        {
            float pickupRadius = 1.0f;
            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, pickupRadius);

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Candy"))
                {
                    Model.CandyCount++;
                    Destroy(collider.gameObject);
                    Model.Weight += 3;
                }
            }
        }

        private void CheckObstacleCollision()
        {
            float rayDistance = 1.5f;
            Vector3 rayOrigin = playerTransform.position + Vector3.up * 0.5f;

            Debug.DrawRay(rayOrigin, Vector3.forward * rayDistance, Color.red);

            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.CompareTag("obstacle"))
                {
                    CollidedWithObstacle?.Invoke();
                    Time.timeScale = 0;
                }
            }
        }
    }
}