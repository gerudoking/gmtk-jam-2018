using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour {

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text waveTime;
    [SerializeField]
    private Text life;
    private int score = 0;
    [SerializeField]
    private PlayerController player;

    private bool onWave = true;
    private Timer waveInterval;
    private int waveCount = 0;
    private Pathfinding pf;

    [SerializeField]
    private List<Transform> spawns;
    [SerializeField]
    private GameObject enemy;

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
        pf = GetComponent<Pathfinding>();

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
        life.text = player.hp.ToString("n0");

        //Controle de Waves
        if (waveEnemies.Count == 0 && onWave) {
            onWave = false;
            waveInterval.Reset();
            waveCount++;
        }

        if (!waveInterval.Finished() && !onWave) {
            waveInterval.Update();
        }
        else if(!onWave){
            onWave = true;
            StartWave();
        }

        //Pathfinding dos inimigos
        if (pf.recalculatePaths) {
            foreach (GameObject e in waveEnemies) {
                EnemyController ec = e.GetComponent<EnemyController>();

                ec.course = pf.FindPath(ec.transform.position, pf.target.position);
            }

            pf.recalculatePaths = false;
            GetComponent<CustomGrid>().CreateGrid();
        }
	}

    private void StartWave() {
        print("Wave!");
        pf.recalculatePaths = true;

        for (int i = 0; i < waveCount + 3; i++) {
            int randomNumber = UnityEngine.Random.Range(0, spawns.Count);
            GameObject aux = (GameObject)Instantiate(enemy, spawns[randomNumber].position, Quaternion.identity);
            waveEnemies.Add(aux);
        }

        print("Wave spawned!");
    }
}
