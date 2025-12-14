using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SaveData : MonoBehaviour
{
    public static SaveData Instance;
    public TMP_InputField InputName;
    public int HighScore;
    public string Name;
    public string HighName;
    public TextMeshProUGUI HighNameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // The 'SaveData' gameObject persists between scenes, destroying other instances in the same scene
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveName()
    {
        // Save the name that the user inputs in the InputField
        Name = InputName.text;
    }

    [System.Serializable]
    public class SavedData
    {
        // The specific data that gets stored into memory - the highest score and the username of the player carrying this score
        public int HighScore;
        public string HighName;
    }

    public void ShareData()
    {
        // Saves the two data values within the SavedData class into Json formatting
        SavedData data = new SavedData();
        data.HighName = HighName;
        data.HighScore = HighScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        // Loads the save data from a previous session, if applicable
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);
            HighName = data.HighName;
            HighScore = data.HighScore;
        }
    }



    //public void TopScore()
    //{
        //if (MainManager.Instance.m_HighPoints != 0)
        //{
            //HighScore = MainManager.Instance.m_HighPoints;
            //HighName = Preferences.Instance.Name;
        //}
    //}


}
