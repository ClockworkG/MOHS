using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	void Start () {
        if (GameObject.Find("PlayerContain") != null)
            GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.transform.position = transform.position;
	}
}
