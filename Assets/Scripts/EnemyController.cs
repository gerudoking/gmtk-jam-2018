using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Pathfinding pathF;

    public List<Node> course;

    private Rigidbody2D r;
    private float moveRate = 5.0f;
    private bool moved = false;

	// Use this for initialization
	void Start () {
        course = new List<Node>();
        pathF = GameObject.Find("Manager").GetComponent<Pathfinding>();
        r = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(course.Count > 0) {
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
	}
}
