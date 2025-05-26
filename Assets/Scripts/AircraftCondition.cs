using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AircraftCondition : MonoBehaviour
{
    public float maxHitPoint = 1000.0f;
    public static float currentHitPoint = 0.0f;
    public Image Fill;
    public Slider healthSlider;
    public Text text;
    public AircraftMovement playerCondition;
    public Rigidbody playerRigid;
    public PropellerSpin propeller;
    float timer = 0.0f;
    public RawImage damageFlash;
    public static bool isTakenDamage = false;
    float countDown = 10;
    // Start is called before the first frame update
    void Start()
    {
        damageFlash.enabled = false;
        currentHitPoint = maxHitPoint;
        healthSlider.maxValue = maxHitPoint;
        healthSlider.value = maxHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHitPoint;
        Fill.color = Color.Lerp(new Color(0.8f, 0f, 0f), new Color(0.9f, 0.9f, 0.9f), (float)healthSlider.value / healthSlider.maxValue);
        if (currentHitPoint <= 0)
        {
            text.text = "YOU LOSE\nTHE GAME WILL RESTART AT "+ Math.Ceiling(countDown);
            playerCondition.enginePower = 0;
            playerCondition.setFlap(false);
            playerCondition.setGear(false);
            playerRigid.mass = 50000f;
            countDown -= Time.deltaTime;
            propeller.isPropellerSpin = false;
            if(countDown <= 0)
            {
                ScoreManager.score = 0;
                SceneManager.LoadScene("Menu");
            }
        }

        if(isTakenDamage)
        {
            damageFlash.enabled = true;
        }
        else if(!isTakenDamage)
        {
            damageFlash.enabled = false;
        }

        if(damageFlash.enabled)
        {
            if(Time.time>timer)
            {
                timer += 0.2f;
                isTakenDamage = false;
                damageFlash.enabled = false;
            }
        }
    }
    public static void TakenDamage(int damage)
    {
        currentHitPoint -= damage;
        isTakenDamage = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AAAAmmo")
        {
            TakenDamage(20);
            Destroy(collision.gameObject);
        }
    }
}
