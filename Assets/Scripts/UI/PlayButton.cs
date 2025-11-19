using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class Play : MonoBehaviour
    {
        private Button _playButton;
        AsyncOperation _asyncOperation;
        void Start()
        {
            _playButton = GetComponent<Button>();
            _playButton.onClick.AddListener(OnButtonClick);
            _asyncOperation = SceneManager.LoadSceneAsync("LoadingScreen");
            _asyncOperation.allowSceneActivation = false;
        }

        private void OnButtonClick()
        {
            _asyncOperation.allowSceneActivation = true;
        }
    }
}
