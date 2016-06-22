using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class ComputerScreen : NetworkBehaviour {
    public PlayerSync player;
    public Ventilation[] ventilationList;
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
        player = GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>();
        ventilationList = new Ventilation[9];
        help = new Dictionary<string, string>();
        help.Add("help", "Display help for each command.\nType \"<command> ?\" to get specific help.");
        help.Add("ipconfig", "Print your Internet Protocol configuration in the status Door.");
        help.Add("tempchk", "Check the current temperature of both the server room and the CPUs and print them in the output Door.");
        help.Add("start", "Launches the specified program : start <program>");
        help.Add("tree", "Print the directory tree from this computer.");
        help.Add("stpvnt", "Stops the rotation of the specified ventilation : stpvnt <number>");
        help.Add("strvnt", "Starts the rotation of the specified ventilation : strvnt <number>");
        for (int i = 0; i < 9; i++)
            ventilationList[i] = GameObject.Find("Ventilation" + (i + 1).ToString()).GetComponent<Ventilation>();
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
        else if (input.text.Length >7 && input.text.Substring(0,7)== "stpvnt ")
        {
            if (input.text.Substring(7) != "1" && input.text.Substring(7) != "2" && input.text.Substring(7) != "3" && input.text.Substring(7) != "4" && input.text.Substring(7) != "5" && input.text.Substring(7) != "6" && input.text.Substring(7) != "7" && input.text.Substring(7) != "8" && input.text.Substring(7) != "9")
                output.text = "Wrong argument. Type \"stpvnt ?\" to get help.\n" + output.text;
            else
            {
                output.text = "Successfull command : " + input.text + '\n' + output.text;
                if (player.isServer && GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>().numPlayers > 1)
                    RpcVentilation(int.Parse(input.text.Substring(7)), false);
                else if (!player.isServer)
                    player.CmdSyncVentilation(int.Parse(input.text.Substring(7)), false);
                else
                    StopVent(int.Parse(input.text.Substring(7)));
            }
        }
        else if (input.text == "stpvnt" || input.text == "stpvnt ")
            output.text = "Wrong argument. Type \"stpvnt ?\" to get help.\n" + output.text;
        else if (input.text.Length > 7 && input.text.Substring(0, 7) == "strvnt ")
        {
            if (input.text.Substring(7) != "1" && input.text.Substring(7) != "2" && input.text.Substring(7) != "3" && input.text.Substring(7) != "4" && input.text.Substring(7) != "5" && input.text.Substring(7) != "6" && input.text.Substring(7) != "7" && input.text.Substring(7) != "8" && input.text.Substring(7) != "9")
                output.text = "Wrong argument. Type \"strvnt ?\" to get help.\n" + output.text;
            else
            {
                output.text = "Successfull command : " + input.text + '\n' + output.text;
                if (player.isServer && GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>().numPlayers > 1)
                    RpcVentilation(int.Parse(input.text.Substring(7)), true);
                else if (!player.isServer)
                    player.CmdSyncVentilation(int.Parse(input.text.Substring(7)), true);
                else
                    StartVent(int.Parse(input.text.Substring(7)));
                
            }
        }
        else if (input.text == "strvnt" || input.text == "strvnt ")
            output.text = "Wrong argument. Type \"strvnt ?\" to get help.\n" + output.text;
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

    [ClientRpc]
    private void RpcVentilation(int number, bool acc)
    {
        ventilationList[number - 1].decc = !acc;
        ventilationList[number - 1].acc = acc;
    }

    private void StopVent(int number)
    {
        ventilationList[number - 1].decc = true;
        ventilationList[number - 1].acc = false;
    }

    private void StartVent(int number)
    {
        ventilationList[number - 1].decc = false;
        ventilationList[number - 1].acc = true;
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
