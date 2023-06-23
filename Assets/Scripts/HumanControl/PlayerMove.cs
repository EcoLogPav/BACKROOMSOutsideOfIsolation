using Photon.Pun;
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

    void Update()
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
            if (Input.GetKey(KeyCode.W))
            {
                transform.localPosition += speed * transform.forward * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.localPosition += speed * -transform.forward * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.localPosition += speed * -transform.right * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.localPosition += speed * transform.right * Time.deltaTime;
            }
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                _isMoving=true;
                animator.SetBool("_isWalking", true);
            }
            else
            {
                _isMoving=false;
                animator.SetBool("_isWalking", false);
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
