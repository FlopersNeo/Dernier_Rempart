//Code by Néo Grapin

using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditsPage = null;

    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void Option()
    {
        SceneManager.LoadSceneAsync("OptionMenu");
    }

    public void Credits()
    {
        _creditsPage.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
