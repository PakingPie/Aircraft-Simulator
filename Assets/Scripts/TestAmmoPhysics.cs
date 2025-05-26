using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmmoPhysics : MonoBehaviour
{
    public int speed = 200;
    Transform target;
    public int damage = 10;
    public float explosion_radius = 0f;
    public float detect_radius = 5f;
    public GameObject impact_effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void seek(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        if (!target)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (dir.magnitude <= distance)
        {
            hitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
        transform.LookAt(target);
    }

    void hitTarget()
    {
        GameObject effect = Instantiate(impact_effect, transform.position, transform.rotation);
        Destroy(effect.gameObject, 2f);
        AircraftCondition.TakenDamage(damage);
        Destroy(this.gameObject);
    }

    void explodeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosion_radius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                singleDamage(collider.transform);
            }
        }
    }
    void singleDamage(Transform enemy)
    {
        AircraftCondition.TakenDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detect_radius);
    }
}
