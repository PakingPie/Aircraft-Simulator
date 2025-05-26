using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABehaviors : MonoBehaviour
{
    Transform target;
    AircraftCondition target_player;

    [Header("Tower Type")]
    public Transform rotate_turret;

    [Header("Tower Attributes")]
    public float fire_rate = 1.0f;
    public float fire_countdown = 0.0f;
    public float rotate_speed;
    public float active_range = 1000.0f;

    [Header("Ammo")]
    public GameObject bullet;
    public Transform fire_spawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    void updateTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float shortest_distance = Mathf.Infinity;
        GameObject nearest_player = null;

        float distance_to_enemy = Vector3.Distance(transform.position, player.transform.position);
        if (distance_to_enemy < shortest_distance)
        {
            shortest_distance = distance_to_enemy;
            nearest_player = player;
        }
        

        if (nearest_player && shortest_distance <= active_range)
        {
            target = nearest_player.transform;
            target_player = nearest_player.GetComponent<AircraftCondition>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        lockOn();

        if (fire_countdown <= 0f)
        {
            Shoot();
            //audio.PlayOneShot(gun_sound_clip);
            fire_countdown = 1f / fire_rate;
        }
        fire_countdown -= Time.deltaTime;

    }

    void lockOn()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion look_rotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotate_turret.rotation, look_rotation, Time.deltaTime * rotate_speed).eulerAngles;
        rotate_turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject fired_object = Instantiate(bullet, fire_spawn.position, fire_spawn.rotation);
        if (fired_object.tag == "Ammo")
        {
            TestAmmoPhysics ammo = fired_object.GetComponent<TestAmmoPhysics>();
            if (ammo)
                ammo.seek(target);
        }
        else if (fired_object.tag == "Missile")
        {
            TestAmmoPhysics ammo = fired_object.GetComponent<TestAmmoPhysics>();
            if (ammo)
                ammo.seek(target);
            // self destruct after 15 seconds
            Destroy(fired_object, 15.0f);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, active_range);
    }
}
