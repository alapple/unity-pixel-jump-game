using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RespawnButton : MonoBehaviour
    {
        private Button respawnButton;
        AsyncOperation _asyncOperation;

        void Awake() // Changed to Awake, good for getting component references
        {
            respawnButton = GetComponent<Button>();
            respawnButton.onClick.AddListener(OnButtonClicked);
            _asyncOperation.allowSceneActivation = false;

        }

        private void OnButtonClicked()
        {
            Debug.Log("Loading Main scene...");
            // This button's ONLY job is to load the scene.
            // The RespawnManager in the "Main" scene will handle spawning.
            _asyncOperation = SceneManager.LoadSceneAsync("LoadingScreen");
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