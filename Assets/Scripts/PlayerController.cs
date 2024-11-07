using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{
    public Text healthText;
    public Text scoreText;
    public Text instructions;
    public Text winLoseText;
    public FixedJoystick joystick;
    
    public float moveSpeed = 2f;

    private float xInput, yInput;
    private int score = 0;
    private int health = 3;
    private bool isGameActive = false;

    

    // Start is called before the first frame update
    void Start()
    {
        winLoseText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            instructions.enabled = false; 
        }
        if (health >= 0)
        {
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
        }
        if(health == 0)
        {
            EndGame("Game Over");

        }
        if(score == 5)
        {
            EndGame("You Win");
        }
        
    }

    private void FixedUpdate()
    {
        if(isGameActive) { return; }
        xInput = joystick.Horizontal * moveSpeed * Time.deltaTime;
        yInput = joystick.Vertical * moveSpeed * Time.deltaTime;


        transform.Translate(xInput, yInput, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Carrot")
        {
            score++;
            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "Bomb")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }

    private void EndGame(string message)
    {
        isGameActive = false;
        winLoseText.text = message;
        winLoseText.enabled = true;

        Invoke("ReloadScene", 2f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
