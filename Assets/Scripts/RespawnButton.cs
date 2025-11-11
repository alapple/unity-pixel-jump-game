using UnityEngine;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
{
    public Button respawnButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnButton = GetComponent<Button>();
        respawnButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        Debug.Log("Respawn button clicked");
    }
    
}
