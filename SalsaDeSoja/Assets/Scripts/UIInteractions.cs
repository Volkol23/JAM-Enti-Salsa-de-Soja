using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInteractions : MonoBehaviour {

    public GameObject buttons;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            buttons.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
