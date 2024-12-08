using UnityEngine;
using VContainer;

namespace Alexander.RunnerCandy
{
    public class SwipeController : IUpdatable
    {
        private Vector2 startTouch, swipeDelta;
        private bool isDragging;

        public bool Tap { get; private set; }
        public bool SwipeLeft { get; private set; }
        public bool SwipeRight { get; private set; }
        public bool SwipeUp { get; private set; }
        public bool SwipeDown { get; private set; }

        public int Priority => UpdatePriorityList.INPUT;

        public void DoUpdate()
        {
            Tap = SwipeLeft = SwipeRight = SwipeUp = SwipeDown = false;

            if (Input.GetMouseButtonDown(0) || (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began))
            {
                Tap = true;
                isDragging = true;
                startTouch = Input.mousePosition;

                if (Input.touches.Length > 0)
                    startTouch = Input.touches[0].position;
            }

            if (Input.GetMouseButtonUp(0) || (Input.touches.Length > 0 &&
                (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)))
            {
                isDragging = false;
                Reset();
            }

            swipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touches.Length > 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            DetectSwipeDirection();
        }

        private void DetectSwipeDirection()
        {
            if (swipeDelta.magnitude > 100) 
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    SwipeLeft = x < 0;
                    SwipeRight = x > 0;
                }
                else
                {
                    SwipeDown = y < 0;
                    SwipeUp = y > 0;
                }

                Reset();
            }
        }

        private void Reset()
        {
            startTouch = swipeDelta = Vector2.zero;
            isDragging = false;
        }
    }
}