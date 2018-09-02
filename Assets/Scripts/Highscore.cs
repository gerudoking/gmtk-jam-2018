using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour
{
    int rectW = 400;
    float timer = 0.0f;
    private string highscoreURL = "http://highscorejam.000webhostapp.com/highscore.php";
    private string addscoreURL = "http://highscorejam.000webhostapp.com/addscore.php";    
 	string result = "";
    void Start()
    {
        StartCoroutine(GetScores());
        //StartCoroutine(PostScores("Mizore", 91));
    }
    void Update()
    {
        timer += Time.deltaTime;
        if ( timer > 2.0f )
        {
            timer = 0.0f;
            StartCoroutine(GetScores());
        }
        if(Input.GetMouseButtonUp(0)
        && Input.mousePosition.x > Screen.width/2-200
        && Input.mousePosition.x < Screen.width/2+200
        && Screen.height-Input.mousePosition.y > Screen.height*0.8f-10
        && Screen.height-Input.mousePosition.y < Screen.height*0.8f+50)
        {
            SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        }       
    }
	void OnGUI()
	{
        ////Rect
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color( 0, 0, 0, 0.85f));
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.skin.box.hover.background = texture;
        GUI.Box(new Rect( Screen.width/2-rectW, 0, rectW*2, Screen.height), "");

		GUI.skin.label.font = (Font)Resources.Load("FontGold");
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.contentColor = new Color( 255.0f/255.0f, 253.0f/255.0f, 153.0f/255.0f, 1);
        GUI.Label(new Rect( 0, Screen.height*0.2f, Screen.width, 500), result);
        GUI.contentColor = new Color( 1, 1, 1, 1);
        GUI.Label(new Rect( 0, Screen.height*0.05f, Screen.width, 500), "LEADERBOARD");
        if(Input.mousePosition.x > Screen.width/2-200
        && Input.mousePosition.x < Screen.width/2+200
        && Screen.height-Input.mousePosition.y > Screen.height*0.8f-10
        && Screen.height-Input.mousePosition.y < Screen.height*0.8f+50)
        {
            GUI.contentColor = new Color( 253.0f/255.0f, 120.0f/255.0f, 2.0f/255.0f, 1);
        }
        else
        {
            GUI.contentColor = new Color( 1, 1, 1, 1);
        }
        GUI.Label(new Rect( 0, Screen.height*0.8f, Screen.width, 500), "RETURN TO MAIN MENU");
	}

    IEnumerator GetScores()
    {
        WWW hsGet = new WWW(highscoreURL);
        yield return hsGet;
 
        if (hsGet.error != null)
        {

        }
        else
        {
            result = hsGet.text; 
        }
    }
 
    ////Usar StartCoroutine
    IEnumerator PostScores(string name, int score)
    {
        string postUrl = addscoreURL + "?name=" + WWW.EscapeURL(name) + "&score=" + score;

        WWW hspost = new WWW(postUrl);
        yield return hspost;
    }

}