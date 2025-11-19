using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class OutOfBounds : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene("Menu");
      
        }
    }
}
