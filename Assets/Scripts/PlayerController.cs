using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float moveRate = 40.0f;
    private Vector3 mVelocity = Vector3.zero;
    private float mMovementSmoothing = .05f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveRate;
        verticalMove = Input.GetAxisRaw("Vertical") * moveRate;

        //Trava de Rotação
        transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate() {
        HMove(horizontalMove);
        VMove(verticalMove);
    }

    private void HMove(float dir) {
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        Vector3 tVelocity = new Vector2(dir * 10f * Time.fixedDeltaTime, r.velocity.y);
        r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);
    }

    private void VMove(float dir) {
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        Vector3 tVelocity = new Vector2(r.velocity.x, dir * 10f * Time.fixedDeltaTime);
        r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);
    }
}
