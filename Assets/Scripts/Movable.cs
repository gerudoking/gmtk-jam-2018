using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {
    private Vector3 lastMousePosition;
    private bool dragging = false;
    private Vector3 mVelocity = Vector3.zero;
    private float mMovementSmoothing = .05f;
    private float moveRate = 2000.0f;
    private Vector3 stoppedPos;
    public bool sliding = false;

    private void Start() {
        stoppedPos = transform.position;
    }

    private void Update() {
        if (sliding == false) {
            transform.position = stoppedPos;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        /*if(GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f && sliding == true) {
            Grid grid = GameObject.Find("Map").GetComponent<Grid>();
            sliding = false;
            stoppedPos = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }*/
    }

    private void OnMouseDown() {
        lastMousePosition = Input.mousePosition;
        dragging = true;
    }

    private void OnMouseUp() {
        if(Mathf.Abs(Input.mousePosition.x - lastMousePosition.x) >= Mathf.Abs(Input.mousePosition.y - lastMousePosition.y)) {
            if (Input.mousePosition.x > lastMousePosition.x) {
                dragging = false;
                sliding = true;
                print("right");
                Move(0);
            }
            else if (Input.mousePosition.x < lastMousePosition.x) {
                dragging = false;
                sliding = true;
                print("left");
                Move(1);
            }
        }
        else {
            if (Input.mousePosition.y > lastMousePosition.y) {
                dragging = false;
                sliding = true;
                print("up");
                Move(2);
            }
            else if (Input.mousePosition.y < lastMousePosition.y) {
                dragging = false;
                sliding = true;
                print("down");
                Move(3);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        print("collided");
        Grid grid = GameObject.Find("Map").GetComponent<Grid>();
        sliding = false;
        stoppedPos = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
        transform.position = stoppedPos;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        GameObject.Find("Manager").GetComponent<CustomGrid>().CreateGrid();
        GameObject.Find("Manager").GetComponent<Pathfinding>().recalculatePaths = true;
    }

    private void Move(int dir) {
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        Vector3 tVelocity;
        switch (dir) {
            case 0:
                /*tVelocity = new Vector2(10f * Time.fixedDeltaTime * moveRate, r.velocity.y);
                r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);*/
                r.AddForce(new Vector2(10f * Time.fixedDeltaTime * moveRate, r.velocity.y));
                break;
            case 1:
                /*tVelocity = new Vector2(-1 * 10f * Time.fixedDeltaTime * moveRate, r.velocity.y);
                r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);*/
                r.AddForce(new Vector2(-1 * 10f * Time.fixedDeltaTime * moveRate, r.velocity.y));
                break;
            case 2:
                /*tVelocity = new Vector2(r.velocity.x, 10f * Time.fixedDeltaTime * moveRate);
                r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);*/
                r.AddForce(new Vector2(r.velocity.x, 10f * Time.fixedDeltaTime * moveRate));
                break;
            case 3:
                /*tVelocity = new Vector2(r.velocity.x, -1 * 10f * Time.fixedDeltaTime * moveRate);
                r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);*/
                r.AddForce(new Vector2(r.velocity.x, -1 * 10f * Time.fixedDeltaTime * moveRate));
                break;
        }
    }
}
