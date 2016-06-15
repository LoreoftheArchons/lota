using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    private Player player;

    //when game starts
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.Damage(20);

            //only way you can start an IEnumerator is with StartCoroutine, can be used for a lot of things, turrets, enemies, etc.
            StartCoroutine(player.Knockback(0.02f, 150,player.transform.position));
        }
    }

}
