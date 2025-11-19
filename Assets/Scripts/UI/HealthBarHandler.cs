using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HealthBarHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerScript playerScript;
        [SerializeField]
        private UIDocument uiDoc;
    
        private Label _mHealthLabel;
        private VisualElement _mHealthBarMask;
        private bool _subscribed;

        private void Awake()
        {
            if (!uiDoc) uiDoc = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            EnsureLabel();
            TrySubscribeToPlayer();
            HealthChanged();
        }

        private void OnDisable()
        {
            TryUnsubscribeFromPlayer();
        }

        public void HealthChanged()
        {
            if (!playerScript)
            {
                TrySubscribeToPlayer();
                if (!playerScript) return;
            }

            UpdateUI(playerScript.currentHealth, playerScript.maxHealth);
        }

        private void OnHealthChanged(int current, int max)
        {
            UpdateUI(current, max);
        }

        private void UpdateUI(int current, int max)
        {
            EnsureLabel();
            if (_mHealthLabel == null) return;

            max = Mathf.Max(1, max);

            _mHealthLabel.text = $"{current}/{max}";
            float healthRatio = Mathf.Clamp01((float)current / max);
            float healthPercent = Mathf.Lerp(0f, 100f, healthRatio);
            _mHealthBarMask.style.width = Length.Percent(healthPercent);
        }

        private void EnsureLabel()
        {
            if (_mHealthLabel != null && _mHealthBarMask != null) return;
            if (!uiDoc) uiDoc = GetComponent<UIDocument>();
            if (uiDoc && uiDoc.rootVisualElement != null)
            {
                _mHealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
                _mHealthBarMask = uiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
            }
        }

        private void TrySubscribeToPlayer()
        {
            if (_subscribed && playerScript) return;
            if (!playerScript)
            {
                playerScript = FindFirstObjectByType<PlayerScript>();
            }
            if (playerScript != null)
            {
                playerScript.OnHealthChanged += OnHealthChanged;
                _subscribed = true;
            }
        }

        private void TryUnsubscribeFromPlayer()
        {
            if (_subscribed && playerScript != null)
            {
                playerScript.OnHealthChanged -= OnHealthChanged;
            }
            _subscribed = false;
        }
    }
}