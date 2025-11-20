using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RespawnButton : MonoBehaviour
    {
        private Button respawnButton;

        void Awake()
        {
            respawnButton = GetComponent<Button>();
            respawnButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single); 
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