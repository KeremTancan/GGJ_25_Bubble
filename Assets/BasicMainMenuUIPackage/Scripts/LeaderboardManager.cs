using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using Dan.Main;
using System;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;

    
    public static LeaderboardManager Instance;


    private void Awake() {

        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        for (int i = 0; i < names.Count; i++)
        {
            names[i].text = "";
            scores[i].text = "";
        }
    }

    private void Start() {
        
    }



    string leaderboardPublicKey = "dfba1af5244ea79eff86f91bf0cee8c3badb862e25aba3ec7ee59058d1ab80f6";
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(leaderboardPublicKey,((msg)=>
        {
            int loopLength = (msg.Length<names.Count)?msg.Length:names.Count;
            //Debug.Log(msg.Length);

            int a = 0;

            for (int i = 0; i < loopLength; i++)
            {
                Debug.Log(msg[i].Username  + " " +msg[i].Score);
                if(msg[i].Score <= 0)
                {
                    continue;
                }
                names[a].text = msg[i].Username;

                TimeSpan t = TimeSpan.FromMilliseconds(msg[i].Score*10);
                string str = string.Format("{0:D2}:{1:D2}:{2:D3}", 
                t.Minutes, 
                t.Seconds, 
                t.Milliseconds);

                scores[a].text = str;
                a++;
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(leaderboardPublicKey, username,score,((msg)=>
        {
            GetLeaderboard();
        }));
    }

    public void IsUsernameAlreadyExist(string username, Action goNext, Action<bool> fail)
    {

        LeaderboardCreator.GetLeaderboard(leaderboardPublicKey, ((msg) =>
        {
            bool isFail = false;
            for (int i = 0; i < msg.Length; i++)
            {
                if(msg[i].Username == username)
                {   
                    isFail = true;
                    break;
                }
            }

            if(!isFail)
            {
                goNext();
            }
            else{
                fail(true);
            }
        }));
    }

    


}
