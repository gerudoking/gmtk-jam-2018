﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour {

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text waveTime;
    private int score = 0;

    private bool onWave = false;
    private Timer waveInterval;
    private int waveCount;

    public List<GameObject> waveEnemies;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

	// Use this for initialization
	void Start () {
        waveEnemies = new List<GameObject>();
        scoreText.text = "Score: 0";
        waveInterval = new Timer(Timer.TYPE.DECRESCENTE, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score " + score;
        if (onWave) {
            waveTime.text = "DEFEND YOURSELF!";
        }
        else {
            waveTime.text = "Next wave in " + waveInterval.GetTime().ToString("n0") + " seconds";
        }

        //Controle de Waves
        if (waveEnemies.Count == 0 && onWave) {
            onWave = false;
            waveInterval.Reset();
        }

        if (!waveInterval.Finished() && !onWave) {
            waveInterval.Update();
        }
        else {
            onWave = true;
            StartWave();
        }

        //TESTE!
        /*if (Input.GetKeyDown(KeyCode.E)) {
            
        }*/
	}

    private void StartWave() {
        print("Wave!");
    }
}