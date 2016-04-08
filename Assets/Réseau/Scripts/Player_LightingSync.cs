using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_LightingSync : NetworkBehaviour {

    [SyncVar]
    private bool state_light;
    [SerializeField]
    private GameObject lights;

    // Update is called once per frame
    void FixedUpdate()
    {
        LerpState();
        TransmitLightState();
    }

    void LerpState()
    {
        if (!isLocalPlayer)
            GameObject.Find("Neon").GetComponentInChildren<Light>().enabled = state_light;
    }

    [Command]
    void CmdLightStateToServer(bool state)
    {
        state_light = state;
    }

    [Client]
    void TransmitLightState()
    {
        if (isLocalPlayer)
        {
            bool light = GameObject.Find("Neon").GetComponentInChildren<Light>().enabled;
            CmdLightStateToServer(light);
        }
    }
}
