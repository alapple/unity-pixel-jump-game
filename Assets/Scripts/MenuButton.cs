using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button _menuButton;
    void Start()
    {
     _menuButton = GetComponent<Button>();
     _menuButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
