using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {

    //ints
    public int curHealth;
    public int maxHealth;

    //float
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 100;
    //cooldown for bullets
    public float bulletTimer;

    //booleans
    public bool awake = false;
    public bool lookingRight = true;

    //refs
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft, shootPointRight;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

        RangeCheck();

        //checks what side player is on
        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }
        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    //activates based off of a range
    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance < wakeRange)
        {
            awake = true;
        }
        
        if (distance > wakeRange)
        {
            awake = false;
        }

    }

    public void Attack(bool attackingRight)
    {
        //puts it up by one second each update
        bulletTimer += Time.deltaTime;

        if (bulletTimer>= shootInterval)
        {
            //direction turret will shoot. This will return a direction to shoot
            Vector2 direction = target.transform.position - transform.position;
            //needed to work
            direction.Normalize();

            //this is how it shoots creates an instand and shoots based off of velocity
            if (!attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
            if (attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("red_flash_player");
    }

}

