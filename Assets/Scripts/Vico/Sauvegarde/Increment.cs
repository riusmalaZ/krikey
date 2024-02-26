using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Increment : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textMeshProUGUI;
    public void Increments()
    {
        Test_Sauvegarde.instance.valeur++;
        
    }

    void Update()
    {
        textMeshProUGUI.text = Test_Sauvegarde.instance.valeur.ToString();
    }

    public void SceneLoader(string sceneCharge)
    {
        SceneManager.LoadScene(sceneCharge);
    }
}
