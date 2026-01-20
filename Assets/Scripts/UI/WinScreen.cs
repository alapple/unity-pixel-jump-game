using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class WinScreen : MonoBehaviour
    {
        public void SwitchToMainScene(string targetScene)
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}