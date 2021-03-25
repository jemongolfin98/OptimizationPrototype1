using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb2d;
    public Text timeText;
    public Text currencyText;
    public Text finalcurrencyText;
    public Text finalTimeText;
    public Text finalScoreText;
    public GameObject gameOverScreen;
    public GameObject gameActiveScreen;

    private int currency;
    private int seconds;
    public int time;
    private float timer = 0.0f;
    private bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (gameOver == true)
        {
            GameOver();
        }

        timer += Time.deltaTime;
        seconds = (int)(timer);
        time = seconds;
        timeText.text = "Time: " + time + " Seconds";

        if (time >= 300)
        {
            GameOver();
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            rb2d.velocity = new Vector2(speed * -1, rb2d.velocity.y);
        if (Input.GetKey(KeyCode.RightArrow))
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Allows Player to Jump if on the Ground
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        //Allows Player to Jump if on a Platform
        if (collision.collider.tag == "Platform")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    //Adjusts Currency Counter with each Pickup Encountered
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adds 25 Currency per Pickup Acquired
        if (other.gameObject.CompareTag("Coin25"))
        {
            other.gameObject.SetActive(false);
            currency = currency + 25;
            SetCurrencyText();

        }

        //Adds 10 Currency per Pickup Acquired
        if (other.gameObject.CompareTag("Coin10"))
        {
            other.gameObject.SetActive(false);
            currency = currency + 10;
            SetCurrencyText();

        }

        //Adds 5 Currency per Pickup Acquired
        if (other.gameObject.CompareTag("Coin05"))
        {
            other.gameObject.SetActive(false);
            currency = currency + 5;
            SetCurrencyText();

        }

        //Adds 1 Currency per Pickup Acquired
        if (other.gameObject.CompareTag("Coin01"))
        {
            other.gameObject.SetActive(false);
            currency = currency + 1;
            SetCurrencyText();

        }

        //Subtracts 10 Currency per Pickup Acquired
        if (other.gameObject.CompareTag("Coin0"))
        {
            other.gameObject.SetActive(false);
            currency = currency - 10;
            SetCurrencyText();

        }

        //Subtracts 25 Currency per Fall
        if (other.gameObject.CompareTag("Wall1"))
        {
            currency = currency - 25;
            SetCurrencyText();

        }
    }

    void SetCurrencyText()
    {
        currencyText.text = "Currency: " + currency.ToString();

        if (currency >= 100)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        speed = 0;
        jumpForce = 0;
        Time.timeScale = 0f;
        gameActiveScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        finalcurrencyText.text = "Final Currency: " + currency;
        finalTimeText.text = "Final Time: " + time + " Seconds";

        if (time <= 30)
        {
            finalScoreText.text = "4 Starz";
        }
        else if (time > 30 && time <= 60)
        {
            finalScoreText.text = "3 Starz";
        }
        else if (time > 60 && time <= 120)
        {
            finalScoreText.text = "2 Starz";
        }
        else if (time > 120 && time <= 300)
        {
            finalScoreText.text = "1 Starz";
        }
        else
        {
            finalScoreText.text = "0 Starz";
        }

    }

    public void GameActive()
    {
        gameOver = false;
    }
}
