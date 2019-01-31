using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverClass : MonoBehaviour
{
    float time;
    bool pleaseDaddy;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();
            pleaseDaddy = true;
        }

        if (pleaseDaddy == true)
        {
            time += Time.deltaTime;
        }

        if (time > 1)
        {
            SceneManager.LoadScene(1);
        }
	}
}
