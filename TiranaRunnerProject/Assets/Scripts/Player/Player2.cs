using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{

    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20f;
    private CharacterController controller;
    private Animator anim;

    private bool isChangingLane = false;
    private Vector3 locationAfterChangingLane;
    //distance character will move sideways
    private Vector3 sidewaysMovementDistance = Vector3.right * 2f;

    public float SideWaysSpeed = 5.0f;

    public float JumpSpeed = 8.0f;
    public float Speed = 6.0f;
    //Max gameobject
    public Transform CharacterGO;
    bool check = false;


    // Use this for initialization
    void Start()
    {
        moveDirection = transform.forward;
        // moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;


        anim = CharacterGO.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
            anim.SetBool("started", true);
            check = true;
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

     //       DetectJumpOrSwipeLeftRight();

            //apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            if (isChangingLane)
            {
                if (Mathf.Abs(transform.position.x - locationAfterChangingLane.x) < 0.1f)
                {
                    isChangingLane = false;
                    moveDirection.x = 0;
                }
            }

            //move the player
            controller.Move(moveDirection * Time.deltaTime);

        }

    //private void DetectJumpOrSwipeLeftRight()
    //{
    //    if (controller.isGrounded && !isChangingLane)
    //    {
    //        isChangingLane = true;

    //        if (CrossPlatformInputManager.GetAxis!=null)
    //        {
    //            locationAfterChangingLane = transform.position - sidewaysMovementDistance;
    //            moveDirection.x = -SideWaysSpeed;
    //        }
    //        else if (inputDirection == InputDirection.Right)
    //        {
    //            Debug.Log("RIGHT");
    //            locationAfterChangingLane = transform.position + sidewaysMovementDistance;
    //            moveDirection.x = SideWaysSpeed;
    //        }
    //    }
    //}

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if we hit the left or right border
        //if (hit.gameObject.tag == Constants.WidePathBorderTag)
        //{
        //    isChangingLane = false;
        //    // moveDirection.x = 0;
        //}
    }

}
