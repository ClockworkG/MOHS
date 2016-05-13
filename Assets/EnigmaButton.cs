using UnityEngine;
using System.Collections;

public class EnigmaButton : MonoBehaviour
{
    public bool condition_flag = true;
    public GameObject porte;
    Transform right_pan;
    Transform left_pan;
    private Vector3 init_right;
    private Vector3 init_left;
    public bool press = false;
    public float P = 0;
    // Use this for initialization
    void Start()
    {
        right_pan = porte.transform.FindChild("left");
        left_pan = porte.transform.FindChild("right");
        init_left = left_pan.position;
        init_right = right_pan.position;
    }

    void FixedUpdate()
    {
        if (press && P<1)
            Open();
        else if (!press && P>0)
            Close();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
            press = true;
        if (Input.GetMouseButtonUp(0))
            press = false;
    }

    void Open()
    {
        left_pan.Translate(0, 0.1f, 0);
        right_pan.Translate(0, -0.1f,0);
        P=P+0.1f;
    }

    void Close()
    {
        left_pan.Translate(0, -0.1f, 0);
        right_pan.Translate(0, 0.1f, 0);
        P = P - 0.1f;
    }
}

