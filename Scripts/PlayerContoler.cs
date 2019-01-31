using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoler : MonoBehaviour {

    public float hSpeed = 5;
    public float vSpeed = 5;
    public float speed = 10;
    public float gravity = 14.0f;
    float jumpForce = 10.0f;
    float verticalVelocity;
    bool onGround = false;
    [SerializeField]
    LayerMask ground;

    // Use this for initialization
    void Start ()
    {
        
	}

    // Update is called once per frame
    void Update()
    {
        hSpeed = Input.GetAxis("Horizontal");
        

        if (onGround)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }

        transform.position = new Vector2(transform.position.x + hSpeed * speed, verticalVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == ground.value)
        {
            onGround = true;
        }
    }
}