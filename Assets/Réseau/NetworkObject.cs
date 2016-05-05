using UnityEngine;
using System.Collections;

public class NetworkObject : MonoBehaviour {
    public bool soloObject;
	void Start () {
        if (!soloObject && GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>().numPlayers == 2)
            Destroy(gameObject);
	}
}
