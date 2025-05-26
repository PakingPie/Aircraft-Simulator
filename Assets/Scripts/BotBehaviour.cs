using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BotBehaviour : MonoBehaviour
{
    Transform player;
    AAAshot shottingCondition;
    public Transform gun;
    public float hitPoint = 1000;
    public float currentHitPoint = 0;
    public int scoreValue = 0;
    public GameObject targetExplosion;
    public Slider healthIndicator;
    public Image healthIndicatorColor;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthIndicator.maxValue = hitPoint;
        shottingCondition = GetComponent<AAAshot>();
        currentHitPoint = hitPoint;
    }

    // Update is called once per frame
    void Update()
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
    public void takenDamage(int damage)
    {
        currentHitPoint -= damage;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            gun.LookAt(player.position);
            shottingCondition.isShootting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        shottingCondition.isShootting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "gunAmmo")
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
