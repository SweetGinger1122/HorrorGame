using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnspeed = 0f;


    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    Rigidbody rb;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        bool hasInputHorizontal = !Mathf.Approximately(horizontal, 0f);
        bool hasInputVertical = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasInputHorizontal || hasInputVertical;

        if (isWalking)
        {
            animator.SetBool("IsWalking", true);
        }
        else if (!isWalking)
        {
            animator.SetBool("IsWalking", false);
        }

        Vector3 desiedForward = Vector3.RotateTowards(transform.forward, movement, turnspeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiedForward);
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
