using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ventilation : NetworkBehaviour
{
    public float acceleration;
    public float deceleration;
    public float lim;
    public bool acc;
    public bool decc;
    public Pale pal;

    void Start()
    {
        lim = pal.rot_speed;
    }

    void Accelerate()
    {
        if (pal.rot_speed < lim)
        {
            pal.rot_speed += acceleration;
        }
        else
        {
            acc = false;
            pal.rot_speed = lim;
        }
            
    }

    void Deccelerate()
    {
        if (pal.rot_speed > 0.3)
        {
            pal.rot_speed -= deceleration;
        }
        else
        {
            if ((pal.transform.rotation.eulerAngles.z > 269 && pal.transform.rotation.eulerAngles.z < 271) || (pal.transform.rotation.eulerAngles.z > 89 && pal.transform.rotation.eulerAngles.z < 91))
            {
                decc = false;
                pal.rot_speed = 0;
            }
        }
            
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            acc = true;
            decc = false;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            acc = false;
            decc = true;
        }
        if (acc)
            Accelerate();
        else if (decc)
            Deccelerate();
    }
}
