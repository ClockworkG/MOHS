using UnityEngine;
using System.Collections;

public class EnigmaButton : MonoBehaviour
{
    public Animation anim;
    public Transform right_pan;
    public Transform left_pan;
    public bool press = false;
    public float P = 0;
    public float lim;
    public bool close = false;
    public float speed;
    private float elapsed = 0;
    public float timeLimit;

    void FixedUpdate()
    {
        if (press && P < lim)
            Open();
        else if (P >= lim)
            press = false;
        if (P >= lim && elapsed < timeLimit)
            elapsed += Time.fixedDeltaTime;
        else if (elapsed >= timeLimit)
            close = true;
        if (close && P > 0)
            Close();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (elapsed == 0)
            {
                press = true;
                anim.Play();
            }
        }  
    }

    void Open()
    {
        left_pan.Translate(0, 0, -speed);
        right_pan.Translate(0, 0, speed);
        P=P+speed;
    }

    void Close()
    {
        left_pan.Translate(0, 0, speed);
        right_pan.Translate(0, 0, -speed);
        P = P - speed;
        if (P <= 0)
        {
            elapsed = 0;
            close = false;
        }
    }
}

