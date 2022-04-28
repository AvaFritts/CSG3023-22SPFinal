/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Ava Fritts
 * Last Edited: April 28 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public float score;

    [Tooltip("The HUD element text boxes")]
    public Text ballTxt;
    public Text scoreTxt;

    [Space(10)]
    public GameObject paddle;
    bool isInPlay; //are we playing?

    Rigidbody rb;

    [Header("Ball Settings")]

    [Tooltip("How many lives?")]
    public int numberOfBalls; 
    public float speed; //how fast the ball moves
    public Vector3 initialForce; //idea: make a new variable to get the Y position
    private AudioSource audioSource;


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>(); //no idea if this will work.
        audioSource = this.GetComponent<AudioSource>(); //find the audio source
    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position
 

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        //update the score and lives
        scoreTxt.text = "Score: " + score;
        ballTxt.text = "Balls: " + numberOfBalls;
        
        if (!isInPlay)
        {
            //move with the 
            Vector3 pos = new Vector3();
            pos.x = paddle.transform.position.x;
            pos.y = transform.position.y; //There has to be a better way to do this: I don't know how
            transform.position = pos; //put the new transformation on the ball.

            if (Input.GetKeyDown("space"))
            {
                isInPlay = true;
                Move();
            }
        }
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            rb.velocity = (speed * rb.velocity.normalized);
        }

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddle
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus its height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()


    void Move()
    {
        rb.AddForce(initialForce); //send the ball upwards
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play(); //play the sound!
        GameObject coGO = collision.gameObject; //get the game object
        if(coGO.tag.Equals("Brick")) //if it is a brick
        {
            score += 100;
            Destroy(coGO); //destroy the brick!
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutBounds")) //if it is the Out of Bounds object
        {
            numberOfBalls--;    
            if(numberOfBalls >= 0) //if there are still lives (I go on a "0 lives means last chance" basis)
            {
                Invoke("SetStartingPos", 2f); //restarts the ball's position after 2 seconds
            }
        }
    }

}
