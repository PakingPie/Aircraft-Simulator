using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AircraftInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public aircraftShot ammo;
    public AircraftMovement movement;
    public Text aircraftInfo;
    public Transform playerPos;
    void Start()
    {
        aircraftInfo.text = "THR: " + movement.throttle + "%\n" +
                                    "IAS: " + Math.Ceiling(movement.speed) + " km/h\n" +
                                    "GUN: " + ammo.gunAmmo + "\n" + 
                                    "MG: " + ammo.mgAmmo + "\n" +
                                    "ALT: " + Math.Ceiling(playerPos.localPosition.y) / 10 + "m\n" +
                                    "ALT: " + Math.Ceiling(playerPos.localPosition.y) / 10 + "m\n" +
                                    "X-Axis: " + Math.Ceiling(playerPos.localPosition.x) / 10 + "m\n" +
                                    "Y-Axis: " + Math.Ceiling(playerPos.localPosition.z) / 10 + "m";
    }

    // Update is called once per frame
    void Update()
    {
        if(ammo.gunAmmo > 0 && ammo.mgAmmo > 0)
            aircraftInfo.text = "THR: " + movement.throttle + "%\n" +
                                        "IAS: " + Math.Ceiling(movement.speed) + " km/h\n" +
                                        "GUN: " + ammo.gunAmmo + "\n" +
                                        "MG: " + ammo.mgAmmo + "\n" +
                                        "ALT: " + Math.Ceiling(playerPos.localPosition.y) / 10 + "m\n" +
                                        "X-Axis: " + Math.Ceiling(playerPos.localPosition.x) / 10 + "m\n" +
                                        "Y-Axis: " + Math.Ceiling(playerPos.localPosition.z) / 10 + "m";
        else if(ammo.gunAmmo > 0 && ammo.mgAmmo <= 0)
            aircraftInfo.text = "THR: " + movement.throttle + "%\n" +
                                        "IAS: " + Math.Ceiling(movement.speed) + " km/h\n" +
                                        "GUN: " + ammo.gunAmmo + "\n" +
                                        "MG: (" + Math.Ceiling(ammo.mgReloadTime) + ")\n" +
                                        "ALT: " + Math.Ceiling(playerPos.localPosition.y) / 10 + "m\n" +
                                        "X-Axis: " + Math.Ceiling(playerPos.localPosition.x) / 10 + "m\n" +
                                        "Y-Axis: " + Math.Ceiling(playerPos.localPosition.z) / 10 + "m";
        else if(ammo.gunAmmo <=0 && ammo.mgAmmo > 0)
            aircraftInfo.text = "THR: " + movement.throttle + "%\n" +
                                        "IAS: " + Math.Ceiling(movement.speed) + " km/h\n" +
                                        "GUN: (" + Math.Ceiling(ammo.gunReloadTime) + ")\n" +
                                        "MG: " + ammo.mgAmmo + "\n" +
                                        "ALT: " + Math.Ceiling(playerPos.localPosition.y) / 10 + "m\n" +
                                        "X-Axis: " + Math.Ceiling(playerPos.localPosition.x) / 10 + "m\n" +
                                        "Y-Axis: " + Math.Ceiling(playerPos.localPosition.z) / 10 + "m";
        else
            aircraftInfo.text = "THR: " + movement.throttle + "%\n" +
                                        "IAS: " + Math.Ceiling(movement.speed) + " km/h\n" +
                                        "GUN: (" + Math.Ceiling(ammo.gunReloadTime) + ")\n" +
                                        "MG: (" + Math.Ceiling(ammo.mgReloadTime) + ")\n" +
                                        "ALT: " + Math.Ceiling(playerPos.localPosition.y) / 10 + "m\n" +
                                        "X-Axis: " + Math.Ceiling(playerPos.localPosition.x) / 10 + "m\n" +
                                        "Y-Axis: " + Math.Ceiling(playerPos.localPosition.z) / 10 + "m";

    }
}
