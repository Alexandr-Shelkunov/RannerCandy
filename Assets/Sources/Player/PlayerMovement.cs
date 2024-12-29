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

        // Dependencies
        private readonly IInput input;
        private readonly Action openLosePanel;

        // TODO: to config
        // Params
        private readonly Transform playerT;
        private readonly float lineWidth;
        private readonly float jumpForce;
        private readonly float fallForce;
        private readonly float acceleration;

        // Собственные поля
        private int currentLine;
        private int targetLine;
        private Vector3 movementVelocity;
        private float speed;
        private bool isSwipedDown;

        public LayerMask groundLayer;
        public LayerMask obstacleLayer;
        public GameObject LosePanel;

        public Vector3 Velocity => movementVelocity;

        public int Priority => UpdatePriorityList.PLAYER_MOVEMENT;

        [Inject]
        public PlayerMovement(Transform playerT,
            IInput input,
            Action openLosePanel,
            float lineDistance,
            float jumpForce,
            float fallForce,
            float intialSpeed,
            GameObject losePanel)
        {
            this.lineWidth = lineDistance;
            this.playerT = playerT;
            this.input = input;
            this.jumpForce = jumpForce;
            this.fallForce = fallForce;
            this.LosePanel = losePanel;

            speed = intialSpeed;

            // TODO: make as parameters
            acceleration = 0.5F;
            currentLine = 1;
        }

        public void Initialize()
        {
            targetLine = currentLine;
            float startX = GetLinePositionX(currentLine);
            SetPlayerPositionX(startX);
        }

        private bool IsGrounded(out Vector3 hitPoint)
        {
            groundLayer = LayerMask.GetMask("Ground");
            Vector3 rayOrigin = playerT.position + Vector3.up * RAY_LENGTH_Y;
            float rayLength = RAY_LENGTH_Y - RAYCAST_RADIUS;

            Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);

            if (Physics.SphereCast(rayOrigin, RAYCAST_RADIUS, Vector3.down, out RaycastHit hit, rayLength, groundLayer))
            {
                float lowerBoundY = playerT.position.y; 
                float maxYThreshold = lowerBoundY + 0.1f; 

                if (hit.point.y >= lowerBoundY && hit.point.y <= maxYThreshold)
                {
                    hitPoint = hit.point;
                    return true; 
                }
            }

            hitPoint = default;
            return false;
        }

        public void OpenLosePanel()
        {
            LosePanel.SetActive(true);
        }

        private void CheckCollisions()
        {
            Vector3 forward = playerT.forward;
            RaycastHit hit;

            if (Physics.Raycast(playerT.position, forward, out hit, RAY_LENGTH_Y, obstacleLayer))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    openLosePanel?.Invoke();
                }
            }
        }

        public void StopMovement()
        {
            movementVelocity = Vector3.zero; 
        }

        public void DoUpdate()
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, MAX_SPEED);
            movementVelocity.z = speed;

            if (IsGrounded(out var hitPoint))
            {
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
