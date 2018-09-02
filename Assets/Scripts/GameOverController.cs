using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string addscoreURL = "http://highscorejam.000webhostapp.com/addscore.php";    
		string postUrl = addscoreURL + "?name=" + WWW.EscapeURL(GlobalVariables.globalName) + "&score=" + GlobalVariables.globalScore;

		WWW hspost = new WWW(postUrl);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey) {
            SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        }
	}
}
