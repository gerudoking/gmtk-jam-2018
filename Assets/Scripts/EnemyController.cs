using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Pathfinding pathF;

    public List<Node> course;

    private Rigidbody2D r;
    private float moveRate = 5.0f;
    private bool moved = false;
    private float hp = 2.0f;
    private Timer damageFade;
    private bool damageFading = false;

	// Use this for initialization
	void Start () {
        course = new List<Node>();
        pathF = GameObject.Find("Manager").GetComponent<Pathfinding>();
        r = GetComponent<Rigidbody2D>();
        damageFade = new Timer(Timer.TYPE.CRESCENTE, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if(course != null && course.Count > 0) {
            /*if (!moved) {
                Vector3 dir = course[0].worldPosition - transform.position;
                dir = dir.normalized;

                r.AddForce(dir * moveRate * Time.fixedDeltaTime);
                moved = true;
            }*/
            transform.position = Vector3.MoveTowards(transform.position, course[0].worldPosition, moveRate * Time.fixedDeltaTime);

            print(course[0].worldPosition);
            if (Vector3.Distance(transform.position, course[0].worldPosition) < 0.05f) {
                /*r.velocity = Vector3.zero;
                r.angularVelocity = 0;*/

                course.RemoveAt(0);
                print("Removed!");
                moved = false;
            }
        }
        else {
            course = pathF.FindPath(transform.position, pathF.target.position);
        }

        if (damageFading && !damageFade.Finished()) {
            damageFade.Update();
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.white;
            damageFading = false;
            damageFade.Reset();
        }

        //Vida
        if(hp <= 0) {
            GameObject.Find("Manager").GetComponent<GeneralController>().waveEnemies.Remove(gameObject);
            GameObject.Find("Manager").GetComponent<GeneralController>().Score++;
            int randomNumber = UnityEngine.Random.Range(20, 41);
            GameObject.Find("Shop").GetComponent<Shop>().gold += randomNumber;
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        print("collided");
        if (collision.gameObject.tag == "slider") {
            if (collision.gameObject.GetComponent<Movable>().sliding) {
                hp--;
                GetComponent<SpriteRenderer>().color = Color.red;
                GetComponent<AudioSource>().Play();
                damageFading = true;
            }
        }

        if(collision.gameObject.tag == "orb" && !damageFading) {
            hp--;
            GetComponent<SpriteRenderer>().color = Color.red;
            damageFading = true;
        }
    }
}
