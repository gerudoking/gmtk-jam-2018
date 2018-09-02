using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float moveRate = 40.0f;
    private Vector3 mVelocity = Vector3.zero;
    private float mMovementSmoothing = .05f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool facingRight = true;
    public float hp = 5.0f;
    private Timer imunityClock;
    private bool immune = false;
    public bool armored = false;

    public float psyMove = 2000.0f;

    public Animator animator;

    // Use this for initialization
    void Start () {
        imunityClock = new Timer(Timer.TYPE.CRESCENTE, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //GameOver
        if(hp <= 0) {
            SceneManager.LoadScene(2);
        }

        float animSpeed = 0;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveRate;
        verticalMove = Input.GetAxisRaw("Vertical") * moveRate;

        imunityClock.Update();

        if (imunityClock.Finished()) {
            immune = false;
        }

        if (Mathf.Abs(horizontalMove) > 0 || Mathf.Abs(verticalMove) > 0)
            animSpeed = 1;

        animator.SetFloat("Speed", animSpeed);

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

        if((dir < 0 && facingRight) || (dir > 0 && !facingRight)) {
            Flip();
        }
    }

    private void VMove(float dir) {
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        Vector3 tVelocity = new Vector2(r.velocity.x, dir * 10f * Time.fixedDeltaTime);
        r.velocity = Vector3.SmoothDamp(r.velocity, tVelocity, ref mVelocity, mMovementSmoothing);
    }

    private void Flip() {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        print("collided");
        if (collision.gameObject.tag == "enemy" && !immune) {
            if (!armored)
                hp--;
            else
                hp -= 0.5f;
            immune = true;
            imunityClock.Reset();
        }
    }
}
