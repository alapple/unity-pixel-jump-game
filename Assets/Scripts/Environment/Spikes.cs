using Player;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField]
        private PlayerScript player;
        [SerializeField] 
        private HealthBarHandler healthBarHandler;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                player.ChangeHealth(-1);
                healthBarHandler.HealthChanged();
                Debug.Log("entered spikes");
            }
        }
    }
}