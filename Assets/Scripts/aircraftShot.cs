using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class aircraftShot : MonoBehaviour
{
    public int gunAmmo = 200;
    public int mgAmmo = 1200;
    public GameObject []shot = new GameObject[4];
    public Transform []shotSpawn = new Transform[8];
    public float []fireRate = new float[2];
    private float []nextFire = { 0.0f, 0.0f, 0.0f, 0.0f};
    private bool []isFired= {false, false, false, false};

    GameObject tempShot1, tempShot2;
    private bool soundPlayed1 = false;
    private bool soundPlayed2 = false;
    public float gunReloadTime = 40.0f;
    public float mgReloadTime = 20.0f;
    int[] mgPair1 = { 2, 4, 6 };
    int[] mgPair2 = { 3, 5, 7 };
    int numPicker = 0;
    float timer = 0;

    AudioSource audioController;
    public AudioClip[] gunSoundClip;

    public ParticleSystem[] gunFire = new ParticleSystem[2];
    private void Start()
    {
        audioController = GetComponent<AudioSource>();
        gunFire[0].Stop();
        gunFire[1].Stop();
    }

    void Update()
    {
        if (gunAmmo <= 0)
        {
            gunReloadTime -= Time.deltaTime;
            if(gunReloadTime <= 0)
            {
                gunAmmo = 200;
                gunReloadTime = 20.0f;
            }
        }

        if (mgAmmo <= 0)
        { 
            mgReloadTime -= Time.deltaTime;
            if(mgReloadTime <= 0)
            {
                mgAmmo = 1200;
                mgReloadTime = 10.0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && (gunAmmo !=200 || mgAmmo!=1200))
        {
            gunAmmo = 0;
            mgAmmo = 0;
        }

        if ((Input.GetKey(KeyCode.Alpha1) || Input.GetButton("Fire1")) && Time.time > nextFire[0] && gunAmmo > 0)
        {
            if (!soundPlayed1)
            {
                if(gunAmmo%4==0)
                {
                    audioController.PlayOneShot(gunSoundClip[0]);
                    soundPlayed1 = true;
                }
            }
            else
                soundPlayed1 = false;
            gunFire[0].Play();
            gunFire[1].Play();
            nextFire[0] = Time.time + fireRate[0];
            gunAmmo--;

            if (gunAmmo % 3 == 0)
                tempShot1 = shot[0];
            else
                tempShot1 = shot[2];

            if(isFired[0]==false)
            {
               Instantiate(tempShot1, shotSpawn[0].position, shotSpawn[0].rotation);
                isFired[0] = true;
            } 
            else
            {
                Instantiate(tempShot1, shotSpawn[1].position, shotSpawn[1].rotation);
                isFired[0] = false;
            }
        }
        if ((Input.GetKey(KeyCode.Alpha2) || Input.GetButton("Fire1")) && Time.time > nextFire[1] && mgAmmo > 0)
        {
            if (!soundPlayed2)
            {
                if(mgAmmo % 12 == 0)
                {
                    audioController.PlayOneShot(gunSoundClip[1]);
                    soundPlayed2 = true;
                }
            }
            else
                soundPlayed2 = false;

            if (mgAmmo % 5 == 0)
            {
                tempShot2 = shot[1];
            }
            else
                tempShot2 = shot[3];
            timer += Time.deltaTime;

            nextFire[1] = Time.time + fireRate[1];
            mgAmmo--;
            numPicker++;
            numPicker = numPicker % 3;

            if (isFired[1] == false)
            {
                Instantiate(tempShot2, shotSpawn[mgPair1[numPicker]].position, shotSpawn[mgPair1[numPicker]].rotation);
                isFired[1] = true;
            }
            else
            {
                Instantiate(tempShot2, shotSpawn[mgPair2[numPicker]].position, shotSpawn[mgPair2[numPicker]].rotation);
                isFired[1] = false;
            }
        }
            /*
            if ((Input.GetKey(KeyCode.Alpha2) || Input.GetButton("Fire1")) && mgAmmo > 0)
            {
                if (mgAmmo % 7 == 0)
                    tempShot2 = shot[1];
                else
                    tempShot2 = shot[3];
                timer += Time.deltaTime;

                if (timer > 0.01f && count == 0)
                {
                    if (Time.time > nextFire[1])
                    {
                        playMachineGunSound();
                        nextFire[1] = Time.time + fireRate[1];
                        mgAmmo -= 1;
                        if (isFired[1] == false)
                        {
                            Instantiate(tempShot2, shotSpawn[2].position, shotSpawn[2].rotation);
                            isFired[1] = true;
                        }
                        else
                        {
                            Instantiate(tempShot2, shotSpawn[3].position, shotSpawn[3].rotation);
                            isFired[1] = false;
                        }
                    }
                    count++;
                }
                if (timer > 0.05f && count == 1)
                {
                    if (Time.time > nextFire[2])
                    {
                        playMachineGunSound();
                        nextFire[2] = Time.time + fireRate[1];
                        mgAmmo -= 1;
                        if (isFired[2] == false)
                        {
                            Instantiate(tempShot2, shotSpawn[4].position, shotSpawn[4].rotation);
                            isFired[2] = true;
                        }
                        else
                        {
                            Instantiate(tempShot2, shotSpawn[5].position, shotSpawn[5].rotation);
                            isFired[2] = false;
                        }
                    }
                    count++;
                }
                if (timer > 0.09f && count == 2)
                {
                    if (Time.time > nextFire[3])
                    {
                        playMachineGunSound();
                        nextFire[3] = Time.time + fireRate[1];
                        mgAmmo -= 1;
                        if (isFired[3] == false)
                        {
                            Instantiate(tempShot2, shotSpawn[6].position, shotSpawn[6].rotation);
                            isFired[3] = true;
                        }
                        else
                        {
                            Instantiate(tempShot2, shotSpawn[7].position, shotSpawn[7].rotation);
                            isFired[3] = false;
                        }
                    }
                    count = 0;
                    timer = 0;
                }
            }
            */
            if ((Input.GetKeyUp(KeyCode.Alpha1) || Input.GetButtonUp("Fire1")))
        {
            gunFire[0].Stop();
            gunFire[1].Stop();
        }
    }

    void playMachineGunSound()
    {
        if (!soundPlayed2)
        {
            audioController.PlayOneShot(gunSoundClip[1]);
            soundPlayed2 = true;
        }
        else
            soundPlayed2 = false;
    }
}
