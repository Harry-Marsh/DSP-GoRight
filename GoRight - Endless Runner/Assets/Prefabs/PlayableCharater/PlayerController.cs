using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //setting gravity variables
    private float gravity = -50f;
    private CharacterController Controller;
    private Animator animatorC;
    private Vector3 velocity;
    private float horizontalInput;
    private bool jumpPressed;
    private float jumpTimer;
    private float jumpGracePeriod = 0.2f;
    public GameObject ExitMenuUI;
    public GameObject Player;
    public GameObject Score;
    public GameObject HighestScore;
    public GameObject pauseButton;
    public float jumpedAmount = 0;
    public float theHighScore;
    public float savedHighScore;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool outOfBounds;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] LayerMask boundsLayers;
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private Transform[] wallChecks;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private float loadedTime;


    // Start is called before the first frame update
    void Start()
    {
        //geting the controller atributes from unity
        Controller = GetComponent<CharacterController>();
        animatorC = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   //setting up the game over screen.
        outOfBounds = false;
        //if collition with the out of zone barrier 
        if (Physics.CheckSphere(transform.position, 0.1f, boundsLayers, QueryTriggerInteraction.Ignore))
        {
            ExitMenuUI.SetActive(true); //enables the menu gui
            Player.SetActive(false); //hides player
            Score.SetActive(false); //hides ingame score
            HighestScore.SetActive(false); //hides ingame highscore
            pauseButton.SetActive(false); // hides the pause button
            Time.timeScale = 0f; //sets the time to 0 to pause the game
            outOfBounds = true; //sets variable out of bounds to true
            theHighScore = GameObject.Find("PlatformGenerator").GetComponent<PlatformGeneration>().blockScore; //creates highscore form how many terrain blocks the character passes
            savedHighScore = PlayerPrefs.GetFloat("HighScore", theHighScore);
            if (theHighScore > savedHighScore)
            {
                PlayerPrefs.SetFloat("HighScore", theHighScore); // saves highscore to local files 
            }
        }
        loadedTime = GameObject.Find("PlatformGenerator").GetComponent<PlatformGeneration>().blockScore; // gets score from block
        loadedTime = loadedTime /= 350; //sets the incrementation rate of the game speed

        if (loadedTime > 1 && loadedTime < 2.2) // sets boundary of speed increse.
        {
            Time.timeScale = loadedTime; // changes time scale.
        }




        //sets constant horrizontal speed
        horizontalInput = 1;
        //sets the facing of the charecter to be the directionm of movment.
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        //used to cheak if the player is touchiung any ground layers
        isGrounded = false;
        foreach (var groundCheck in groundChecks)
        {
            if (Physics.CheckSphere(groundCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore)) // if ground check object is toching terrain
            {
                isGrounded = true;
                break;
               
            }
        }
        //used to cheak if the player is touchiung any walls 
        var blocked = false;
        foreach (var wallCheck in wallChecks)
        {
            if (Physics.CheckSphere(wallCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore)) // if wall check object is toching terrain
            {
                isGrounded = true;
                break;
            }
        }

        if (!blocked)
        {
            Controller.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);//if the players blocked the move speed is set to 0
        }


        //if touching ground do not apply downwords velocity simulated gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;// sets verticle velocity to 0
        }
        else
        {
            //setting the gravity for the player
            velocity.y += gravity * Time.deltaTime;
        }

        //applises movment ot the controller
        Controller.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);

        // adding jumping to the game
        jumpPressed = Input.GetButtonDown("Jump");
        if (isGrounded == true)
        {
            jumpedAmount = 0; //resets the total jump amount
        }
        if (jumpPressed)
        {
            jumpTimer = Time.time;
        }

        if (isGrounded && (jumpPressed || (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod)))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity); //increases the verticle velocity for a calculate amount of time.
            if( jumpSoundEffect != null) // checks if a sound is already playing
            {
                AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position, 1f); // plays jump sound when input detected
            }
            jumpTimer = -1;
        }else if (jumpPressed || (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod))
        {
            if (jumpedAmount < 1) // checks if player has jumed more then once.
            {
                jumpedAmount += 1;
                velocity.y += Mathf.Sqrt(jumpHeight * -4 * gravity);
                if (jumpSoundEffect != null) // checks if a sound is already playing
                {
                    AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position, 1f); // plays jump sound when input detected
                }
            }
        }

        //settingf the velocity of the fall
        Controller.Move(velocity * Time.deltaTime);
        animatorC.SetBool("isGrounded", isGrounded);
        animatorC.SetFloat("speed", horizontalInput);
    }
}
