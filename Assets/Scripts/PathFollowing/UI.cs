using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    public Toggle myToggle;
    // Start is called before the first frame update
    void Start()
    {
        myToggle.isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToCoba1()
    {
        if(myToggle.isOn)
        {
            SceneManager.LoadScene("COBA1", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("COBA2");
        }
        else
        {
            SceneManager.LoadScene("COBA2", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("COBA1");
        }
    }

}
