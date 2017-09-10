using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Assets.CoinsScripts;

public class Player : MonoBehaviour
{
	private CharacterController controller;
	public static float Speed;
	private Animator anim;
	public Transform CharacterGO;
	private Vector3 moveVector;
	private float verticalVelocity = 0.0f;
	private float gravity = 1.0f;
	public float JumpSpeed = 8.0f;
	private bool hasJumped = false;
	private float canJump = 0f;
	public bool isDead = false;
	public GameObject completeLevelUI;
	private float stepHeight = 0.1f;
	private float deltaStep = 0.3f;
	private bool toggle = false;
	private float jumpSpeed;
	private float leftRightSpeed;

    // Use this for initialization
    void Start()
    {
        anim = CharacterGO.GetComponent<Animator>();
        controller = GetComponentInChildren<CharacterController>();
        Speed =5f;
        leftRightSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
	controller.stepOffset = stepHeight + (toggle? 0 :  deltaStep);
	toggle = !toggle;

        anim.SetInteger("AnimParameter", Input.GetButtonUp("Jump") ? 1 : 0);
      
        if (isDead) { return; }
        if (controller.isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        Movement();
    }

    private void Movement()
    {
        anim.SetInteger("AnimParameter", Input.GetButtonUp("Jump") ? 1 : 0);
        moveVector = Vector3.zero;

        if (Input.GetButtonUp("Jump") && Time.time > canJump)
        {
            hasJumped = true;

            verticalVelocity += gravity * Time.deltaTime;
            moveVector.y += 10;
            canJump = Time.time + 1.5f;
        }
        else
        {
            moveVector.y = verticalVelocity;
        }
        //X - Left & Right (A D)
        moveVector.x = Input.GetAxis("Horizontal") * leftRightSpeed;
		if (!controller.isGrounded && Mathf.Abs ( moveVector.x) > 0.01f)
			return;
        //Z - Forward & Backward
        moveVector.z = Speed; //since we're going forward !!
        //anim.SetBool("started", true);
        controller.Move(moveVector * Time.deltaTime);
    }
}
