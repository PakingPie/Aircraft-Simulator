using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAAshot : MonoBehaviour
{
    public bool isShootting = false;
    public GameObject shot;
    public Transform shotSpawn;
    public AudioClip gunSoundClip;
    public float fireRate = 0.02f;
    private float nextFire = 0.0f;
    AudioSource audioController;
    bool soundPlayed = false;
    private void Start()
    {
        audioController = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isShootting)
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (!soundPlayed)
                {
                    audioController.PlayOneShot(gunSoundClip);
                    soundPlayed = true;
                }
                else 
                    soundPlayed = false;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
    }
}
