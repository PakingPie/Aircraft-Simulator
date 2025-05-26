using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIndicatorController : MonoBehaviour
{
    public static bool isEnabled = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
            this.enabled = true;
        else
            this.enabled = false;
    }
}
