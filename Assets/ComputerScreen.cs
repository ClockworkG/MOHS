using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class ComputerScreen : MonoBehaviour {

    public Text input;
    public Text Ouput;
    public Text Status;
    bool IPC = false;
    bool inHelp = false;
    Dictionary<string, string> help;

    void Start()
    {
        help = new Dictionary<string, string>();
        help.Add("help", "Display help for each command, press \"Return\" to see more.\nType <command> ? to get specific help");
        help.Add("ipconfig", "Print your Internet Protocol configuration in the status Door.");
    }


	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }
	}
    
    private void Enter()
    {
        if (input.text == "ipconfig")
        {
            IPC = true;
            Ouput.text = "Succesfull command : " + input.text + '\n' + Ouput.text;
        }
        else if (input.text == "help")
        {
            Ouput.text = "Succesfull command : " + input.text + '\n' + Ouput.text;
            Help();
        }
        else
            Ouput.text = "Unknown command : " + input.text + '\n' + Ouput.text;
        if (IPC)
            IPconfig();
    }

    private void Help()
    {
        
    }

    private void IPconfig()
    {
        int i = 1;
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                Status.text += "Connection " + i.ToString() + " (Wireless) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        Status.text += "- IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        Status.text += "- IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            else if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                Status.text += "Connection " + i.ToString() + " (Ethernet) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        Status.text += "IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        Status.text += "IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            i++;
        }
    }
}
