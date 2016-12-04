using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    //we fill this up with sprites in Unity engine, usefull for dynamically changing health/things
    public Sprite[] HeartSprites;
    public Image HeartUI;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        //what i've done here is make the animation happen when divided by 20 does teh same thing
        HeartUI.sprite = HeartSprites[player.currHealth/20];
    }
}
