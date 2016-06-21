using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class MiniGame : NetworkBehaviour
{
    public int[,] solvedIndex = new int[3, 3];
    public int[,] nbMat = new int[3, 3];
    public Color[,] colMat = new Color[3, 3];
    public Image[] imgs;
    public Text[] nbTexts = new Text[9];
    public Text[] resTexts = new Text[3];
    static bool[,] visitMat = new bool[3, 3];
    public int[] resList = new int[3];
    private int length = 3;
    private bool[] solved = new bool[3];
    public bool gameSolved = false;
    public Material lit;

    public void initializeNb()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                nbMat[i, j] = UnityEngine.Random.Range(1, 13);
            }
        }
    }

    public void initializeVisit()
    {
        for (int i = 0; i < length; i++)
        {
            solved[i] = false;
            for (int j = 0; j < 3; j++)
            {
                visitMat[i, j] = false;
            }
        }
    }

    public void shuffle()
    {
        int[,] newMat = new int[3, 3];
        int rand1 = (int)UnityEngine.Random.Range(0, 4);
        int rand2 = (int)UnityEngine.Random.Range(0, 4);
        for (int i = 0; i < 3; i++)
        {
            newMat[i, 0] = nbMat[i, 0];
            newMat[(i + rand1) % 3, 1] = nbMat[i, 1];
            newMat[(i + rand2) % 3, 2] = nbMat[i, 2];
        }
        nbMat = newMat;
    }

    public void moveUp()
    {
        int[] newList = new int[3];
        for (int i = 0; i < 3; i++)
            newList[(i + 2) % 3] = nbMat[i, 0];
        for (int i = 0; i < 3; i++)
            nbMat[i, 0] = newList[i];
        display();
        checkWin();
    }

    public void moveDown()
    {
        int[] newList = new int[3];
        for (int i = 0; i < 3; i++)
            newList[(i + 1) % 3] = nbMat[i, 0];
        for (int i = 0; i < 3; i++)
            nbMat[i, 0] = newList[i];
        display();
        checkWin();
    }

    public void display()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                nbTexts[i * 3 + j].text = nbMat[i, j].ToString();
            }
        }
    }

    public void initializeResList()
    {
        int j = 0;
        for (int i = 0; i < length; i++)
        {
            j = 0;
            while (visitMat[j, 0])
                j++;
            resList[i] = nbMat[j, 0];
            visitMat[j, 0] = true;

            //colonne 2
            j = 0;
            while (visitMat[j, 1])
                j++;
            resList[i] *= nbMat[j, 1];
            visitMat[j, 1] = true;

            //colonne 3
            j = 0;
            while (visitMat[j, 2])
                j++;
            resList[i] += nbMat[j, 2];
            visitMat[j, 2] = true;
            resTexts[i].text = resList[i].ToString();

        }
    }

    public void changeColor(GameObject sender)
    {
        int x = Int32.Parse(sender.transform.GetChild(0).name[0].ToString());
        int y = Int32.Parse(sender.transform.GetChild(0).name[2].ToString());

        Color col = sender.transform.GetChild(0).GetComponent<Image>().color;
        if (col == Color.white)
        {
            if (!solved[0])
                col = Color.blue;
            else if (!solved[1])
                col = Color.green;
            else if (!solved[2])
                col = Color.red;
        }
        else if (col == Color.blue)
        {
            if (!solved[0])
            {
                if (!solved[1])
                    col = Color.green;
                else if (!solved[2])
                    col = Color.red;
            }

        }
        else if (col == Color.green)
        {
            if (!solved[1])
            {
                if (!solved[2])
                    col = Color.red;
                else if (!solved[0])
                    col = Color.blue;
            }

        }
        else if (col == Color.red)
        {
            if (!solved[2])
            {
                if (!solved[0])
                    col = Color.blue;
                else if (!solved[1])
                    col = Color.green;
            }

        }
        foreach (Image i in sender.transform.GetComponentsInChildren<Image>())
        {
            i.color = col;
        }
        colMat[x, y] = col;
        checkWin();
    }

    // Use this for initialization
    void Start()
    {
        initializeVisit();
        initializeNb();
        initializeResList();
        shuffle();
        display();
    }

    void checkWin()
    {
        Color[] cols = new Color[3];
        cols[0] = Color.blue;
        cols[1] = Color.green;
        cols[2] = Color.red;

        for (int i = 0; i < 3; i++)
        {
            if (!solved[i])
            {
                int temp1 = 0;
                int temp2 = 0;
                int[] mat = new int[3];
                for (int y = 1; y < 3; y++)
                {
                    int x = 0;
                    while (x < 3 && colMat[x, y] != cols[i])
                        x++;
                    if (x < 3)
                    {
                        if (y == 1)
                            temp1 = y;
                        if (y == 2)
                            temp2 = y;
                        mat[y] = nbMat[x, y];
                    }  
                    else
                        mat[y] = 0;
                }
                solved[i] = (resList[i] == (nbMat[i, 0] * mat[1] + mat[2]));
                if (solved[i])
                {
                    solvedIndex[i, 0] = i;
                    solvedIndex[i, 1] = temp1;
                    solvedIndex[i, 2] = temp2;
                }
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (solved[i])
                imgs[i].material = lit;
        }
        gameSolved = solved[0] && solved[1] && solved[2];
        if (gameSolved)
        {
            if (GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().isServer && GameObject.Find("NetworkManager").GetComponent<MOHSNetworkManager>().numPlayers > 1)
                RpcSyncDoor();
            else if (!GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().isServer)
                GameObject.Find("PlayerContain").GetComponent<PlayerContain>().player_obj.GetComponent<PlayerSync>().CmdSyncDoor2();
            else
                GameObject.FindGameObjectWithTag("SyncDoor2").GetComponentInChildren<HorizontalAnim>().locked = false;
        }

    }

    [ClientRpc]
    public void RpcSyncDoor()
    {
        GameObject.FindGameObjectWithTag("SyncDoor2").GetComponentInChildren<HorizontalAnim>().locked = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
