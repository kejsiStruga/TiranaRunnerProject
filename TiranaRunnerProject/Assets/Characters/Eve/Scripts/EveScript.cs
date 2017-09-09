using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveScript : MonoBehaviour {

    private Animator anim;
    private CharacterController controller;

    public float speed = 6.0f;
    public float turnSpeed = 60.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        anim.SetInteger("AnimParameter", Input.GetKey("up") ? 1 : 0);

        //if (controller.isGrounded)
        //{
        //moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        //

        transform.position += transform.forward * speed * Time.deltaTime;

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        //controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
