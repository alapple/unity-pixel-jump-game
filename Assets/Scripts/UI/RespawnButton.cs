using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RespawnButton : MonoBehaviour
    {
        private Button respawnButton;

        void Awake() // Changed to Awake, good for getting component references
        {
            respawnButton = GetComponent<Button>();
            respawnButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Debug.Log("Loading Main scene...");
            // This button's ONLY job is to load the scene.
            // The RespawnManager in the "Main" scene will handle spawning.
            SceneManager.LoadScene("Main", LoadSceneMode.Single); 
        }

        private void OnDestroy()
        {
            if (respawnButton != null)
            {
                respawnButton.onClick.RemoveListener(OnButtonClicked);
            }
        }
    }
}