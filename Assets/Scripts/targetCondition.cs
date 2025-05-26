using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

public class targetCondition : MonoBehaviour
{
    AAAshot shottingCondition;
    public float hitPoint = 1000;
    public float currentHitPoint = 0;
    Transform player;
    NavMeshAgent nav;
    public int scoreValue = 0;
    public GameObject targetExplosion;
    public Transform gun;
    public Slider healthIndicator;
    public Image healthIndicatorColor;
    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shottingCondition = GetComponent<AAAshot>();
        currentHitPoint = hitPoint;
        healthIndicator.maxValue = hitPoint;
        nav = GetComponent<NavMeshAgent>();
    }

    public void takenDamage(int damage)
    {
        currentHitPoint -= damage;
        
    }

    // Update is called once per frame
    private void Update()
    {
        healthIndicator.value = currentHitPoint;
        healthIndicatorColor.color = Color.Lerp(new Color(0.8f, 0f, 0f), new Color(0.9f, 0.9f, 0.9f), (float)healthIndicator.value / healthIndicator.maxValue); ;
        if (currentHitPoint <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(targetExplosion, transform.position, transform.rotation);
            ScoreManager.score += scoreValue;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            shottingCondition.isShootting = true;
            nav.SetDestination(player.position);
            gun.LookAt(player.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            shottingCondition.isShootting = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "gunAmmo")
        {
            Destroy(collision.gameObject);
            HitIndicatorController.isEnabled = true;
            takenDamage(50);
        }
        if (collision.gameObject.tag == "mgAmmo")
        {
            Destroy(collision.gameObject);
            HitIndicatorController.isEnabled = true;
            takenDamage(10);
        }
    }
}
