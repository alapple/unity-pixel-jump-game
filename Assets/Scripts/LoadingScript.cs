using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingScript : MonoBehaviour
{
    AsyncOperation _asyncOperation;
    bool _loadingDone  = false;
    public Slider loadingBar;
    public TextMeshProUGUI loadingTip;

    
    void Start()
    {
        loadingBar.value = 0;
        StartCoroutine(LoadAsyncScene("Main"));
        DisplayLoadingTip();
    }

    IEnumerator LoadAsyncScene(string SceneNameame)
    {
        _asyncOperation =  SceneManager.LoadSceneAsync(SceneNameame);
        _asyncOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(2);
        while (loadingBar.value < 0.7)
        {
            loadingBar.value += Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }

        while (!_asyncOperation.isDone)
        {
            loadingBar.value = _asyncOperation.progress;
            if (_asyncOperation.progress >= 0.9f)
            {
                _asyncOperation.allowSceneActivation = true;
                loadingBar.value = 1;
            }
            yield return null;
        }

        _loadingDone = _asyncOperation.isDone;
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
            loadingTip.text = $"{tips[Random.Range(0, tips.Count)]}";
    }
}
