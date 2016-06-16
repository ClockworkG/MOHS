using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class ComputerScreen : MonoBehaviour {

    public Text input;
    public Text ouput;
    public Text status;
    bool IPC = false;
    string subHelp;
    Dictionary<string, string> help;

    void Start()
    {
        help = new Dictionary<string, string>();
        help.Add("help", "Display help for each command.\nType <command> ? to get specific help.");
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
        if(input.text=="?")
            ouput.text = "Why ?" + '\n' + ouput.text;
        else if (input.text.Length > 1 && input.text.Substring(input.text.Length - 2) == " ?")
        {
            if (input.text.Length > 2)
            {
                subHelp = input.text.Substring(0, input.text.Length - 2);
                //ouput.text = subHelp +'.'+'\n' + ouput.text;
                try
                {
                ouput.text = subHelp+" : "+help[subHelp] + '\n' + ouput.text;
                }
                catch (KeyNotFoundException e)
                {
                    ouput.text = "Unknown command : " + subHelp + '\n' + ouput.text;
                }
            }
            else
                ouput.text = "Why ?" + '\n' + ouput.text;
        }
        else if (input.text == "ipconfig")
        {
            IPC = true;
            ouput.text = "Succesfull command : " + input.text + '\n' + ouput.text;
        }
        else if (input.text == "help")
        {
            ouput.text = "Succesfull command : " + input.text + '\n' + ouput.text;
            Help();
        }
        else
            ouput.text = "Unknown command : " + input.text + '\n' + ouput.text;
        Status();
        if (IPC)
            IPconfig();
    }

    private void Help()
    {
        ouput.text ='\n' + ouput.text;
        foreach (KeyValuePair<string, string> key in help)
        {
            ouput.text = key.Key+" : " + key.Value + '\n' + ouput.text;
        }
        ouput.text = '\n' + ouput.text;
    }

    private void IPconfig()
    {
        int i = 1;
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                status.text += "Connection " + i.ToString() + " (Wireless) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        status.text += "- IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        status.text += "- IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            else if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                status.text += "Connection " + i.ToString() + " (Ethernet) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        status.text += "IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        status.text += "IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            i++;
        }
    }

    private void Status()
    {
        status.text = "";
    }
}
