using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class arrow_movement : MonoBehaviour
{
    Rigidbody2D Rb;
    
    public float neutralSpeed;
    public float fastSpeed;
    public float slowSpeed;
    public float statusTimeInSeconds;
    float currentSpeed;
    float MovementX;
    float MovementY;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
        currentSpeed = neutralSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        StopAllCoroutines();
        if (collision.gameObject.tag == "SpeedSquare") {
            StartCoroutine(TempSpeedBuff(statusTimeInSeconds));
        } else if (collision.gameObject.tag == "SlowSquare") {
            StartCoroutine(TempSlowDebuff(statusTimeInSeconds));
        } else if (collision.gameObject.tag == "Finish") {
            Debug.Log("Player 1 Wins!");
            //TODO: Delete this and instead go back to the board
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator TempSpeedBuff(float waitTime) {
        currentSpeed = fastSpeed;
        yield return new WaitForSeconds(waitTime);
        currentSpeed = neutralSpeed;
        Debug.Log("currentSpeed = " + currentSpeed);
    }

    IEnumerator TempSlowDebuff(float waitTime) {
        currentSpeed = slowSpeed;
        yield return new WaitForSeconds(waitTime);
        currentSpeed = neutralSpeed;
        Debug.Log("currentSpeed = " + currentSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Rb.velocity = new Vector2(MovementX * currentSpeed * Time.deltaTime, MovementY * currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovementY = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovementY = -1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovementX = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovementX = -1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) 
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            MovementX = 0;
        }
    }
}
