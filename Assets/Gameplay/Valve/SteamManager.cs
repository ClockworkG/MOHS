using UnityEngine;
using System.Collections;

public class SteamManager : MonoBehaviour {
    public Valve[] valves;
    public ParticleSystem part1;
    public ParticleSystem part2;
    public BoxCollider box; 
	void FixedUpdate () {
	    for (int i = 0; i < 4; i++)
        {
            if (!valves[i].done)
                return;
        }
        part1.Stop();
        part2.Stop();
        box.enabled = false;
	}
}
