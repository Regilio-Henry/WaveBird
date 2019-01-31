using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player_controler : MonoBehaviour
{

    public float speed;
    public float moveH;
    float moveV;
    public float jumpheight;
    Rigidbody2D rb;
    bool grounded = false;
    bool ceilinged = false;
    public LayerMask Ground;
    public float rad = 2.0f;
    public float ceilrad = 2.0f;
    public float area = 0.5f;
    private float varspeed = 10;
    public Transform OnGround;
    public Transform OnCeil;
    public Vector3 force = Vector3.zero;
    public float groundedHeight = 0.5f;
    public float heightOffset = 0.25f;
    public float forceAmount = 20;
    bool dash = false;
    public GameObject deathBounds;
    public CircleCollider2D collider;
    public int sparkCount;
    public RectTransform rectangle;
    public float energyAmount = 100;
    float fullscale;
    public int score = 0;
    public Text[] scoreText = new Text[2];
    public Text[] highScoreText = new Text[2];
    public Text[] actionText = new Text[2];
    bool pastMedian = false;
    public GameObject medianHeight;
    public string action = "";
    int combo;
    Color[] colours = new Color[2];
    public float fadeOutTime;
    bool recovering = false;
    public GameObject powerIcon;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        grounded = true;
        varspeed = 10;
        fullscale = rectangle.localScale.x;
        for(int i = 0; i < 2; i++)
        {
            colours[i] = actionText[i].color;
            highScoreText[i].text = "HIGHSCORE: " + PlayerPrefs.GetInt("highScore").ToString();
        }

        
    }


    void Update()
    {
        energyAmount -= .1f;
        rectangle.localScale = new Vector3(fullscale / 100 * energyAmount, rectangle.localScale.y);

        if(sparkCount <= 0)
        {
            powerIcon.SetActive(false);
        }
        else
        {
            powerIcon.SetActive(true);
        }
        for (int i = 0; i < 2; i++)
        {
            scoreText[i].text = "SCORE: " + score;
            actionText[i].text = action;
        }

        if (energyAmount >= 100)
        {
            energyAmount = 100;
        }

        if (energyAmount <= 0)
        {
            energyAmount = 0;
            endScene();
        }

        scoring();
    }

    void actionFade()
    {
        actionText[0].color = colours[0];
        actionText[1].color = colours[1];

        for (int i = 0; i < 2; i++)
        {
            //FadeOut(actionText[i]);
        }
    }

    void scoring()
    {

        float time = 0;
        int counter = 0;

        if (transform.position.y < -3)
        {
            time += Time.deltaTime;
            time = 0;
            counter++;
            recovering = true;

        }
        else if(recovering)
        {
            score += 30;
            recovering = false;
            action = "RECOVERY!";
        }

        float currentHigh = 0;

        if (medianHeight.transform.position.y < transform.position.y)
        {
            currentHigh = transform.position.y;
            if (currentHigh < transform.position.y)
            {
                currentHigh = transform.position.y;
            }
            pastMedian = true;
            action = "BIG AIR!";

        }

        if (pastMedian && medianHeight.transform.position.y > transform.position.y)
        {
            score += (int)((medianHeight.transform.position.y - currentHigh) * 2);
            pastMedian = false;
            
        }

        actionFade();
    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        speed = varspeed;

        //Movement variables
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveH * speed, rb.velocity.y);

        if (Input.GetKeyDown("space") && grounded)
        {
            dash = true;
            force = Vector3.up * forceAmount;
            print(force);

            //PlayerAudio.PlaySound("bounce");
            GetComponent<AudioSource>().Play();
        }
        if (Physics2D.OverlapCircle(OnGround.position, rad, Ground) && transform.position.y >= 0)
        {
            grounded = true;
            score += combo * 10;
            combo = 0;
        }
        else
        {
            grounded = false;
        }

        if (Physics2D.OverlapCircle(OnCeil.position, ceilrad, Ground) && transform.position.y >= 0)
        {
            ceilinged = true;
        }
        else
        {
            ceilinged = false;
        }

        if (deathBounds.transform.position.y > transform.position.y)
        {
            setHighScore();
            endScene();
        }


        if (dash && Input.GetKeyDown("z"))
        {
            rb.AddForce(new Vector3(moveH * forceAmount * 10, transform.position.y, transform.position.z));
            //transform.Translate(moveH * 20,0);
            transform.position = Vector3.MoveTowards(transform.position, Vector3.one * 10 * moveH, .5f);

        }

        if (!grounded)
        {
            if (Input.GetKeyDown("space") && transform.position.y >= 0)
            {
                rb.AddForce(Vector3.down * forceAmount * 10);
            }

            if (transform.position.y >= 0)
            {
                force = Vector3.zero;
            }
        }



        if (transform.position.y <= 0)
        {
            if (Input.GetKeyDown("space") && grounded)
            {
                rb.AddForce(Vector3.up * forceAmount);
                collider.enabled = false;
            }
            collider.enabled = true;
            grounded = true;
        }

        rb.AddForce(force);
    }

    void endScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void setHighScore()
    {
        if (PlayerPrefs.GetInt("highScore") < score)
            PlayerPrefs.SetInt("highScore", score);
        PlayerPrefs.Save();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(OnGround.position, rad);
    }

    public void FadeOut(Text text)
    {
        StartCoroutine(FadeOutRoutine(text));
    }

    private IEnumerator FadeOutRoutine(Text text)
    {
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "pickup")
        {
            Destroy(collision.gameObject);
            sparkCount++;
            score += 10;
            combo += 1;
            if (combo >= 1)
            {
                action = "BIRD PICKUP!";
            }
            else
            {
                action = "BIRD PICKUP x" + combo;
            }
        }
      

        if (collision.gameObject.tag == "bat")
        {
            score += 50;
            action = "WHO TOUCHA MY BAT!?";
        }


        if (collision.gameObject.tag == "pickup")
        {
            Destroy(collision.gameObject);
            sparkCount++;
            score += 10;
        }
    }

}
