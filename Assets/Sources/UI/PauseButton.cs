using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alexender.Runer.UI
{
    public class PauseButton
    {
        public void pause()
        {
            Time.timeScale = 0f;
        }
    }
}