using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleAPI_Manager : MonoBehaviour {

    [SerializeField]
    private GameManager m_Manager;

    [SerializeField]
    private RawImage m_PlayerIcon;

    private bool m_PlayerUsesGooglePlay = false;
    private bool m_UserLoggedIn = false;

    string error;

	// Use this for initialization
	void Start () {
        //GameManager.OnSettingLoaded += InitializeGooglePlayAPI;
        InitializeGooglePlayAPI();
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.skin.label.fontSize = Screen.width / 40;

        //GUILayout.Label("Username: " + PlayGamesPlatform.Instance.localUser.userName);
        GUILayout.Label("Username: " + Social.localUser.userName);
        GUILayout.Label("Status: " + error);

       

    }

    void InitializeGooglePlayAPI()
    {
        if (true)
        {
            m_PlayerUsesGooglePlay = true;

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            //SignIn();
            //Social.ShowLeaderboardUI();
        }
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

        }
    }

    public void SignInCallback(bool success, string data)
    {
        Debug.Log("[Abyss Login]: " + data);
        error = data;
        if (success)
        {
            Debug.Log("(Lollygagger) Signed in!");
            // Change sign-in button text
            //signInButtonText.text = "Sign out";

            // Show the user's name
            //authStatus.text = "Signed in as: " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("(Lollygagger) Sign-in failed...");

            // Show failure message
            //signInButtonText.text = "Sign in";
            //authStatus.text = "Sign-in failed";
        }

        Texture2D playerIcon = PlayGamesPlatform.Instance.localUser.image;
        m_PlayerIcon.texture = playerIcon;
    }
}
