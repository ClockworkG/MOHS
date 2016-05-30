using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkObject : NetworkBehaviour {
    private MOHSNetworkManager net;
	void Start () {
        net = GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>();
	}

    void FixedUpdate()
    {
        if (isServer &&  net.numPlayers > 1)
        {
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
        }
    }
}
