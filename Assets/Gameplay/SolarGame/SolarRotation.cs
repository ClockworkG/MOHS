using UnityEngine;
using System.Collections;

public class SolarRotation : MonoBehaviour {
    public Transform Solar;
    public byte rotating = 0;
    public int rotationY = 0;
    public float delta = 0f;

    void OnTriggerStay(Collider other) {
        if (rotating != 0 && Input.GetKeyDown(KeyCode.LeftArrow))
            rotating = 1;
        else if (rotating != 0 && Input.GetKeyDown(KeyCode.RightArrow))
            rotating = 2;
    }

    void FixedUpdate()
    {
        if (rotating == 1)
            RotateRight();
        if (rotating == 2)
            RotateLeft();

    }

    void RotateRight()
    {
        Solar.Rotate(0, 0, 0.1f);
        delta += 0.1f;
        if (delta < 90)
        {
            rotating = 0;
            delta = 0;
        }
    }

    void RotateLeft()
    {
        Solar.Rotate(0, 0, -0.1f);
        delta += 0.1f;
        if (delta < 90)
        {
            rotating = 0;
            delta = 0;
        }
    }
}
