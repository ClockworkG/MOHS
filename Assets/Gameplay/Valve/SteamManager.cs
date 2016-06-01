using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SteamManager : NetworkBehaviour {
    public Valve[] valves;
    public AudioSource aud;
    public ParticleSystem part1;
    public ParticleSystem part2;
    public BoxCollider box;
    private PlayerSync sync;
	void FixedUpdate () {
	    for (int i = 0; i < 4; i++)
        {
            if (!valves[i].done)
                return;
        }
        if (sync == null)
        {
            aud.Stop();
            sync = GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>();
            part1.Stop();
            part2.Stop();
            box.enabled = false;
            if (sync.isServer)
                RpcSyncSteam();
            else
                sync.CmdStopSteam();
        }  
        
	}

    [ClientRpc]
    void RpcSyncSteam()
    {
        aud.Stop();
        part1.Stop();
        part2.Stop();
        box.enabled = false;
    }
}
