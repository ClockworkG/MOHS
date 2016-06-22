using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerSync : NetworkBehaviour {
    [Command]
	public void CmdSync1()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("SyncLight1");
        GameObject door = GameObject.FindGameObjectWithTag("SyncDoor1");
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponentInChildren<Light>().enabled = true;
        }
        door.GetComponentInChildren<HorizontalAnim>().locked = false;
    }

    [Command]
    public void CmdChangeScene(string scene)
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ServerChangeScene(scene);
    }

    [Command]
    public void CmdSyncRover()
    {
        GameObject rover = GameObject.FindGameObjectWithTag("Rover");

        rover.GetComponentInChildren<Light>().enabled = true;
        rover.GetComponent<RoverDisplace>().battery = true;
    }

    [Command]
    public void CmdSyncRoverPos(float x, float y, float z)
    {
        GameObject rover = GameObject.FindGameObjectWithTag("Rover");
        rover.GetComponent<RoverDisplace>().Translate(x, y, z);
    }

    [Command]
    public void CmdSyncRoverRot(float x, float y, float z)
    {
        GameObject rover = GameObject.FindGameObjectWithTag("Rover");
        rover.GetComponent<RoverDisplace>().Rotate(x, y, z);
    }

    [Command]
    public void CmdSyncDoor2()
    {
        GameObject.FindGameObjectWithTag("SyncDoor2").GetComponentInChildren<HorizontalAnim>().locked = false;
    }

    [Command]
    public void CmdSyncVentilation(int num, bool acc)
    {
        GameObject.Find("Ventilation" + num.ToString()).GetComponent<Ventilation>().acc = acc;
        GameObject.Find("Ventilation" + num.ToString()).GetComponent<Ventilation>().decc = !acc;
    }

    [Command]
    public void CmdSyncDoorPos(float x, float y, float z, int n)
    {
        string tag = "";
        switch (n)
        {
            case 0:
                tag = "DoorRed";
                break;
            case 1:
                tag = "DoorBlue";
                break;
            case 2:
                tag = "DoorGreen";
                break;
            case 3:
                tag = "DoorYellow";
                break;
        }
        GameObject[] doors = GameObject.FindGameObjectsWithTag(tag);
        doors[0].transform.Translate(-x, -y, -z);
        doors[1].transform.Translate(x, y, z);
    }


    [Command]
    public void CmdMoveElevator(int number, float x, float y, float z, bool new_down, bool new_up)
    {
        GameObject platform = GameObject.Find("Elevator" + number.ToString());
        platform.transform.parent.GetComponent<Elevator>().down = new_down;
        platform.transform.parent.GetComponent<Elevator>().up = new_up;
        platform.transform.Translate(x, y, z);
    }

    [Command]
    public void CmdSyncValveDone(int number)
    {
        GameObject.Find("Valve" + number.ToString()).GetComponent<Valve>().done = true;
    }

    [Command]
    public void CmdSyncValveRot(int number, bool new_rot)
    {
        GameObject.Find("Valve" + number.ToString()).GetComponent<Valve>().rot = new_rot;
    }

    [Command]
    public void CmdSyncRotPanel(int num, int dir, int rot)
    {
        GameObject pan = GameObject.Find("Solar" + num.ToString());

        pan.GetComponentInChildren<SolarRotation>().CDirection = dir;
        pan.GetComponentInChildren<SolarRotation>().rotating = rot;
        pan.GetComponent<SolarRotation>().txt.text  = "Solar Panel Num. " + num.ToString() + "\n\n" + "Impossible operation : panel->display_voltage()" + "\n" + "Exception : x86475264" + "\n" + "Use arrows to rotate" + "\nCurrent rotation : " + dir.ToString();
    }
}
