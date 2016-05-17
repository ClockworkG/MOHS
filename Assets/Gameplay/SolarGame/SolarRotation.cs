using UnityEngine;
using System.Collections;

public class SolarRotation : MonoBehaviour {
    public int[] Direction = new int[4];
    public int CDirection = 1;
    public Transform Solar;
    public int rotating = 0;
    public float delta = 0f;
    public float rotationspeed = 0f;

    void OnTriggerStay(Collider other) {
        if (rotating == 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            rotating = 1;
            CDirection++;
            DirectionCamp();
        }
        else if (rotating == 0 && Input.GetKey(KeyCode.RightArrow))
        {
            rotating = 2;
            CDirection--;
            DirectionCamp();
        }
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
        rotationspeed = 4.6f - Mathf.Abs(4.5f - delta / 10f);
        RotationClamp();
        if (delta >= 90)
        {
            rotating = 0;
            delta = 0;
        }
    }

    void RotateLeft()
    {
        Solar.Rotate(0, 0, -rotationspeed);
        delta += rotationspeed;
        rotationspeed = 4.6f - Mathf.Abs(4.5f - delta / 10f);
        RotationClamp();
        if (delta >= 90)
        {
            rotating = 0;
            delta = 0;
        }
    }

    void RotationClamp()
    {
        if (rotationspeed > 0.5f)
            rotationspeed = 0.5f;
    }

    void DirectionCamp()
    {
        if (CDirection > 3)
            CDirection = 0;
        else if (CDirection < 0)
            CDirection = 3;
    }
}
