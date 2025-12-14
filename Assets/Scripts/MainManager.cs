using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_Name;
    public int m_HighPoints;
    public string m_HighTitle;
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        // Displays the current high score at the start of the game
        HighScoreText.text = "Best Score : " + SaveData.Instance.HighName + " : " + SaveData.Instance.HighScore;
    }

    private void Update()
    {
        //HighScoreText.text = $"Best Score : {Preferences.Instance.HighName} : {Preferences.Instance.HighScore}";
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        // If the player's total score is higher than the current high score:
        if (m_Points > SaveData.Instance.HighScore)
        {
            // Override the current values stored in the SaveData script with the new score and player name
            SaveData.Instance.HighName = SaveData.Instance.Name;
            SaveData.Instance.HighScore += point;
            // Display this new high score during gameplay
            HighScoreText.text = "Best Score : " + SaveData.Instance.HighName + " : " + SaveData.Instance.HighScore;
            // Call "ShareData()" to save the values into Json formatting
            SaveData.Instance.ShareData();
        }
        // Displays the current high score during gameplay
        HighScoreText.text = "Best Score : " + SaveData.Instance.HighName + " : " + SaveData.Instance.HighScore;
    }

    public void Exit()
    {
        // When the player presses the quit button, after exit out of Unity Editor's play mode, or otherwise quit out of the game entirely
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
