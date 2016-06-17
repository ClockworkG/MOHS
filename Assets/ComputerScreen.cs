using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class ComputerScreen : MonoBehaviour {
    public InputField input;
    public Text output;
    public Text status;
    bool IPC = false;
    string subHelp;
    int time = 0;
    int timeLimit = 0;
    int CPUT = 1;
    int roomT = 1;
    string ipConfig;
    Dictionary<string, string> help;

    void FixedUpdate()
    {
        time++;
        Status();
    }
    void Start()
    {
        help = new Dictionary<string, string>();
        help.Add("help", "Display help for each command.\nType \"<command> ?\" to get specific help.");
        help.Add("ipconfig", "Print your Internet Protocol configuration in the status Door.");
        help.Add("tempchk", "Check the current temperature of both the server room and the CPUs and print them in the output Door.");
        help.Add("start", "Launches the specified program : start <program>");
        help.Add("tree", "Print the directory tree from this computer.");
    }
    
    public void Enter()
    {
        output.text = '\n' + output.text;
        if(input.text=="?")
            output.text = "Why ?" + '\n' + output.text;
        else if (input.text.Length > 1 && input.text.Substring(input.text.Length - 2) == " ?")
        {
            if (input.text.Length > 2)
            {
                subHelp = input.text.Substring(0, input.text.Length - 2);
                try
                {
                    output.text = subHelp+" : "+help[subHelp] + '\n' + output.text;
                }
                catch (KeyNotFoundException)
                {
                    output.text = "Unknown command : " + subHelp + '\n' + output.text;
                }
            }
            else
                output.text = "Why ?" + '\n' + output.text;
        }
        else if (input.text == "start mohs.exe")
            output.text = "Cannot start program \"mohs.exe\" : <error> You are already in the game !\n" + output.text;
        else if (input.text.Length>5 && input.text.Substring(0,5)=="start")
        {
                output.text = "Wrong argument. Type \"start ?\" to get help.\n" + output.text;

        }
        else if (input.text == "tree")
        {
            output.text = "Successfull command : " + input.text + '\n' + output.text;
            Tree();
        }
        else if (input.text == "ipconfig")
        {
            IPconfig();
            IPC = true;
            output.text = "Successfull command : " + input.text + '\n' + output.text;
        }
        else if (input.text == "help")
        {
            output.text = "Successfull command : " + input.text + '\n' + output.text;
            Help();
        }
        else if (input.text == "tempchk")
        {
            output.text = "Successfull command : " + input.text + '\n' + output.text;
            Tempchk();
        }
        else
            output.text = "Unknown command : " + input.text + '\n' + output.text;
        Status();
        input.text = "";
    }

    private void Help()
    {
        foreach (KeyValuePair<string, string> key in help)
        {
            output.text = key.Key+" : " + key.Value + '\n' + output.text;
        }
    }

    private void IPconfig()
    {
        int i = 1;
        ipConfig = "";
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                ipConfig += "Connection " + i.ToString() + " (Wireless) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        ipConfig += "- IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        ipConfig += "- IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            else if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                ipConfig += "Connection " + i.ToString() + " (Ethernet) : " + ni.Name + '\n';
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        ipConfig += "IPV4 : " + ip.Address.ToString() + '\n';
                    else if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        ipConfig += "IPV6 : " + ip.Address.ToString() + '\n';
                }
            }
            i++;
        }
    }

    private void Status()
    {
        if (time>timeLimit)
        {
            time = 0;
            timeLimit = Random.Range(20, 30);
                CPUT += Random.Range(-2, 3);
            roomT = Random.Range(19, 22);
            if (CPUT < 61)
                CPUT = 61;
            else if (CPUT > 79)
                CPUT = 79;
        }
        status.text = "Server room current temperature : " + roomT.ToString()+"°C\nCPUs current global temperature : "+CPUT.ToString()+"°C\n\n";
        if (IPC)
            status.text += ipConfig;
    }

    private void Tempchk()
    {
        output.text = "Server room temperature : " + roomT.ToString()+ "°C\nCPUs global temperature : " + CPUT.ToString() + "°C\n"+output.text;
    }

    private void Tree()
    {
        output.text = "C:\n├─log.txt\n├─Servers\n│   ├─Server1to5\n│   │   ├Server1to5_log.txt\n│   │   └Server1to5.ini\n│   ├─Server6to10\n│   │   ├Server6to10_log.txt\n│   │   └Server6to10.ini\n│   ├─Server11to15\n│   │   ├Server11to15_log.txt\n│   │   └Server11to15.ini\n│   └─Server16to20\n│       ├Server16to20_log.txt\n│       └Server16to20.ini\n├─mohs_data\n│   ├─mohs.unity\n│   ├─mohs.unity.meta\n│   └─mohs.sav\n└─mohs.exe\n" + output.text;
    }
}
