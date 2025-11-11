using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Static instance of Respawn, accessible from anywhere
    public static Respawn Instance { get; private set; }

    private void Awake() // Awake is called before Start, good for initialization
    {
        if (Instance != null && Instance != this)
        {
            // If another instance already exists, destroy this one
            Debug.LogWarning("Found more than one Respawn instance! Destroying this duplicate.");
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
            // Optionally, if you want this RespawnManager to persist across scene loads:
            // DontDestroyOnLoad(gameObject); 
            Debug.Log("Respawn instance set!");
        }
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn player (called via Singleton)");
        // Add your actual respawn logic here
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; // Clear the instance when this object is destroyed
        }
    }
}