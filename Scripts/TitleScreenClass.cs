using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenClass : MonoBehaviour
{
    float time;
    bool ugh;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Debug.Log(time);

		if (Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();
            ugh = true;
        }

        if (ugh == true)
        {
            time += Time.deltaTime;
        }

        if (time > 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
