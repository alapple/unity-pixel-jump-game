using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuitButton : MonoBehaviour
    {
        private Button _quitButton;
        void Start()
        {
            _quitButton = GetComponent<Button>();
            _quitButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            print("Quit");
            Application.Quit();
        }
    }
}
