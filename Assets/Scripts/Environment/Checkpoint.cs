using UnityEngine;

namespace Environment
{
    public class Checkpoint : MonoBehaviour
    {
        private bool _checkpointReached = false;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _checkpointReached = true;
                Debug.Log("checkpoint reached");
            }   
        }
    }
}
