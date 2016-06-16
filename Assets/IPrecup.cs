using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class IPrecup : MonoBehaviour {
    public Text IP;
	// Use this for initialization
	void Start () {
        int i = 1;
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                IP.text += "Connection " + i.ToString() + " (Wireless) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        IP.text += "- IPV4 : " + ip.Address.ToString() + '\n';
                }
            }
            else if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                IP.text += "Connection " + i.ToString() + " (Ethernet) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        IP.text += "- IPV4 : " + ip.Address.ToString() + '\n';
                }
            }
            IP.text += '\n';
            i++;
        }
    }
}
