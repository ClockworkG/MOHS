using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class ComputerScreen : MonoBehaviour {

    public Text input;
    public Text Ouput;
    public Text Status;


    // Use this for initialization
    void Start () {
        int i = 1;
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                Status.text += "Connexion " +i.ToString() + " (Wireless) : " + ni.Name+ '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        Status.text += "- IPV4 : "+ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        Status.text += "- IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            else if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                Status.text += "Connexion " + i.ToString() + " (Ethernet) : " + ni.Name +  '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        Status.text += "- IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        Status.text += "- IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            Status.text += '\n';
            i++;
        }
        //      Status.text += i.ToString()+" : "+ Dns.GetHostAddresses(Dns.GetHostName())[2].ToString() + '\n';
        Ouput.text = "test : 2";
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
                Enter();
	}
    
    private void Enter()
    {
        Ouput.text = input.text+'\n'+ Ouput.text;
    }
}
