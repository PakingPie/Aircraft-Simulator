using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 2.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    //public Animator anim;

    // Update is called once per frame
    void Update()
    {
        float horizonal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizonal, 0f, vertical).normalized;


        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //anim.SetBool("moving", true);

            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
        /*else
        {
            anim.SetBool("moving", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("running", false);
            speed = 2f;
        }
        */

    }
}
