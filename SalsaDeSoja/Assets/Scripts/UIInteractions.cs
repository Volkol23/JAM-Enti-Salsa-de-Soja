using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInteractions : MonoBehaviour {
	
    public void LoadLevel()
    {
        SceneManager.LoadScene("Player");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
