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
    public float jumpedAmount = 0;


    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool outOfBounds;
    public float theHighScore;
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
    {
        outOfBounds = false;
        if (Physics.CheckSphere(transform.position, 0.1f, boundsLayers, QueryTriggerInteraction.Ignore))
        {
            ExitMenuUI.SetActive(true);
            Player.SetActive(false);
            Score.SetActive(false);
            HighestScore.SetActive(false);
            Time.timeScale = 0f;
            outOfBounds = true;
            theHighScore = GameObject.Find("PlatformGenerator").GetComponent<PlatformGeneration>().blockScore;
            PlayerPrefs.SetFloat("HighScore", theHighScore);
        }
        loadedTime = GameObject.Find("PlatformGenerator").GetComponent<PlatformGeneration>().blockScore;
        loadedTime = loadedTime /= 550;

        if (loadedTime > 1 && loadedTime < 2.2)
        {
            Time.timeScale = loadedTime;
        }




        //sets constant horrizontal speed
        horizontalInput = 1;
        //sets the facing of the charecter to be the directionm of movment.
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        //used to cheak if the player is touchiung any ground layers
        isGrounded = false;
        foreach (var groundCheck in groundChecks)
        {
            if (Physics.CheckSphere(groundCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                break;
               
            }
        }
        //used to cheak if the player is touchiung any walls 
        var blocked = false;
        foreach (var wallCheck in wallChecks)
        {
            if (Physics.CheckSphere(wallCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                break;
            }
        }

        if (!blocked)
        {
            Controller.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);
        }


        //if touching ground dfo not apply downwords velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
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
            jumpedAmount = 0;
        }
        if (jumpPressed)
        {
            jumpTimer = Time.time;
        }

        if (isGrounded && (jumpPressed || (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod)))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            if( jumpSoundEffect != null)
            {
                AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position, 1f);
            }
            jumpTimer = -1;
        }else if (jumpPressed || (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod))
        {
            if (jumpedAmount < 2)
            {
                jumpedAmount += 1;
                velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
                if (jumpSoundEffect != null)
                {
                    AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position, 1f);
                }
            }
        }

        //settingf the velocity of the fall
        Controller.Move(velocity * Time.deltaTime);

        animatorC.SetBool("isGrounded", isGrounded);
        animatorC.SetFloat("speed", horizontalInput);
    }
}
