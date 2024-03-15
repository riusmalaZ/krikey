using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void SceneLoad(string Name)
    {
        SceneManager.LoadScene(Name);
    }

    public void ZeroTimeScale()
    {
        Time.timeScale = 0f;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

    public void Quitte()
    {
        Application.Quit();
    }
}
