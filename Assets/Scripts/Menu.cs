using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {

	int rectW = 120;
	int rectH = 160;
	int yOr = 400;
	int selected = 0;
	int state = 1;
	float alphaTitle = 0.0f;
	float [,] options = { { 1.0f, -1.0f}, { 1.0f, -1.0f}, { 1.0f, -1.0f} };
	string [] optName = { "PLAY", "LEADERBOARD", "QUIT"};

	// Use this for initialization
	void Start(){
		GlobalVariables.globalScore = 0;
 	}

	// Update is called once per frame
	void Update (){

		 if (Input.GetKeyUp("up"))
		 {
		 	selected--;
		 }
		 else if (Input.GetKeyUp("down"))
		 {
		 	selected++;	
		 }
		 selected = Mathf.Clamp( selected, 0, 2);

	}

	void OnGUI()
    {

    	if ( state == 1 )
    	{
    		if ( alphaTitle < 1.0f )
    		{
    			alphaTitle += 3f*Time.deltaTime;
    		}

	    	////Mouse Position
	        Camera  c = Camera.main;
	        Event   e = Event.current;
	        Vector2 mousePos = new Vector2();
	        mousePos.x = e.mousePosition.x;
	        mousePos.y = c.pixelHeight - e.mousePosition.y;
	        for( int i = 0; i < 3; i++)
	        {
	        	if(mousePos.x > Screen.width/2-rectW
	        	&& mousePos.x < Screen.width/2+rectW
	        	&& 720-mousePos.y > Screen.height*(400.0f/720.0f) + i*45
	        	&& 720-mousePos.y < Screen.height*(400.0f/720.0f) + i*45+44)
	        	{
	        		selected = i;
	        		break;
	        	}
	        }

	        if(Input.GetKeyDown("return") || Input.GetMouseButtonDown(0) && mousePos.x > Screen.width/2-rectW
			&& mousePos.x < Screen.width/2+rectW
			&& 720-mousePos.y > Screen.height*(400.0f/720.0f)
			&& 720-mousePos.y < Screen.height*(400.0f/720.0f)+rectH)
			 {
			 	switch( selected )
			 	{
			 		case 0:
			 			SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
			 			break;

			 		case 1:
			 			SceneManager.LoadScene("Scenes/Leaderboard", LoadSceneMode.Single);
			 			break;

			 		case 2:
			 			Application.Quit();
			 			break;
			 	}
			 	
			 }

	        GUI.skin.label.alignment = TextAnchor.UpperCenter;

	        ////Title
	        GUI.skin.label.font = (Font)Resources.Load("FontTitle");
	        GUI.contentColor = new Color( 253.0f/255.0f, 120.0f/255.0f, 2.0f/255.0f, 1.0f);
	        GUI.Label(new Rect( Screen.width/2-400, 60, 400*2, 200), "Main Menu");

			GUI.skin.label.font = (Font)Resources.Load("Font1");


	        ////Rect
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, new Color( 0, 0, 0, 0.8f));
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.skin.box.hover.background = texture;
			GUI.Box(new Rect( Screen.width/2-rectW, Screen.height*(400.0f/720.0f)+rectH/2-(rectH/2)*alphaTitle, rectW*2, rectH*alphaTitle), "");

	        ////Options
	        for( int i = 0; i < options.Length; i++)
	        {
		       	if(options[i,0] < 0.65f)
		    	{
		    		options[i,1] = 1.0f;
		    	}
		    	if(options[i,0] > 1.0f)
		    	{
		    		options[i,1]  = -1.0f;
		    	}
		    	options[i, 0] += 0.35f*Time.deltaTime*options[i,1];
		    	if ( i==selected )
		    	{
	       			GUI.contentColor = new Color( 253.0f/255.0f, 120.0f/255.0f, 2.0f/255.0f, options[i, 0]-(1.0f-alphaTitle));
	       		}
	       		else
	       		{
	       			GUI.contentColor = new Color( 1, 1, 1, 0.9f-(1.0f-alphaTitle));
	       		}
	        	GUI.Label(new Rect( Screen.width/2-rectW, Screen.height*(400.0f/720.0f)+10+i*45, rectW*2, 100), optName[i]);
	        }
	        GUI.contentColor = new Color( 1, 1, 1, 1);
	    }
    }
}
