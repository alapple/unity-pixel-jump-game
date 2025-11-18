using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class LoadingScript : MonoBehaviour
{
    AsyncOperation _asyncOperation;
    bool _loadingDone  = false;
    public Slider loadingBar;
    public TextMeshProUGUI loadingTip;
    public Image spinner;
    public float spinSpeed;

    void Start()
    {
        loadingBar.value = 0;
        StartCoroutine(LoadAsyncScene());
        DisplayLoadingTip();
    }

    IEnumerator LoadAsyncScene()
    {
        _asyncOperation =  SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        _asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
            print(_asyncOperation.progress);
            if (_asyncOperation.progress >= 0.9f)
            {
                _asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        _loadingDone = _asyncOperation.isDone;
    }

    private void Update()
    {
        if (!_loadingDone)
        {
            loadingBar.value = _asyncOperation.progress * 10;
        }
        
        transform.eulerAngles += new Vector3(0,0, Time.deltaTime * spinSpeed);
    }

    void DisplayLoadingTip()
    {
        List<String> tips = new List<String>();
        Random rnd = new Random();
        tips.Add("State Fact: CEOs double their mistakes when profits drop.");
        tips.Add("Tip: The Hammer is slow, comrade, but it hits harder than a sudden market crash.");
        tips.Add("Announcement: Gravity is a capitalist construct. Defy it by launching enemies with the Hammer.");
        tips.Add("Remember: There is no 'I' in Team, but there is a 'Me' in Hammer.");
        tips.Add("Pause the game if you must. Even the revolution needs a coffee break.");
        tips.Add("Checkpoints are distributed equally among the levels.");
        loadingTip.text = $"{tips[rnd.Next(0, tips.Count)]}";
    }
}
