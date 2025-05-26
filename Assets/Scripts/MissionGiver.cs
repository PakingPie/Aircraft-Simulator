using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionGiver : MonoBehaviour
{
    public Text mission;
    public Text Chat;
    public static bool isMissionGiven = false;
    public static bool isMissionFinished = false;
    public LevelManager level;
    public GameObject EnemyManage;
    public GameObject MainObject;
    private void Start()
    {
        Chat.text = "Hi, get your mission here!";
    }
    // Update is called once per frame
    void Update()
    {
        if(isMissionFinished)
            mission.text = "Mission Accomplished\nGo back to find the mission publisher";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isMissionGiven == false)
        {
            mission.text = "Mission 1: Destroy all enemies\nMission 2: Do Not Die";
            Chat.text = "Now you get your mission, stay alive!";
            isMissionGiven = true;
        }
        if (isMissionGiven)
            Chat.text = "Go and finish your mission!";
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && isMissionFinished == true)
        {
            Chat.text = "Great Work!!!";
            level.winCon();
        }
    }
}
