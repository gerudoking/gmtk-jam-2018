using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class GlobalVariables{
	public static string globalName = "ENTER YOUR NAME";
	public static int globalScore = 0;
}

public class GetName : MonoBehaviour
{
    void OnGUI()
    {
    	GUI.skin.textField.font = (Font)Resources.Load("Font1");
        GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
        GlobalVariables.globalName = GUI.TextField(new Rect(Screen.width/2-200, Screen.height/2-20, 400, 40), GlobalVariables.globalName, 20);
        if(GlobalVariables.globalName != "ENTER YOUR NAME")
		{
			GUI.skin.label.alignment = TextAnchor.UpperCenter;
			GUI.skin.label.font = (Font)Resources.Load("FontGold");
	        if(Input.mousePosition.x > Screen.width/2-75
	        && Input.mousePosition.x < Screen.width/2+75
	        && Screen.height-Input.mousePosition.y > Screen.height*0.6f-10
	        && Screen.height-Input.mousePosition.y < Screen.height*0.6f+50)
	        {
	            if(Input.GetMouseButtonUp(0))
        		{
        			SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        		}
	            GUI.contentColor = new Color( 253.0f/255.0f, 120.0f/255.0f, 2.0f/255.0f, 1);
	        }
	        else
	        {
	            GUI.contentColor = new Color( 1, 1, 1, 1);
	        }
	        GUI.Label(new Rect( 0, Screen.height*0.6f, Screen.width, 500), "CONTINUE");
    	}
	}
}