using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public enum InputDirection
    {
        Right,
        Left,
        Down,
        Up,

        None
    }

    public interface IInput
    {
        public InputDirection InputDirection { get; }
    }
}
