using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour{

    private Vector2 velocity;

    //floats
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    //if you wanna make boundaries
    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //does the work for making sure the camera follows the player

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x,ref velocity.x,smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y,ref velocity.y, smoothTimeY);

        //again z needs to be taken account of as it is 2D, just make it not do anything
        transform.position = new Vector3(posX, posY, transform.position.z);

        if(bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
    
}
