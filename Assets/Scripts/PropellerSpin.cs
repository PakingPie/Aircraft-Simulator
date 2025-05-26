using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    float spinSpeed = 0.0f;
    public bool isPropellerSpin = false;
    public float rollSpeed = 0.1f;
    float timer = 0.0f;
    public Vector3 vecDirect = Vector3.up;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vecDirect * spinSpeed * Time.deltaTime * rollSpeed);
        if (Input.GetKeyDown(KeyCode.I) && isPropellerSpin == false)
        {
            isPropellerSpin = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isPropellerSpin == true)
        {
            isPropellerSpin = false;
        }
        timer += Time.deltaTime;
        if (isPropellerSpin && spinSpeed < 2000.0f)
            if(timer > 1f)
                spinSpeed += 10.0f;
        if(!isPropellerSpin && spinSpeed >= 0.0f)
            if (timer > 1f)
                spinSpeed -= 10.0f;
    }
}
