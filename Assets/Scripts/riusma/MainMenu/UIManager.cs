using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static System.Net.Mime.MediaTypeNames;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject actualCanva;
    
    public void SwitchCanva(GameObject newCanva)
    {
        actualCanva.SetActive(false);
        newCanva.SetActive(true);
        actualCanva = newCanva;
    }

    public void LoadRogue()
    {
        SceneManager.LoadScene("Rogue");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
