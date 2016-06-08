using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class IPrecup : MonoBehaviour {

	// Use this for initialization
	void Start () {
    
    GetComponent<Text>().text = "Local IP adress : "+Dns.GetHostAddresses(Dns.GetHostName())[2].ToString();
	}
}
