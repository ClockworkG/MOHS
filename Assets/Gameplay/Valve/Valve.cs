using UnityEngine;
using System.Collections;

public class Valve : MonoBehaviour {
    public Transform transf;
    private float delta;
    public float limit;
    public float speed;
    private bool rot;
    public bool done;
    public MeshRenderer txt;

    void Start()
    {
        done = false;
    }

    void FixedUpdate()
    {
        if (rot)
        {
            if (delta < limit)
            {
                transf.Rotate(0, 0, speed);
                delta += speed;
            }
            else
            {
                rot = false;
                done = true;
            }
                
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!done)
            txt.enabled = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!rot && Input.GetKeyDown(KeyCode.E))
            rot = true;
    }

    void OnTriggerExit(Collider other)
    {
        txt.enabled = false;
    }
}
