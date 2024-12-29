using System;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class PlayerMovement : IUpdatable, IPrioritizedInitializable
    {
        private const float RAY_LENGTH_Y = 3.0F;
        private const float RAYCAST_RADIUS = 0.5F;
        private const float MAX_SPEED = 20.0F;
        private const int MAX_LINES_COUNT = 3;
        private const float REGISTER_HEIGHT_TIME_SPAN = 0.02F;

        // Dependencies
        private readonly IInput input;
        private readonly Action openLosePanel;

        // TODO: to config
        // Params
        private readonly Transform playerT;
        private readonly float lineWidth;
        private readonly float jumpForce;
        private readonly float fallForce;

        // Собственные поля
        private int targetLine;
        private float heightSecondAgo;
        private Vector3 movementVelocity;
        private float speed;
        private bool isSwipedDown;
        private float acceleration;
        private float registerNewHeightTimer;

        public LayerMask groundLayer;
        public LayerMask obstacleLayer;
        public GameObject LosePanel;

        public Vector3 Velocity => movementVelocity;

        public int Priority => UpdatePriorityList.PLAYER_MOVEMENT;

        [Inject]
        public PlayerMovement(Transform playerT,
            IInput input,
            float lineDistance,
            float jumpForce,
            float fallForce,
            float intialSpeed)
        {
            this.lineWidth = lineDistance;
            this.playerT = playerT;
            this.input = input;
            this.jumpForce = jumpForce;
            this.fallForce = fallForce;

            speed = intialSpeed;

            // TODO: make as parameters
            acceleration = 0.5F;
        }

        public void Initialize()
        {
            // TODO: make as parameter
            float startX = GetLinePositionX(1);
            SetPlayerPositionX(startX);
        }

        private bool ScanGround(out Vector3 hitPoint)
        {
            groundLayer = LayerMask.GetMask("Ground");
            Vector3 rayOrigin = playerT.position + Vector3.up * RAY_LENGTH_Y;
            float rayLength = RAY_LENGTH_Y - RAYCAST_RADIUS;

            if (Physics.SphereCast(rayOrigin, RAYCAST_RADIUS, Vector3.down, out RaycastHit hit, rayLength, groundLayer))
            {
                hitPoint = hit.point;
                return true;
            }
            else
            {
                Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);
            }

            hitPoint = default;
            return false;
        }

        private void CheckCollisions()
        {
            Vector3 forward = playerT.forward;

            if (Physics.Raycast(playerT.position, forward, out var hit, RAY_LENGTH_Y, obstacleLayer))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    openLosePanel?.Invoke();
                }
            }
        }

        //public void StopMovement()
        //{
        //    movementVelocity = Vector3.zero;
        //    acceleration = 0.0F;
        //}

        public void DoUpdate()
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, MAX_SPEED);
            movementVelocity.z = speed;

            registerNewHeightTimer += Time.deltaTime;

            if (ScanGround(out var hitPoint))
            {
                if (registerNewHeightTimer >= REGISTER_HEIGHT_TIME_SPAN)
                {
                    float delta = heightSecondAgo - hitPoint.y;
                    if (delta >= 0.01F)
                    {
                        Debug.Log("Collided");
                    }

                    registerNewHeightTimer -= REGISTER_HEIGHT_TIME_SPAN;
                    heightSecondAgo = hitPoint.y;
                }

                isSwipedDown = false;

                playerT.position = new Vector3(playerT.position.x, hitPoint.y, playerT.position.z);
                movementVelocity.y = 0;

                if (input.InputDirection == InputDirection.Up)
                {
                    movementVelocity.y = jumpForce;
                }
            }
            else
            {
                movementVelocity.y += Time.deltaTime * Physics.gravity.y;

                if (input.InputDirection == InputDirection.Down)
                {
                    isSwipedDown = true;
                }

                if (isSwipedDown)
                {
                    movementVelocity.y += fallForce * Physics.gravity.y * Time.deltaTime * 5f;
                }
            }

            SelectTargetLine();
            MoveToTargetLine();
            CheckCollisions();

            playerT.position += movementVelocity * Time.deltaTime;
        }

        private void SelectTargetLine()
        {
            int deltaLine = 0;
            switch (input.InputDirection)
            {
                case InputDirection.Left: deltaLine--; break;
                case InputDirection.Right: deltaLine++; break;
            }

            targetLine += deltaLine;
            targetLine = Mathf.Clamp(targetLine, 0, MAX_LINES_COUNT - 1);
        }

        private float GetLinePositionX(int lineIndex)
        {
            float targetX = lineIndex * lineWidth + lineWidth / 2.0F;
            return targetX;
        }

        private void MoveToTargetLine()
        {
            float targetX = GetLinePositionX(targetLine);

            // TODO: make as parameter
            float moveSpeedX = 10.0f;

            float xPosition = Mathf.MoveTowards(playerT.position.x, targetX, moveSpeedX * Time.deltaTime);
            if (xPosition == playerT.position.x)
            {
                return;
            }

            SetPlayerPositionX(xPosition);
        }

        private void SetPlayerPositionX(float x)
        {
            var playerPosition = playerT.position;
            playerPosition.x = x;
            playerT.position = playerPosition;
        }
    }
}
