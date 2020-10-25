using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //Basic variables
    private Rigidbody2D rd2d;

    public float speed;

    private int scoreValue = 0;
    
    private int lives = 3;

    //Text Variables
    public Text score;

    public Text livesText;

    public Text winText;

    //Audio Variables
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    //Animator Variables
    Animator anim;

    private bool facingRight = true;

    //Ground Variables
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Set texts
        SetCountText();
        SetLivesText();
        winText.text = "";
        score.text = scoreValue.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        //Key animanions
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

         if (Input.GetKeyDown(KeyCode.W))

        {
          anim.SetInteger("State", 2);
         }

        if (Input.GetKeyUp(KeyCode.W))

        {
          anim.SetInteger("State", 0);
         }

        if (Input.GetKeyDown(KeyCode.A))

        {
          anim.SetInteger("State", 1);
         }

        if (Input.GetKeyUp(KeyCode.A))

        {
          anim.SetInteger("State", 0);

         }

        if (Input.GetKeyDown(KeyCode.D))

        {

          anim.SetInteger("State", 1);
         }

        if (Input.GetKeyUp(KeyCode.D))

        {

          anim.SetInteger("State", 0);

        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    //Coin collision(enter)
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            lives = lives - 1;  
            SetLivesText();
        }
    }

    //Ground collision(stay)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    //Set score text
    void SetCountText()
    {
         score.text = "Score: " + scoreValue.ToString ();
        
        if (scoreValue == 4)
        {
            transform.position = new Vector3(90.0f, 3.0f, 0.0f);
            lives = 3;
        }
        
        else if(scoreValue >= 8)
        {
            winText.text = "You Win! But only because Nathan said so.";
        }
    }

    //Se lives text
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives == 0)
        {
            winText.text = "You Lose! Because Nathan said so.";
        }
    }

    //Flip function for anim
    void Flip()
    {    
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }
}
