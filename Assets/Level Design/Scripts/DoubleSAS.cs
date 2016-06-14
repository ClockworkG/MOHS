using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DoubleSAS : NetworkBehaviour {
    public int inc = 1;
    public int required = 0;
    public HorizontalAnim door1;
    public HorizontalAnim door2;
    private bool opened1;

    void OnTriggerEnter(Collider other)
    {
        required += inc;
        if (required == 2)
        {
            door1.locked = true;
            door2.locked = false;
            inc = -1;
            required = 0;
        }   
        else if (required == -2)
        {
            door2.locked = true;
            door1.locked = false;
            inc = 1;
            required = 0;
        } 
    }

    void OnTriggerExit(Collider other)
    {
        required = 0;
    }
}
