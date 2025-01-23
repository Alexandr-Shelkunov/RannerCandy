using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Alexender.Runer.UI
{
    public class Pause : MonoBehaviour
    {
        public void PauseGame()
        {
            Time.timeScale = 0.0F;
        }
    }
}
