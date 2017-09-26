using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueScript : MonoBehaviour {

    public int hitsToKill;
    public int points;
    private int numberOfHits;

    // Use this for initialization
    void Start () {
        numberOfHits = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pelota")
        {
            numberOfHits++;

            if (numberOfHits == hitsToKill)
            {
                // get reference of player object
                GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

                // send message
                player.SendMessage("addPoints", points);

                // destruye el objeto
                Destroy(this.gameObject);
            }
        }
    }
}
