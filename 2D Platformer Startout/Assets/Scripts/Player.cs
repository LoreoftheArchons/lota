using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    //floats
    public float maxspeed = 3;
    public float speed = 50;
    public float jumpPower = 50;

    //bools
    public bool grounded;
    public bool canDoubleJump;

    //stats
    public int currHealth;
    public int maxHealth=100;

    //references
    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        //just starting the game, have health
        currHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {

        //sets theparameters in the animator
        anim.SetBool("Grounded",grounded);
        //if output is negative, will be positive, be easier to check if moving (rb2d.velocity.x will take in the players speed, rather then just the the A/D or left/right key
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        //flips player
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //triggers the SPACE key to jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                //triggers double jumping to be true
                canDoubleJump = true;
            }
            else
            {
                //if already double jumping, wanna make sure that the person cant infinetely jump
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    //dont affect anything, and then we can jump upwards again, kind of reseting
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    //the 1.25 reduces the second jump power, not entirely needed
                    rb2d.AddForce(Vector2.up * jumpPower / 1.25f);
                }
            }
           
        }

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        if (currHealth <= 0)
        {
            Die();
        }

    }

    //deals with physics
    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        //doesnt affect y
        easeVelocity.y = rb2d.velocity.y;
        //z axis not used in 2d
        easeVelocity.z = 0.0f;
        //multiplies the easevelocity.x by 0.75 which will reduce the exit velocity, reducing speed
        easeVelocity.x *= 0.75f;


        //takes left and right arrows or a and d as input 1 for right and -1 for left
        float h = Input.GetAxis("Horizontal");

        //fake friction/easing x speed of player
        
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        //this should move the player based on what we input (vector2 is x axis)
        rb2d.AddForce((Vector2.right * speed) * h);

        //limits speed of player
        if (rb2d.velocity.x > maxspeed)
        {
            rb2d.velocity = new Vector2(maxspeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxspeed)
        {
            rb2d.velocity = new Vector2(-maxspeed, rb2d.velocity.y);
        }
    }

    void Die()
    {

        Application.LoadLevel(Application.loadedLevel);
    }
}
