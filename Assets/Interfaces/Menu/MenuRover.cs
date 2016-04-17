using UnityEngine;
using System.Collections;

public class MenuRover : MonoBehaviour {
    private float speed_tr = 0.08f;
    private float speed_rt = 2f;
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(0, -speed_tr, 0);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(0, speed_tr, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-speed_rt, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(speed_rt, 0, 0);
    }
}
