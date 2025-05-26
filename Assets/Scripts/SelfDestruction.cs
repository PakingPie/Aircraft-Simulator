using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    float t = 0f;
    public float timeSelfDestruction = 2.0f;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > timeSelfDestruction)
        {
            Destroy(gameObject);
        }

    }
}
