using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public aimController aimController;
    public RectTransform aimView;
    public RectTransform aimDot;
    public RectTransform aimIndicator;
    Camera myCamera;

   

    private void Awake()
    {
        myCamera = aimController.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (aimDot)
        {
            aimDot.position = myCamera.WorldToScreenPoint(aimController.aimDotPos());
            aimDot.gameObject.SetActive(aimDot.position.z > 1f);
        }
        if(aimIndicator)
        {
            aimIndicator.position = aimDot.position;
        }
        if (aimView)
        {
            aimView.position = myCamera.WorldToScreenPoint(aimController.aimViewPos());
            aimView.gameObject.SetActive(aimView.position.z > 1f);
        }
    }
}
