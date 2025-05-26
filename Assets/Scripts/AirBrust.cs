using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBrust : MonoBehaviour
{
    public GameObject explosion;
    public float explosionRadius;
    public float explosionForce;
    public LayerMask ammoMask;
    System.Random rnd = new System.Random();
    int failChance;
    void Start()
    {
        failChance = rnd.Next(10);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            AircraftCondition.TakenDamage(ammoPhysics.damage);
            if (explosion)
                Instantiate(explosion, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, ammoMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigid = colliders[i].GetComponent<Rigidbody>();
                if (!targetRigid)
                    continue;

                targetRigid.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(failChance > 5)
        {
        
            if (other.gameObject.tag == "Player")
            {
                AircraftCondition.TakenDamage(ammoPhysics.damage);
                if (explosion)
                    Instantiate(explosion, transform.position, transform.rotation);
                Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, ammoMask);

                for (int i = 0; i < colliders.Length; i++)
                {
                    Rigidbody targetRigid = colliders[i].GetComponent<Rigidbody>();
                    if (!targetRigid)
                        continue;

                    targetRigid.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
