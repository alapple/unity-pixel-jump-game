using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    
    
    public PlayerScript playerScript;
    public UIDocument uiDoc;
    
    private Label _mHealthLabel;
    
    private void Start()
    {
        _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");

        HealthChanged();
    }

    void HealthChanged()
    {
        _mHealthLabel.text = $"{playerScript.currentHealth}/{playerScript.maxHealth}";
        float healthRatio = (float)playerScript.currentHealth / playerScript.maxHealth;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        _mHealthLabel.style.width = Length.Percent(healthPercent);
    }
}