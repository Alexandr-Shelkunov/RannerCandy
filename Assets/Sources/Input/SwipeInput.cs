using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class SwipeInput : IInput, IUpdatable, IInitializable
    {
        // Parameters/dependencies
        private readonly float swipeRegisterOffset;

        // Own fields
        private Vector2 startTouchPosition;
        private Vector2 currentTouchPosition;
        private bool isSwipeInProgress;

        // Public fields
        public InputDirection InputDirection { get; private set; }
        public int Priority => UpdatePriorityList.INPUT;

        public SwipeInput(float swipeRegisterOffset)
        {
            this.swipeRegisterOffset = swipeRegisterOffset;
        }

        public void Initialize()
        {
            InputDirection = InputDirection.None;
        }

        public void DoUpdate()
        {
            if (Input.touchCount == 0)
            {
                return;
            }

            InputDirection = InputDirection.None;
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began: HandleTouchStart(touch); break;
                case TouchPhase.Moved: HandleTouchChange(touch); return;
                case TouchPhase.Ended: HandleTouchEnd(); break;
            }
        }

        private void HandleTouchEnd()
        {
            isSwipeInProgress = false;
        }

        private void HandleTouchStart(Touch touch)
        {
            startTouchPosition = touch.position;
            isSwipeInProgress = true;
        }

        private void HandleTouchChange(Touch touch)
        {
            if (!isSwipeInProgress)
            {
                return;
            }

            currentTouchPosition = touch.position;
            Vector2 delta = currentTouchPosition - startTouchPosition;

            bool isHorizontal = Mathf.Abs(delta.x) > Mathf.Abs(delta.y);
            if (isHorizontal)
            {
                if (delta.x > swipeRegisterOffset)
                {
                    SetSwipeDirection(InputDirection.Right);
                }
                else if (delta.x < -swipeRegisterOffset)
                {
                    SetSwipeDirection(InputDirection.Left);
                }
            }
            else
            {
                if (delta.y > swipeRegisterOffset)
                {
                    SetSwipeDirection(InputDirection.Up);
                }
                else if (delta.y < -swipeRegisterOffset)
                {
                    SetSwipeDirection(InputDirection.Down);
                }
            }
        }

        private void SetSwipeDirection(InputDirection direction)
        {
            InputDirection = direction;
            isSwipeInProgress = false;
        }
    }
}