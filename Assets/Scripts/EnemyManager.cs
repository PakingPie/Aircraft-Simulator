using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float spawnTime = 5f;
    public int spawnCount = 10;
    public GameObject []enemy;
    public Transform []spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if(spawnCount > 0)
        {
            spawnCount--;
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int spawnEnemyIndex = Random.Range(0, enemy.Length);
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy[spawnEnemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        
    }

}
