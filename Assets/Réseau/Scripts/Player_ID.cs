using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour {
    [SyncVar]
    public string playerUniqueName;

    private NetworkInstanceId playerNetID;
    private Transform myTransform;
    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }
    // Use this for initialization
    void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (myTransform.name == "" || myTransform.name == "Player(Clone)")
            SetIdentity();
	}

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyName(MakeUniqueName());
    }

    string MakeUniqueName()
    {
        string uniqueName = "Player " + playerNetID.ToString();
        return uniqueName;
    }

    [Command]
    void CmdTellServerMyName(string name)
    {
        playerUniqueName = name;
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueName;
        }
        else
        {
            myTransform.name = MakeUniqueName();
        }
    }
}
