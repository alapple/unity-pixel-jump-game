using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool checkpointReached = false;
    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointReached = true;
            Debug.Log("checkpoint reached");
        }   
    }
}
