//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Preferences : MonoBehaviour
{
    public static Preferences Instance;
    public int HighScore;
    public string Name;
    public string HighName;
    public TMP_InputField InputName;
    public TextMeshProUGUI HighNameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Directly loads the high score data from the static SaveData class
        SaveData.Instance.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the highest score in the first room
        OverallTop();
    }

    public void SaveName()
    {
        // Saves the player's inputted name
        Name = InputName.text;
    }

    public void StartNew()
    {
        // Loads the main gameplay scene when the player presses the 'Start' button
        SceneManager.LoadScene(1);
    }

    public void OverallTop()
    {
        //HighNameText.text = $"Best Score : {SaveData.Instance.HighName} : {SaveData.Instance.HighScore}";
        // The text that displays the best scoring
        HighNameText.text = "Best Score : " + SaveData.Instance.HighName + " : " + SaveData.Instance.HighScore;
    }
    public void Exit()
    {
        // Either exits the Unity Editor's play mode or quits the game entirely
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
