using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // movement variables
    public float maxSpeed;

    // jumping variables
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;
    
    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;

    // for shooting
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f;
    float nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        facingRight = true;
    }

    void Update(){
        if (grounded && Input.GetAxis("Jump") > 0) {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
        }

        // player shooting
        // if the palyer presses the fire button
        if (Input.GetAxisRaw("Fire1") > 0 ){
            fireRocket();
        }
    }

    // called after a period of time (not for every frame like the method update())
    void FixedUpdate() {

        // check if we are grounded. if no, then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);

        // this changes the blend tree
        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move)); 

        myRB.velocity = new Vector2(move*maxSpeed, myRB.velocity.y); 

        if (move > 0 && !facingRight){ // pressing D button
            flip();
        }
        else if (move < 0 && facingRight) {// pressing A key
            flip();
        }
    }

    void flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //Debug.Log(transform.localScale.y.ToString());
    }

    void fireRocket(){
        if (Time.time > nextFire){
            nextFire = Time.time + fireRate;
            if (facingRight){
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0,0,0)));
            } else {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0,0,180f)));
            }
            //Debug.Log(gunTip.position.y.ToString());
        }
    }
}

