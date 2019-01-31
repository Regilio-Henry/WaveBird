using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkCollection : MonoBehaviour {
    public LayerMask layer;
    public GameObject player;
    public int points;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spark") 
        {
            Destroy(collision.gameObject);
            player.GetComponent<Player_controler>().energyAmount += points;
        }
    }
}
