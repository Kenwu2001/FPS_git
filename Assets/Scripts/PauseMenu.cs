using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    // public Canvas canvasMenu;

    void Start()
    {
        PausePanel.SetActive(true);
        // canvasMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        // canvasMenu.enabled = true;
        Time.timeScale = 0;
    }

    public void Contunue()
    {
        PausePanel.SetActive(false);
        // canvasMenu.enabled = false;
        Time.timeScale = 1;
    }
}
