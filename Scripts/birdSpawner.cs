using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdSpawner : MonoBehaviour {
    float time;
    int counter;
    float timeLenght = 3;
    public GameObject entity;
    public int min;
    public int max;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= timeLenght)
        {
            time = 0;
            counter++;
            Instantiate(entity,transform);
            timeLenght = Random.Range(min, max);

            GetComponent<AudioSource>().Play();
        }
    }
}
