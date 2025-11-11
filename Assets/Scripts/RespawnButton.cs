using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
{
    private Button respawnButton;

    void Start()
    {
        respawnButton = GetComponent<Button>();
        respawnButton.onClick.AddListener(OnButtonClicked);

        // Check if the Respawn singleton is available
        if (Respawn.Instance == null)
        {
            Debug.LogError("Respawn.Instance is null! Make sure the Respawn script is active in a loaded scene and its Awake method has run.");
        }
    }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        Debug.Log("Respawn button clicked");
        if (Respawn.Instance != null) // Access the singleton instance
        {
            Respawn.Instance.RespawnPlayer();
        }
        else
        {
            Debug.LogError("Respawn manager not available!");
        }
    }

    private void OnDestroy()
    {
        if (respawnButton != null)
        {
            respawnButton.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
