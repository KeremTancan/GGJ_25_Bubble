using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class NormalMainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameScoreText;


    private void Start() {
        ShowUsernameScore();
        //LeaderboardManager.Instance.SetLeaderboardEntry(PlayerPrefs.GetString("Username"),PlayerPrefs.GetInt("BestScore"));
    }
    void ShowUsernameScore()
    {

        TimeSpan t = TimeSpan.FromMilliseconds(PlayerPrefs.GetInt("BestScore")*10);
        string str = string.Format("{0:D2}:{1:D2}:{2:D3}", 
                t.Minutes, 
                t.Seconds, 
                t.Milliseconds);
        
        
        usernameScoreText.text = "Username: " + PlayerPrefs.GetString("Username") 
                                + "\nBest Time: " + str;
                                
    }

    public void PlayGame()
    {
        if(PlayerPrefs.HasKey("Tutorial"))
        {
            SceneManager.LoadScene("Level1");

        }
        else{
            
            SceneManager.LoadScene("TutorialLevel");
        }

        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnEnable() {
        //Debug.Log(PlayerPrefs.GetString("Username")+ " "+PlayerPrefs.GetInt("BestScore"));
        if(PlayerPrefs.GetInt("BestScore")!=0)
        {
            LeaderboardManager.Instance.SetLeaderboardEntry(PlayerPrefs.GetString("Username"),PlayerPrefs.GetInt("BestScore"));
        }
        
        ShowUsernameScore();
    }
}
