using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 3f;
    private float JumpForce = 100f;
    private Rigidbody _rb;
    private Animator animator;
    private bool _isMoving = false;
    private bool _isGrounded;
    PhotonView view;

    void Start()
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        _rb = GetComponent<Rigidbody>();
       
    }

    void FixedUpdate()
    {
      
        MovementLogic();
        JumpLogic();
        SprintUpdate();
    }

    private void SprintUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift)&&_isMoving)
        {
            speed = 5;
            animator.SetBool("_isRunning", true);
        }
        else
        {
           speed = 3;
            animator.SetBool("_isRunning", false);
        }
    }
    private void MovementLogic()
    {
        if (view.IsMine)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            transform.Translate(movement * speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
           
            if (_isMoving)
            {
                animator.SetBool("_IsWalking", true);
            }
            else
            {
                animator.SetBool("_IsWalking", false);
            }
        }
    }

    private void JumpLogic()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}
