using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class OutOfBounds : MonoBehaviour
    {
        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            if (_collider2D != null && !_collider2D.isTrigger)
            {
                // Ensure this works as a trigger zone even if the flag wasn't set in the Inspector
                Debug.LogWarning("OutOfBounds collider was not set as Trigger. Enabling isTrigger at runtime.", this);
                _collider2D.isTrigger = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            Debug.Log("Player entered OutOfBounds trigger. Loading Menu scene...");
            SceneManager.LoadScene("RespawnMenu");
        }

        // Fallback in case the collider wasn't a trigger for some reason on the other object
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Player")) return;

            Debug.LogWarning("Player collided with OutOfBounds (non-trigger). Loading Menu scene...");
            SceneManager.LoadScene("RespawnMenu");
        }
    }
}
