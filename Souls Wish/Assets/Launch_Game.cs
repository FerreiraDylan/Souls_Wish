using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launch_Game : MonoBehaviour
{
    public string scene;

    public void PlayScene()
    {
        if (scene != null)
            SceneManager.LoadScene(scene);
    }
}
