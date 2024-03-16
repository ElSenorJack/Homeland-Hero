using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] int startingBlance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;


    public int CurrentBalance {  get { return currentBalance; } }
    

    void Awake()
    {
      currentBalance = startingBlance;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
        //Mathf.Absolute serve a impedire che il valore si negativizzi se viene inserito un valore negativo
        if (currentBalance > 1000) 
        {
            Invoke("LoadNextScene", 2f);
        }
    }



    public void Withdraw(int amount)
    { 
        currentBalance -= Mathf.Abs (amount);
        UpdateDisplay();

        if (currentBalance < 0)
        { 
            ReloadScene();
        }
    }

    void ReloadScene()
    { 
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

