using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    private Button _playButton;
    void Start()
    {
        _playButton = GetComponent<Button>();
        _playButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene("Main",  LoadSceneMode.Single);
    }
}
