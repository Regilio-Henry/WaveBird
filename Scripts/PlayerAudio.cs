using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static AudioClip bounce, groundPound;
    static AudioSource audioPlace;

	void Start ()
    {
        bounce = Resources.Load<AudioClip>("Bounce");
        groundPound = Resources.Load<AudioClip>("GroundPound");

        audioPlace = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
		
	}

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Bounce":
                audioPlace.PlayOneShot(bounce);
                break;
            case "GroundPound":
                audioPlace.PlayOneShot(groundPound);
                break;
        }

    }
}
