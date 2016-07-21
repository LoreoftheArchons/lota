using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public int points;
    public Text pointsText;
    public Text InputText;

    void Update()
    {
        pointsText.text = ("Points: " + points);
    }
}
