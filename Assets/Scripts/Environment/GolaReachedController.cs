using Environment;
using UnityEngine;

public class GoalReachedController : MonoBehaviour
{
    public Checkpoint checkpoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && checkpoint.getCheckpointReached())
        {
            Debug.Log("Player reached Goal");
        }
    }
}
