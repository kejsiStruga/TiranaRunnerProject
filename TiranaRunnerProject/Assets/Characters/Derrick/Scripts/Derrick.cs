using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derrick : MonoBehaviour {

    private Animator _animmator;
    private CharacterController _controller;

    public float Speed = 1.0f;
    public float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animmator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        _moveDirection.y -= Gravity * Time.deltaTime;
    }
}
