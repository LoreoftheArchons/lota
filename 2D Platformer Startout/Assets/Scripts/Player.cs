using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {
    //floats
    public float maxspeed = 3;
    public float speed = 50;
    public float jumpPower = 50;

    //bools
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    //stats
    public int currHealth;
    public int maxHealth=100;

    //references
    private Rigidbody2D rb2d;
    private Animator anim;
    private GameMaster gm;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;


	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        //just starting the game, have health
        currHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
	}

    // Update is called once per frame
    void Update() {

        //sets theparameters in the animator
        anim.SetBool("Grounded", grounded);
        //if output is negative, will be positive, be easier to check if moving (rb2d.velocity.x will take in the players speed, rather then just the the A/D or left/right key
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        //flips player
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
            facingRight = false;
        }
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            facingRight = true;
        }

        //triggers the SPACE key to jump
        if (Input.GetButtonDown("Jump") && !wallSliding)
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

        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }
        }

        if(wallCheck == false || grounded)
        {
            wallSliding = false;
        }
    }

    void HandleWallSliding()
    {

        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);

        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-1.5f, 2) * jumpPower);

            }
            else
            {
                rb2d.AddForce(new Vector2(1.5f, 2) * jumpPower);
            }
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
        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed/2) * h);
        }
        

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
        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (PlayerPrefs.GetInt("Highscore") < gm.points)
            {
                PlayerPrefs.SetInt("Highscore", gm.points);
            }
            
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", gm.points);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Application.LoadLevel(Application.loadedLevel);
    }


    //allows player to take damage
    public void Damage(int dmg)
    {
        currHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("red_flash_player");
    }

    //knockDur, how long we are going to add force for, knockbackPwr, the power of the knockback, knockbackDir, the direction the kock hits you too
    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }
        // in ienumerators, we need to return something, so this will finish this loop and function
        yield return 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            gm.points += 1;
        }
    }
}
