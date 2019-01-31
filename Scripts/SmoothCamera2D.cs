using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    bool shaking = true;
    float time;
    int counter;
    public int timeLenght = 0;

    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        print(counter);
    }

    void screenShake()
    {
        time += Time.deltaTime;
        if (time >= timeLenght)
        {
            time = 0;
            counter++;

            if (shaking)
            {
                transform.position = new Vector3(Mathf.PerlinNoise(transform.position.x, transform.position.y), Mathf.PerlinNoise(transform.position.x, transform.position.y), transform.position.z);
                shaking = false;
            }
        }
    }

    public void shake()
    {
        //shaking = true;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }
}