using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ammoPhysics : MonoBehaviour
{
    public static int damage = 10;
    public float speed;
    public GameObject explosion;
    public float explosionRadius;
    public float explosionForce;
    public LayerMask ammoMask;

    private void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag!="gunAmmo" && other.gameObject.tag != "mgAmmo")
            if(explosion)
                Instantiate(explosion, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, ammoMask);
        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigid = colliders[i].GetComponent<Rigidbody>();
            if (!targetRigid)
                continue;

            targetRigid.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}
