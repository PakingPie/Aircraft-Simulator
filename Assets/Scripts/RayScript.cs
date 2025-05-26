using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayScript : MonoBehaviour
{
    Ray ray;
    float rayLength = 500f;
    RaycastHit hit;
    float timer = 0.0f;
    public Transform plane;
    public Image crosshair;
    int scale = 0;
    bool isHitted = false;
    // Update is called once per frame
    void Update()
    {
        ray = new UnityEngine.Ray(plane.position, transform.forward);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            hit.transform.SendMessage("HitByRaycast", gameObject, SendMessageOptions.DontRequireReceiver);
            if(hit.transform.gameObject.tag == "enemy")
            {
                crosshair.color = Color.red;
            }
            isHitted = true;
            /*
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            print(hit.transform.name);
            */
        }
        else
        {
            crosshair.color = Color.white;
            isHitted = false;
        }

        if(isHitted && scale < 30 && Time.time > timer)
        {
            timer += 0.2f;
            crosshair.rectTransform.sizeDelta = new Vector2(crosshair.rectTransform.sizeDelta.x + 1, crosshair.rectTransform.sizeDelta.y + 1);
            scale++;
        }
        if(!isHitted && scale > 0 && Time.time > timer)
        {
            timer += 0.1f;
            crosshair.rectTransform.sizeDelta = new Vector2(crosshair.rectTransform.sizeDelta.x - 1, crosshair.rectTransform.sizeDelta.y - 1);
            scale--;
        }
    }
}
