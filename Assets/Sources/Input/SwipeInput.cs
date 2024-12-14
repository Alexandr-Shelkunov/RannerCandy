using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public class SwipeInput : IInput, IUpdatable
    {
        public InputDirection InputDirection { get; private set; }

        public int Priority => UpdatePriorityList.INPUT;

        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        private bool _isSwipe;

        public void DoUpdate()
        {
            InputDirection = InputDirection.None;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _isSwipe = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isSwipe)
                        {
                            _endTouchPosition = touch.position;
                            Vector2 delta = _endTouchPosition - _startTouchPosition;

                            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                            {
                                if (delta.x > 50) 
                                {
                                    InputDirection = InputDirection.Right;
                                    _isSwipe = false;
                                }
                                else if (delta.x < -50) 
                                {
                                    InputDirection = InputDirection.Left;
                                    _isSwipe = false;
                                }
                            }
                            else
                            {
                                if (delta.y > 50) 
                                {
                                    InputDirection = InputDirection.Up;
                                    _isSwipe = false;
                                }
                                else if (delta.y < -50)
                                {
                                    InputDirection = InputDirection.Down;
                                    _isSwipe = false;
                                }
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        _isSwipe = false;
                        break;
                }
            }
        }
    }
}