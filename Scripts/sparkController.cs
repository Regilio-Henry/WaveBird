using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkController : MonoBehaviour {

    public GameObject wire;
    GameObject[] pointCollection;
    //public GameObject player;
    int i = 0;
    float direction;
	// Use this for initialization
	void Start ()
    {
        pointCollection = GameObject.Find("Test").GetComponent<test>().vertex;//wire.GetComponent<test>().vertex;
        //transform.position = pointCollection[0].transform.position;
    }
	
    public void Init(int _i, float dir)
    {
        int i = _i;
        direction = dir;
    }
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x == pointCollection[i].transform.position.x)
        {
            i++;
        }
        transform.position = new Vector3(Vector3.MoveTowards(transform.position, pointCollection[i].transform.position, .1f).x, pointCollection[i].transform.position.y);
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = pointCollection[i].transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);


    }
}
