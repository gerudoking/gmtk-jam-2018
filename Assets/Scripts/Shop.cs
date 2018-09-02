using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	// Use this for initialization
	int state = 1;
	int iconPosX = 20;
	int iconPosY = 20;
	int rectW = 350;
	int rectH = 200;
	int sel = -1;
	public int gold = 0;

	float margin;

	int[] prices = { 10, 20, 30, 40, 50};

	string[,] options = { {"Psyboost", "Increase Attack Speed", "Textures/ShopIcon", "Textures/ShopIcon2"},
	{"Life Flask", "Recover 1 HP", "Textures/ShopIcon", "Textures/ShopIcon2"},
	{"Jet Boots", "Improve Movement Speed", "Textures/ShopIcon", "Textures/ShopIcon2"},
	{"Mind Orb", "Alternate Protection and Attack", "Textures/ShopIcon", "Textures/ShopIcon2"},
	{"Power Armor", "Reduce Damage Taken by Half", "Textures/ShopIcon", "Textures/ShopIcon2"}};

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonUp(0) 
        && Input.mousePosition.x > iconPosX
        && Input.mousePosition.x < iconPosX+96
        && Screen.height-Input.mousePosition.y > iconPosY
        && Screen.height-Input.mousePosition.y < iconPosY+81
        )
        {
			state = -state;
        }

        margin = ( rectW*2.0f ) - ( (96.0f+32.0f)*options.GetLength(0) );

		for( int i = 0; i < options.GetLength(0); i++)
		{
			if(Input.mousePosition.x > Screen.width/2-rectW+margin/2+(i*(96+32))+14
        	&& Input.mousePosition.x < Screen.width/2-rectW+margin/2+(i*(96+32))+14+96
        	&& Screen.height-Input.mousePosition.y > Screen.height/2-rectH+64
        	&& Screen.height-Input.mousePosition.y < Screen.height/2-rectH+64+81)
        	{
        		if(Input.GetMouseButtonUp(0))
        		{
        			sel = i;
        		}
        	}
		}

		if( sel != -1 )
		{
			if(Input.mousePosition.x > Screen.width/2-65
	    	&& Input.mousePosition.x < Screen.width/2+65
	    	&& Screen.height-Input.mousePosition.y > Screen.height/2+rectH-80
	    	&& Screen.height-Input.mousePosition.y < Screen.height/2+rectH-90+30)
	    	{
	    		if(Input.GetMouseButtonUp(0) && gold>=prices[sel] && prices[sel] > -1)
	    		{
    				gold -= prices[sel];
    				switch( sel )
    				{
    					////Ação da compra de cada item
    					case 0:
                            GameObject.Find("Player").GetComponent<PlayerController>().psyMove *= 1.5f;
                            prices[sel] = -1; //Seta o preço pra -1, tornando-o incrompravel
    						break;

    					case 1:
                            GameObject.Find("Player").GetComponent<PlayerController>().hp++;
    						////Não seta o preço pra -1 para que o player possa comprar novamente
    						break;

    					case 2:
                            GameObject.Find("Player").GetComponent<PlayerController>().moveRate *= 1.5f;
                            prices[sel] = -1; //Seta o preço pra -1, tornando-o incrompravel
    						break;

    					case 3:
                            GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
    						prices[sel] = -1; //Seta o preço pra -1, tornando-o incrompravel
    						break;

    					case 4:
                            GameObject.Find("Player").GetComponent<PlayerController>().armored = true;
    						prices[sel] = -1; //Seta o preço pra -1, tornando-o incrompravel
    						break;
    				}
	    		}
	    	}
    	}

	}

	void OnGUI()
	{

		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.font = (Font)Resources.Load("FontGold");
		GUI.contentColor = new Color( 255.0f/255.0f, 253.0f/255.0f, 153.0f/255.0f, 1);
		GUI.Label(new Rect( Screen.width-150, 20, 100, 100), ""+gold+"$");

		GUI.skin.box.normal.background = Resources.Load<Texture2D>("Textures/ShopIcon");
		GUI.skin.box.hover.background = Resources.Load<Texture2D>("Textures/ShopIcon2");
		GUI.Box(new Rect( iconPosX, iconPosY, 96, 81), "");

		if( state == -1 )
		{
	        ////Title
	        GUI.skin.label.alignment = TextAnchor.UpperCenter;
	        GUI.skin.label.font = (Font)Resources.Load("FontTitle");
	        GUI.contentColor = new Color( 0, 0, 0, 0.95f);
	        GUI.Label(new Rect( Screen.width/2-400, 0, 400*2, 200), "SHOP");

	        ////Rect
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, new Color( 0, 0, 0, 1));
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.skin.box.hover.background = texture;
			GUI.Box(new Rect( Screen.width/2-rectW, Screen.height/2-rectH, rectW*2, rectH*2), "");

			for( int i = 0; i < options.GetLength(0); i++)
			{
				GUI.skin.box.normal.background = Resources.Load<Texture2D>(options[i, 2]);
				GUI.skin.box.hover.background = Resources.Load<Texture2D>(options[i, 3]);
				margin = ( rectW*2.0f ) - ( (96.0f+32.0f)*options.GetLength(0) );
				GUI.Box(new Rect( Screen.width/2-rectW+margin/2+(i*(96+32))+14, Screen.height/2-rectH+64, 96, 81), "");
			}

			if( sel != -1)
			{
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				GUI.skin.label.font = (Font)Resources.Load("Font1");
				GUI.contentColor = new Color( 1, 0.8f, 0.8f, 1.0f);
				GUI.Label(new Rect( Screen.width/2-rectW, Screen.height/2+rectH-230, rectW*2, rectH*2), options[sel, 0]);	
				GUI.contentColor = new Color( 1, 1, 1, 1.0f);
				GUI.Label(new Rect( Screen.width/2-rectW, Screen.height/2+rectH-195, rectW*2, rectH*2), options[sel, 1]);
				GUI.contentColor = new Color( 1, 1, 1, 0.95f);	
				if( prices[sel] > -1)
				{
					GUI.Label(new Rect( Screen.width/2-rectW, Screen.height/2+rectH-90, rectW*2, rectH*2), "BUY $"+prices[sel]);
				}
				else
				{
					GUI.Label(new Rect( Screen.width/2-rectW, Screen.height/2+rectH-90, rectW*2, rectH*2), "SOLD");
				}
			}
		}

	}

}
