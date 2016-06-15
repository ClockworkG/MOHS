using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class IPrecup : MonoBehaviour {

	// Use this for initialization
	void Start () {
    
    GetComponent<Text>().text = "Local IP adress : "+Dns.GetHostAddresses(Dns.GetHostName())[2].ToString();
	}
}
