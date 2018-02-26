using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void LoadScene(string scene)
    {
        Fade.Instance.FadeIn(() => SceneManager.LoadScene(scene));
    }
}