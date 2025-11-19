using System.Collections;
using UnityEngine;

// For IEnumerator

namespace Gameplay
{
    public class Respawn : MonoBehaviour
    {
        public static Respawn Instance { get; private set; }

        [Header("Respawn Settings")]
        public Transform respawnPoint; 
        public float respawnDelay = 0f; 
        public GameObject playerPrefab; 

        private GameObject _currentPlayerInstance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Found more than one Respawn instance! Destroying this duplicate.", this);
                Destroy(gameObject); 
            }
            else
            {
                Instance = this;
                // Uncomment if this RespawnManager should persist across ALL scene loads
                // DontDestroyOnLoad(gameObject);
                Debug.Log("Respawn instance set up!");
            }
        }

        void Start() // This Start method will run *after* the Main scene is fully loaded
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player Prefab not assigned to Respawn Manager! Cannot initialize player.", this);
                return;
            }

            if (respawnPoint == null)
            {
                Debug.LogWarning("Respawn Point not assigned to Respawn Manager! Using default (0,0,0).", this);
                GameObject defaultRespawnObj = new GameObject("DefaultRespawnPoint");
                respawnPoint = defaultRespawnObj.transform;
                respawnPoint.position = new Vector3(-3.8243f,-0.572f,0.05456218f);
            }

            // IMPORTANT: Call the spawn routine here to spawn the player when the scene loads
            // Use a coroutine even if delay is 0, to ensure all other Start() methods have a chance to run
            StartCoroutine(RespawnRoutine(respawnDelay)); 
        }

        public void RespawnPlayer()
        {
            // This method can be called later for in-game respawns (e.g., after player dies)
            if (_currentPlayerInstance != null)
            {
                Destroy(_currentPlayerInstance);
            }
            StartCoroutine(RespawnRoutine(respawnDelay));
        }

        private IEnumerator RespawnRoutine(float delay)
        {
            if (delay > 0)
            {
                Debug.Log($"Waiting {delay} seconds to spawn/respawn player...");
                yield return new WaitForSeconds(delay);
            }
            else
            {
                // Even with 0 delay, yielding for a frame allows other initializations
                yield return null; 
            }

            SpawnPlayer();
            Debug.Log("Player spawned!");
        }

        private void SpawnPlayer()
        {
            if (!playerPrefab || !respawnPoint)
            {
                Debug.LogError("Player Prefab or Respawn Point is null! Cannot spawn player.", this);
                return;
            }

            if (!_currentPlayerInstance && !playerPrefab)
            {
                _currentPlayerInstance = Instantiate(playerPrefab, respawnPoint.position, respawnPoint.rotation);
                _currentPlayerInstance.name = "Player (Active)";
            }
        
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}