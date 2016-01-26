using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncFlashlight : NetworkBehaviour {
    [SyncVar]
    private bool light_act;
    [SerializeField]
    private Light m_light;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (isLocalPlayer)
            TransmitState();
        else
            LerpState();
    }

    void LerpState()
    {
        if (!isLocalPlayer)
        {
            m_light.enabled = light_act;
        }
    }

    [Command]
    void CmdStateToServer(bool state)
    {
        light_act = state;
    }

    [Client]
    void TransmitState()
    {
        CmdStateToServer(m_light.enabled);
    }
}
