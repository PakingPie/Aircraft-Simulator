using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftMovement : MonoBehaviour
{
    public aimController controller;
    public Vector3 turnTorque = new Vector3(75, 20f, 40f);
    public float enginePower = 500.0f;
    public float sensitivity = 1f;
    public float aggressiveTurnAngle = 5f;

    public Transform[] Flap = new Transform[2];
    public Transform[] Gear = new Transform[3];

    public Transform[] AssistFlap;
    public Transform[] TailFlap;
    public Transform TailWing;
    bool isRolling = false;
    bool rollDirection = false; //false = roll right, true = roll left
    bool isPitching = false;
    bool PitchDirection = false; //false = pitch up, true = pitch down

    int assistFlapRollLeft = 0;
    int assistFlapRollRight = 0;
    int tailFlapPitchUp = 0;
    int tailFlapPitchDown = 0;


    public float speed = 0.0f;

    float timer = 0f;
    float timer_2 = 0f;
    float timer_3 = 0f;
    int flapPos = 0;
    bool isFlapDown = false;

    int gearPos = 0;
    bool isGearUp = false;

    public int throttle = 0;
    float pitch = 0f;
    float yaw = 0f;
    private float roll = 0f;
   

    Rigidbody rigid;

    bool rollOverride = false;
    bool pitchOverride = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        if (isGearUp == false)
            sensitivity -= 0.5f;
    }
    public void setFlap(bool con)
    {
        isFlapDown = con;
    }
    public void setGear(bool con)
    {
        isGearUp = con;
    }
    private void Update()
    {
        speed = rigid.linearVelocity.magnitude;
        rollOverride = false;
        pitchOverride = false;

        float HorizontalRoll = Input.GetAxis("Horizontal");
        if (Mathf.Abs(HorizontalRoll) > .25f)
        {
            rollOverride = true;
        }

        float VerticalPitch = -Input.GetAxis("Vertical");
        if (Mathf.Abs(VerticalPitch) > .25f)
        {
            pitchOverride = true;
            rollOverride = true;
        }

        float yawCorreclation = 0f;
        float pitchCorreclation = 0f;
        float rollCorreclation = 0f;

        planeCorreclation(controller.aimViewPos(), out yawCorreclation, out pitchCorreclation, out rollCorreclation);

        yaw = yawCorreclation;
        pitch = (pitchOverride) ? VerticalPitch : pitchCorreclation;
        roll = (rollOverride) ? HorizontalRoll : rollCorreclation;
    }

    void planeCorreclation(Vector3 target, out float yaw, out float pitch, out float roll)
    {
        var localTarget = transform.InverseTransformPoint(target).normalized * sensitivity;
        var angleTarget = Vector3.Angle(transform.forward, target - transform.position);

        yaw = Mathf.Clamp(localTarget.x, -1f, 1f);
        pitch = -Mathf.Clamp(localTarget.y, -1f, 1f);

        var agressiveRoll = Mathf.Clamp(localTarget.x, -1f, 1f);
        var wingsRoll = transform.right.y;
        var wingsInfluence = Mathf.InverseLerp(0f, aggressiveTurnAngle, angleTarget);
        roll = Mathf.Lerp(wingsRoll, agressiveRoll, wingsInfluence);
    }

    private void FixedUpdate()
    {
        GameObject propeller = GameObject.Find("Propeller");
 //       GameObject additionPropeller = GameObject.Find("Addition Propeller");
        PropellerSpin propellerSpin = propeller.GetComponent<PropellerSpin>();
 //       PropellerSpin additionPropellerSpin = additionPropeller.GetComponent<PropellerSpin>();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKey(KeyCode.UpArrow))
        {
            throttle = throttle >= 100 ? 100 : throttle + 2;
            propellerSpin.rollSpeed = propellerSpin.rollSpeed > 1.0f ? 1.0f : propellerSpin.rollSpeed + throttle/10;
 //           additionPropellerSpin.rollSpeed = additionPropellerSpin.rollSpeed > 1.0f ? 1.0f : additionPropellerSpin.rollSpeed + throttle / 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKey(KeyCode.DownArrow))
        {
            throttle = throttle <= 0 ? 0 : throttle - 2;
            propellerSpin.rollSpeed = propellerSpin.rollSpeed <= 0.0f ? 0.0f : propellerSpin.rollSpeed - 0.01f;
//            additionPropellerSpin.rollSpeed = additionPropellerSpin.rollSpeed <= 0.0f ? 0.0f : additionPropellerSpin.rollSpeed - throttle / 10;
        }

        //Flap Control
        if (isFlapDown == false && Input.GetKeyDown(KeyCode.F))
            isFlapDown = true;
        if(flapPos <80 && Time.time > timer &&  isFlapDown == true)
        {
            timer += 0.1f;
            sensitivity += 0.02f;
            Flap[0].transform.position = Flap[0].transform.position + Flap[0].TransformDirection(new Vector3(-0.01f, 0, 0));
            Flap[1].transform.position = Flap[1].transform.position + Flap[1].TransformDirection(new Vector3(-0.01f, 0, 0));
            flapPos++;
            enginePower -= 1f;
        }

        if (isFlapDown == true && Input.GetKeyDown(KeyCode.H))
            isFlapDown = false;
        if (flapPos > 0 && Time.time > timer && isFlapDown == false)
        {
            timer += 0.1f;
            sensitivity -= 0.02f;
            Flap[0].transform.position = Flap[0].transform.position + Flap[0].TransformDirection(new Vector3(0.01f, 0, 0));
            Flap[1].transform.position = Flap[1].transform.position + Flap[1].TransformDirection(new Vector3(0.01f, 0, 0));
            flapPos--;
            enginePower += 1f;
        }
        //Gear Control
        if (isGearUp == false && Input.GetKeyDown(KeyCode.G))
            isGearUp = true;
        if (gearPos < 90 && Time.time > timer && isGearUp == true)
        {
            timer += 0.1f;
            sensitivity += 0.02f;
            Gear[0].transform.Rotate(Vector3.right);
            Gear[1].transform.Rotate(Vector3.left);
            Gear[2].transform.Rotate(Vector3.forward / 4);
            gearPos++;
            enginePower += 2f;
        }
        if (isGearUp == true && Input.GetKeyDown(KeyCode.T))
            isGearUp = false;
        if (gearPos > 0 && Time.time > timer && isGearUp == false)
        {
            timer += 0.1f;
            sensitivity -= 0.02f;
            Gear[0].transform.Rotate(Vector3.left);
            Gear[1].transform.Rotate(Vector3.right);
            Gear[2].transform.Rotate(- Vector3.forward / 4);
            gearPos--;
            enginePower -= 2f;
        }

        //Propeller & engine 
        if (propellerSpin.isPropellerSpin)
        {
            rigid.AddRelativeForce(Vector3.forward * throttle * enginePower, ForceMode.Force);
            rigid.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * enginePower, ForceMode.Force);
        }

        float isPlayerControllingHorizontalMove = Input.GetAxis("Horizontal");
        float isPlayerControllingVerticalMove = Input.GetAxis("Vertical");
        //autoFlap & tail
        playerControlledCorrection();

    }
    void playerControlledCorrection()
    {
        float HorizontalRoll = transform.rotation.z;
        float VerticalPitch = rigid.linearVelocity.y;
        if (VerticalPitch > 10.0f)
        {
            isPitching = true;
            PitchDirection = false;
        }
        else if (VerticalPitch < -10.0f)
        {
            isPitching = true;
            PitchDirection = true;
        }
        else
            isPitching = false;

        if (isPitching && !PitchDirection && Time.time > timer_3 && tailFlapPitchDown < 20)
        {
            timer_3 += 0.1f;
            tailFlapPitchDown++;
            TailFlap[0].Rotate(2 * Vector3.up);
            TailFlap[1].Rotate(2 * Vector3.up);
        }
        else if (isPitching && PitchDirection && Time.time > timer_3 && tailFlapPitchUp < 20)
        {
            timer_3 += 0.1f;
            tailFlapPitchUp++;
            TailFlap[0].Rotate(2 * Vector3.down);
            TailFlap[1].Rotate(2 * Vector3.down);
        }

        if (!isPitching && Time.time > timer_3 && tailFlapPitchUp > 0)
        {
            timer_3 += 0.05f;
            tailFlapPitchUp--;
            TailFlap[0].Rotate(2 * Vector3.up);
            TailFlap[1].Rotate(2 * Vector3.up);
        }
        else if (!isPitching && Time.time > timer_3 && tailFlapPitchDown > 0)
        {
            timer_3 += 0.05f;
            tailFlapPitchDown--;
            TailFlap[0].Rotate(2 * Vector3.down);
            TailFlap[1].Rotate(2 * Vector3.down);
        }


        //Assist Flap & tail flap moving
        if (HorizontalRoll > 0.1f)
        {
            isRolling = true;
            rollDirection = false;
        }
        else if (HorizontalRoll < -0.1f)
        {
            isRolling = true;
            rollDirection = true;
        }
        else
            isRolling = false;

        if (isRolling && !rollDirection && Time.time > timer_2 && assistFlapRollRight < 30)
        {
            timer_2 += 0.1f;
            assistFlapRollRight++;
            AssistFlap[0].Rotate(2 * Vector3.up);
            AssistFlap[1].Rotate(2 * Vector3.down);
            TailWing.Rotate(2 * Vector3.up);
        }
        else if (isRolling && rollDirection && Time.time > timer_2 && assistFlapRollLeft < 30)
        {
            timer_2 += 0.1f;
            assistFlapRollLeft++;
            AssistFlap[0].Rotate(2 * Vector3.down);
            AssistFlap[1].Rotate(2 * Vector3.up);
            TailWing.Rotate(2 * Vector3.down);
        }

        if (!isRolling && Time.time > timer_2 && assistFlapRollRight > 0)
        {
            timer_2 += 0.05f;
            assistFlapRollRight--;
            AssistFlap[0].Rotate(2 * Vector3.down);
            AssistFlap[1].Rotate(2 * Vector3.up);
            TailWing.Rotate(2 * Vector3.down);
        }
        else if (!isRolling && Time.time > timer_2 && assistFlapRollLeft > 0)
        {
            timer_2 += 0.05f;
            assistFlapRollLeft--;
            AssistFlap[0].Rotate(2 * Vector3.up);
            AssistFlap[1].Rotate(2 * Vector3.down);
            TailWing.Rotate(2 * Vector3.up);
        }
    }
    void autoCorrectFlapAndTails()
    {
        float HorizontalRoll = transform.rotation.z;
        float VerticalPitch = rigid.linearVelocity.y;
        if (VerticalPitch > 10.0f)
        {
            isPitching = true;
            PitchDirection = false;
        }
        else if (VerticalPitch < -10.0f)
        {
            isPitching = true;
            PitchDirection = true;
        }
        else
            isPitching = false;

        if (isPitching && !PitchDirection && Time.time > timer_3 && tailFlapPitchDown < 20)
        {
            timer_3 += 0.1f;
            tailFlapPitchDown++;
            TailFlap[0].Rotate(2 * Vector3.down);
            TailFlap[1].Rotate(2 * Vector3.down);
        }
        else if (isPitching && PitchDirection && Time.time > timer_3 && tailFlapPitchUp < 20)
        {
            timer_3 += 0.1f;
            tailFlapPitchUp++;
            TailFlap[0].Rotate(2 * Vector3.up);
            TailFlap[1].Rotate(2 * Vector3.up);
        }

        if (!isPitching && Time.time > timer_3 && tailFlapPitchUp > 0)
        {
            timer_3 += 0.05f;
            tailFlapPitchUp--;
            TailFlap[0].Rotate(2 * Vector3.down);
            TailFlap[1].Rotate(2 * Vector3.down);
        }
        else if (!isPitching && Time.time > timer_3 && tailFlapPitchDown > 0)
        {
            timer_3 += 0.05f;
            tailFlapPitchDown--;
            TailFlap[0].Rotate(2 * Vector3.up);
            TailFlap[1].Rotate(2 * Vector3.up);
        }


        //Assist Flap & tail flap moving
        if (HorizontalRoll > 0.1f)
        {
            isRolling = true;
            rollDirection = false;
        }
        else if (HorizontalRoll < -0.1f)
        {
            isRolling = true;
            rollDirection = true;
        }
        else
            isRolling = false;

        if (isRolling && !rollDirection && Time.time > timer_2 && assistFlapRollRight < 30)
        {
            timer_2 += 0.1f;
            assistFlapRollRight++;
            AssistFlap[0].Rotate(2 * Vector3.down);
            AssistFlap[1].Rotate(2 * Vector3.up);
            TailWing.Rotate(2 * Vector3.down);
        }
        else if (isRolling && rollDirection && Time.time > timer_2 && assistFlapRollLeft < 30)
        {
            timer_2 += 0.1f;
            assistFlapRollLeft++;
            AssistFlap[0].Rotate(2 * Vector3.up);
            AssistFlap[1].Rotate(2 * Vector3.down);
            TailWing.Rotate(2 * Vector3.up);
        }

        if (!isRolling && Time.time > timer_2 && assistFlapRollRight > 0)
        {
            timer_2 += 0.05f;
            assistFlapRollRight--;
            AssistFlap[0].Rotate(2 * Vector3.up);
            AssistFlap[1].Rotate(2 * Vector3.down);
            TailWing.Rotate(2 * Vector3.up);
        }
        else if (!isRolling && Time.time > timer_2 && assistFlapRollLeft > 0)
        {
            timer_2 += 0.05f;
            assistFlapRollLeft--;
            AssistFlap[0].Rotate(2 * Vector3.down);
            AssistFlap[1].Rotate(2 * Vector3.up);
            TailWing.Rotate(2 * Vector3.down);
        }
    }
}
