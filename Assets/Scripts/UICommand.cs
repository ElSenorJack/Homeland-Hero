using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICommand : MonoBehaviour
{
    TextMeshProUGUI instructions;
    new AudioSource audio;

    void Start()
    {
        instructions = GetComponent<TextMeshProUGUI>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.R))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (instructions.enabled == true)
            { instructions.enabled = false; }
            else
            { instructions.enabled = true;  }
           
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            audio.mute = !audio.mute; 
        }      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
