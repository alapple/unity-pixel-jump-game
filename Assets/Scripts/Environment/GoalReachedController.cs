using Environment;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalReachedController : MonoBehaviour
{
    public Checkpoint checkpoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && checkpoint.getCheckpointReached())
        {
            Debug.Log("Player entered Finish Flag trigger. Loading Menu scene...");
            SceneManager.LoadScene("WinScene");   
        }
    }
}
