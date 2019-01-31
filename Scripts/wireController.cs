using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireController : MonoBehaviour {
    public int length = 3;
    float xOffset = 1;
    public int yOffset;
    public float lineWidth = 1;
    public GameObject connectionPoint;
    GameObject newConnectionPoint;
    public Color lineColor = Color.white;
    GameObject[] wireCollection;
    public GameObject start;
    public GameObject end;
    public int pointComplexity;
    int pointAmount;
    int distance;
    float intercept;

    static int size = 5; // Number of vertices
    static float velocityDamping = 0.999999f; // Proprotional velocity damping, must be less than or equal to 1.
    static float timeScale = 50f;

    float[] newHeight = new float[size];
    float[] velocity = new float[size];

    GameObject[] vertex = new GameObject[size];

    // Use this for initialization
    void Start()
    {
        //length = (int)Vector2.Distance(start.transform.position, end.transform.position) / pointComplexity;
        pointAmount = (int)Vector2.Distance(start.transform.position, end.transform.position) * pointComplexity;
        distance = (int)Vector2.Distance(start.transform.position, end.transform.position);
        print("points on line: " + pointAmount);
        print("distance: " + distance);
        xOffset = 1f / pointComplexity;
        print("offset: " + xOffset);
        //Create wire collection
        newHeight = new float[pointAmount + 1];
        velocity = new float[pointAmount + 1];
        wireCollection = new GameObject[pointAmount + 1];
        wireCollection[0] = start;
        newConnectionPoint = Instantiate(connectionPoint);
        
        //Add first connection point and set position
        wireCollection[1] = newConnectionPoint;
        newConnectionPoint.transform.position = wireCollection[0].transform.position - new Vector3(-xOffset, 0);

        //Add line renders
        wireCollection[0].AddComponent<LineRenderer>();
        wireCollection[1].AddComponent<LineRenderer>();

        AddPoints(2, wireCollection.Length);
        wireCollection[wireCollection.Length - 1] = end;
        
    }

    void pointFunction()
    {
        float startRange = .1f;
        float endRange = .2f;
        float range = (endRange - startRange)/ 2;

        for (int i = 1; i < wireCollection.Length; i++)
        {
            if (i != wireCollection.Length - 1)
            {
                wireCollection[i].transform.Translate(0, Mathf.Sin(Time.time * i) * (range), 0);
                //wireCollection[i].transform.position += transform.up * Mathf.Sin(Time.time * 100f + i) * 5f;
            }
        }

    }

    void spring()
    {

    }

    void pointDisapate()
    {
        for (int i = 1; i < wireCollection.Length; i++)
        {
            if (i != wireCollection.Length - 1)
            {
                //wireCollection[i].transform.Translate(wireCollection[i].transform.position.x, Mathf.Lerp(wireCollection[i].transform.position.y, start.transform.position.y, .5f), wireCollection[i].transform.position.z);
            }
        }

    }




    void waves(int size)
    {

        // Water tension is simulated by a simple linear convolution over the height field.
        for (int i = 1; i < size - 1; i++)
        {
            int j = i - 1;
            int k = i + 1;
            newHeight[i] = (wireCollection[i].transform.position.y + wireCollection[j].transform.position.y + wireCollection[k].transform.position.y) / 3.0f;
        }

        // Velocity and height are updated...
        for (int i = 0; i < size; i++)
        {
            // update velocity and height
            velocity[i] = (velocity[i] + (newHeight[i] - wireCollection[i].transform.position.y)) * velocityDamping;

            float timeFactor = Time.deltaTime * timeScale;
            if (timeFactor > 1f) timeFactor = 1f;

            newHeight[i] += velocity[i] * timeFactor;

            // update the vertex position
            Vector3 newPosition = new Vector3(
                wireCollection[i].transform.position.x,
                newHeight[i],
                wireCollection[i].transform.position.z);
            vertex[i].transform.position = newPosition;
        }
    }

   


    void AddPoints(int startIndex, int indexLenght)
    {
        for (int i = 2; i < wireCollection.Length; i++)
        {
            //Instansiate ConectionPoint
            newConnectionPoint = Instantiate(connectionPoint);
            
            //Add point to collection
            wireCollection[i] = newConnectionPoint;

            //Set ConnectionPoint position
            if (i != wireCollection.Length - 1)
            {
                //intercept = end.transform.position.y - Mathf.Tan(Vector3.Angle(start.transform.position, end.transform.position)) * newConnectionPoint.transform.position.x;   
                //yOffset = (int)(intercept - Mathf.Tan(Vector3.Angle(start.transform.position, end.transform.position)) * newConnectionPoint.transform.position.x);
                newConnectionPoint.transform.position = wireCollection[i - 1].transform.position - new Vector3(-xOffset, 0);
                wireCollection[i].AddComponent<LineRenderer>();
            }
            else
            {
                newConnectionPoint.transform.position = wireCollection[i - 1].transform.position - new Vector3(-xOffset, 0);
            }
        }
    }

    void SetLineAndLenght(GameObject start, GameObject end)
    {
        start.GetComponent<LineRenderer>().SetPosition(0, start.transform.position);
        start.GetComponent<LineRenderer>().SetPosition(1, end.transform.position);
        addPhysLine(start.transform.position, end.transform.position);
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
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

    // Update is called once per frame
    void Update()
    {
      
        SetLineAndLenght(wireCollection[0], wireCollection[1]);
        SetLineProperties(wireCollection[0]);
        SetLineAndLenght(wireCollection[1], wireCollection[2]);
        SetLineProperties(wireCollection[1]);
        for (int i = 1; i < wireCollection.Length; i++)
        {
            if (i != wireCollection.Length - 1)
            {
                SetLineAndLenght(wireCollection[i], wireCollection[i + 1]);
                SetLineProperties(wireCollection[i]);
                
            }
        }

        if (Input.anyKey)
        {
            //pointFunction();
        }
        //pointDisapate();
        waves(wireCollection.Length);

    }
}
