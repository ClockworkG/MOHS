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

    void Start()
    {
        valves = new Valve[4];
        for (int i = 1; i <= 4; i++)
            valves[i - 1] = GameObject.Find("Valve" + i.ToString()).GetComponent<Valve>();
    }

	void FixedUpdate () {
	    for (int i = 0; i < 4; i++)
        {
            if (!valves[i].done)
                return;
        }
        if (sync == null)
        {
            aud.Stop();
            part1.Stop();
            part2.Stop();
            box.enabled = false;
        }  
        
	}
}
