using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexander.RunnerCandy
{
    public enum InputDirection
    {
        None,

        Right,
        Left,
        Down,
        Up,
    }

    public interface IInput
    {
        public InputDirection InputDirection { get; }
    }
}
