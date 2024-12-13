//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Alexender.Runer
//{
//    public class Player : MonoBehaviour, IUpdatable
//    {
//        // �����������
//        [SerializeField] private LoopController loopController;
//        [SerializeField] private Transform sceneRoot;

//        // ��������� ��������
//        [SerializeField] private float jumpForce;
//        [SerializeField] private float fallForce;
//        [SerializeField] private float gravity;
//        [SerializeField] private float lineDistance;
//        [SerializeField] private float speed;

//        public event Action CollidedWithObstacle;

//        public PlayerModel Model { get; private set; }

//        // ����� ��������� ������ ��������
//        private PlayerMovement playerMovement;

//        private void Awake()
//        {
//            // ������������� ������ � ��������
//            Model = new PlayerModel();
//            playerMovement = new PlayerMovement(lineDistance, transform, jumpForce, fallForce, speed);
//        }

//        private void Start()
//        {
//            // ������������ Player � LoopController
//            if (loopController != null)
//            {
//                loopController.Register(this);
//            }
//        }

//        // ���������� ������� (����� � LoopController)
//        public void DoUpdate()
//        {
//            // ��������� �������� � ������ ��������������
//            playerMovement.DoUpdate();
//            CheckCandyPickup();
//            CheckObstacleCollision();
//        }

//        // �������� ������
//        private void CheckCandyPickup()
//        {
//            float pickupRadius = 1.0f; // ������ �������� ������
//            Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRadius);

//            foreach (var collider in hitColliders)
//            {
//                if (collider.CompareTag("Candy"))
//                {
//                    Model.CandyCount++;
//                    Destroy(collider.gameObject);
//                    Model.Weight += 3; // ���������� CANDY_WEIGHT �� PlayerPhysicsHandler
//                }
//            }
//        }

//        // �������� �����������
//        private void CheckObstacleCollision()
//        {
//            float rayDistance = 1.5f; // ��������� �������� ����� �������
//            Vector3 rayOrigin = transform.position + Vector3.up * 0.5f; // �������� �����

//            Debug.DrawRay(rayOrigin, Vector3.forward * rayDistance, Color.red);

//            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, rayDistance))
//            {
//                if (hit.collider.CompareTag("obstacle"))
//                {
//                    CollidedWithObstacle?.Invoke();
//                    Time.timeScale = 0; // ������������� ����
//                }
//            }
//        }
//    }
//}