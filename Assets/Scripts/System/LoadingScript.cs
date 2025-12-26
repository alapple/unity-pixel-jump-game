using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace System
{
    public class LoadingScript : MonoBehaviour
    {
        AsyncOperation _asyncOperation;
        private bool _loadingDone  = false;
        public Slider loadingBar;
        public TextMeshProUGUI loadingTip;

    
        void Start()
        {
            loadingBar.value = 0;
            StartCoroutine(LoadAsyncScene("Main"));
            DisplayLoadingTip();
        }

        IEnumerator LoadAsyncScene(string sceneName)
        {
            _asyncOperation =  SceneManager.LoadSceneAsync(sceneName);
            _asyncOperation.allowSceneActivation = false;

            while (loadingBar.value < 0.9f)
            {
                float targetProgress = Mathf.Min(_asyncOperation.progress / 0.9f, 1f);
                loadingBar.value = Mathf.MoveTowards(loadingBar.value, targetProgress, Time.deltaTime * 0.5f);
                    
                if (loadingBar.value >= 0.9f && _asyncOperation.progress >= 0.9f)
                {
                    _asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            _loadingDone = true;
        }

        void DisplayLoadingTip()
        {
            List<String> tips = new List<String>();
            tips.Add("State Fact: CEOs double their mistakes when profits drop.");
            tips.Add("Tip: The Hammer is slow, comrade, but it hits harder than a sudden market crash.");
            tips.Add("Announcement: Gravity is a capitalist construct. Defy it by launching enemies with the Hammer.");
            tips.Add("Remember: There is no 'I' in Team, but there is a 'Me' in Hammer.");
            tips.Add("Pause the game if you must. Even the revolution needs a coffee break.");
            tips.Add("Checkpoints are distributed equally among the levels.");
            loadingTip.text = $"{tips[UnityEngine.Random.Range(0, tips.Count)]}";
        }
    }
}
