//Code by Néo Grapin

using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OptionMenuUI : MonoBehaviour
{
    public void Controls()
    {
        SceneManager.LoadSceneAsync("Controls");
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
