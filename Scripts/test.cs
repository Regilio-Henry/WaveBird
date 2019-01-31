using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    static int size = 20; // Number of vertices
    static float velocityDamping = 0.999999f; // Proprotional velocity damping, must be less than or equal to 1.
    static float timeScale = 50f;
    public Color lineColor = Color.white;
    float[] newHeight;
    float[] velocity;
    public float lineWidth = 1;
    public GameObject[] vertex;
    public GameObject vertexObject;
    public int pointComplexity;
    public GameObject start;
    public GameObject end;
    int pointAmount;
    int distance;
    float xOffset = 1;
    public float startY;

    void Start()
    {
        size = 39;
        newHeight = new float[size + 1];
        velocity = new float[size + 1];
        vertex = new GameObject[size];
        //pointAmount = (int)Vector2.Distance(start.transform.position, end.transform.position) * pointComplexity;
        //distance = (int)Vector2.Distance(start.transform.position, end.transform.position);
        //size = pointAmount;
        //xOffset = 1f / pointComplexity;

        // we'll use spheres to represent each vertex for demonstration purposes
        for (int i = 0; i < size; i++)
        {
            GameObject Go = Instantiate(vertexObject);
            Go.AddComponent<LineRenderer>();
            vertex[i] = Go;
            vertex[i].transform.position = new Vector3(6 + (i - size / 2), 0, 0);
        }
        //vertex[vertex.Length - 1] = end;
        //vertex[0] = start;
    }

    void SetLineAndLenght(GameObject start, GameObject end)
    {
        start.GetComponent<LineRenderer>().SetPosition(0, start.transform.position);
        start.GetComponent<LineRenderer>().SetPosition(1, end.transform.position);
        for (int i = 0; i < 1; i++)
        {
            addPhysLine(start.transform.position, end.transform.position);
        }
    }

    void SetLineProperties(GameObject Line)
    {
        Line.GetComponent<LineRenderer>().startWidth = lineWidth;
        Line.GetComponent<LineRenderer>().endWidth = lineWidth;
        Line.GetComponent<LineRenderer>().material.color = lineColor;
    }

    void addPhysLine(Vector3 startPos, Vector3 endPos)
    {
        BoxCollider2D col = new GameObject("Collider").AddComponent<BoxCollider2D>();
        col.gameObject.layer = 8;
        col.transform.parent = transform; // Collider is added as child object of line
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);

    }



    void Update()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        SetLineAndLenght(vertex[0], vertex[1]);
        SetLineProperties(vertex[0]);
        SetLineAndLenght(vertex[1], vertex[2]);
        SetLineProperties(vertex[1]);

        // Water tension is simulated by a simple linear convolution over the height field.
        for (int i = 1; i < size - 1; i++)
        {
            int j = i - 1;
            int k = i + 1;
            newHeight[i] = (vertex[i].transform.position.y + vertex[j].transform.position.y + vertex[k].transform.position.y) / 3.0f;
        }

       
        //Velocity and height are updated...
        for (int i = 0; i < size; i++)
        {
            // update velocity and height
            velocity[i] = (velocity[i] + (newHeight[i] - vertex[i].transform.position.y)) * velocityDamping;
            float timeFactor = Time.deltaTime * timeScale;
            if (timeFactor > 1f) timeFactor = 1f;

            newHeight[i] += velocity[i] * timeFactor;

            if (i != size -1)
            {
                SetLineAndLenght(vertex[i], vertex[i+1]);
                SetLineProperties(vertex[i]);

            }

            // update the vertex position
            Vector3 newPosition = new Vector3(
                vertex[i].transform.position.x,
                newHeight[i],
                vertex[i].transform.position.z);
            vertex[i].transform.position = newPosition;
        }
    }
}
