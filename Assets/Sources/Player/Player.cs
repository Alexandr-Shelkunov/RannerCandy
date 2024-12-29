using System;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public class Player : MonoBehaviour, IUpdatable
    {
        private readonly Transform playerTransform;

        public event Action CollidedWithObstacle;

        public PlayerModel Model { get; private set; }

        private PlayerMovement playerMovement;

        private WeightBar weightBar;

        public int Priority => UpdatePriorityList.PLAYER;

        public LayerMask candyLayer;
        public LayerMask obstacleLayer;

        public GameObject LosePanel;


        public Player(Transform playerT)
        {
            playerTransform = playerT;
        }

        public void Initialize(PlayerModel model, PlayerMovement movement)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            playerMovement = movement ?? throw new ArgumentNullException(nameof(movement));
            weightBar = weight ?? throw new ArgumentNullException(nameof(weight));

            weightBar.GameOver += OnGameOver;
        }


        private void Start()
        {
            Debug.Log("Player Initialized with PlayerModel. CandyCount: " + Model.CandyCount);
        }

        public void DoUpdate()
        {
            playerMovement.DoUpdate();
            weightBar.DoUpdate();
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
                    Model.CandyCount++;
                    Destroy(collider.gameObject);
                    weightBar.AddWeightForCandy();
                }
            }
        }

        private void CheckObstacleCollision()
        {
            float rayDistance = 1.5f;
            Vector3 rayOrigin = playerTransform.position + Vector3.up * 0.5f;

            Debug.DrawRay(rayOrigin, Vector3.forward * rayDistance, Color.red);

            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, rayDistance, obstacleLayer))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    CollidedWithObstacle?.Invoke();
                    playerMovement.StopMovement();
                    Time.timeScale = 0;
                }
            }

            private void OnGameOver()
            {
                Debug.Log("Game Over!");
                LosePanel.SetActive(true);
                Time.timeScale = 0; 
            }
        }
    }
}