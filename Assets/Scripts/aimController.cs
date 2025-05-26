using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimController : MonoBehaviour
{
    public Transform aircraft;
    public Transform aimDot;
    public Transform cameraPos;
    public Camera myCamera;
    float camSmoothSpeed = 5.0f;
    float mouseSensitivity = 3.0f;
    public float aimDistance = 500.0f;
    bool isMouseAimFrozen = false;

    float timer = 0.0f;
    bool isZoomed = false;
    Vector3 frozenDirection = Vector3.forward;

    private void Awake()
    {
        transform.parent = null;
    }
    public Vector3 aimDotPos()
    {
        return aircraft == null ? transform.forward * aimDistance : (aircraft.transform.forward * aimDistance) + aircraft.transform.position;
    }

    public Vector3 aimViewPos()
    {
        if (!aimDot)
            return transform.forward * aimDistance;
            
        return isMouseAimFrozen ? GetFrozenMouseAimPos() : aimDot.position + (aimDot.forward * aimDistance);
    }

     Vector3 GetFrozenMouseAimPos()
    {
        if (aimDot != null)
            return aimDot.position + (frozenDirection * aimDistance);
        else
            return transform.forward * aimDistance;
    }

    Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt)
    {
        return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }
    void Update()
    {
        transform.position = aircraft.position;

        if (Input.GetKeyDown(KeyCode.C))
        {
            isMouseAimFrozen = true;
            Cursor.visible = true;
            frozenDirection = aimDot.forward;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isMouseAimFrozen = false;
            Cursor.visible = false;
            aimDot.forward = frozenDirection;
        }

        

        if((Input.GetButton("Fire2") || Input.GetKey(KeyCode.Z)) && Time.time > timer )
        {
            isZoomed = true;
            timer += 0.015f;
            if(myCamera.fieldOfView > 20)
                myCamera.fieldOfView --;
        }
        if (Input.GetButtonUp("Fire2") || Input.GetKeyUp(KeyCode.Z))
            isZoomed = false;

        if (!isZoomed && Time.time > timer)
        {
            timer += 0.01f;
            if (myCamera.fieldOfView < 60)
                myCamera.fieldOfView ++;
        }
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        aimDot.Rotate(myCamera.transform.right, mouseY, Space.World);
        aimDot.Rotate(myCamera.transform.up, mouseX, Space.World);

        Vector3 lookUp = (Mathf.Abs(aimDot.forward.y) > 0.9f) ? cameraPos.up : Vector3.up;

        cameraPos.rotation = Damp(cameraPos.rotation, Quaternion.LookRotation(aimDot.forward, lookUp), camSmoothSpeed, Time.deltaTime);


    }
}

