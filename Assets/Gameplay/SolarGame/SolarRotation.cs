using UnityEngine;
using System.Collections;

public class SolarRotation : MonoBehaviour {
    public Transform Solar;
    public int rotating = 0;
    public int rotationY = 0;
    public float delta = 0f;
    public float rotationspeed = 1f;

    void OnTriggerStay(Collider other) {
        if (rotating == 0 && Input.GetKeyDown(KeyCode.LeftArrow))
            rotating = 1;
        else if (rotating == 0 && Input.GetKeyDown(KeyCode.RightArrow))
            rotating = 2;
        Debug.Log(rotating);
    }

    void FixedUpdate()
    {
        if (rotating == 1)
            RotateRight();
        else if (rotating == 2)
            RotateLeft();

    }

    void RotateRight()
    {
        Solar.Rotate(0, 0, rotationspeed);
        delta += rotationspeed;
        if (delta > 90)
        {
            rotating = 0;
            delta = 0;
        }
    }

    void RotateLeft()
    {
        Solar.Rotate(0, 0, -rotationspeed);
        delta += rotationspeed;
        if (delta > 90)
        {
            rotating = 0;
            delta = 0;
        }
    }
}
