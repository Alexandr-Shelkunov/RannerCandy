using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public class KeyboardInput : IInput, IUpdatable
    {
        public InputDirection InputDirection { get; private set; }

        public int Priority => UpdatePriorityList.INPUT;

        public void DoUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InputDirection = InputDirection.Left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                InputDirection = InputDirection.Right;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                InputDirection = InputDirection.Up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                InputDirection = InputDirection.Down;
            }
            else
            {
                InputDirection = InputDirection.None;
            }
        }
    }
}
