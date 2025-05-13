using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject Resume;
    public GameObject[] LevelsTexts;


    [Header("Int")]
    public int[] LevelStages;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LevelManager()
    {
        SceneManager.LoadScene("Farm");
    }

    public void ResumeMethod()
    {
        if (Resume.activeSelf)
        {
            Resume.gameObject.SetActive(false);
        }
        else
        {
            Resume.gameObject.SetActive(true);
        }
    }
    public void ExitToHome()
    {
        SceneManager.LoadScene("FarmMenu");
    }
}
