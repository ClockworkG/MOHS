using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {
    private Camera player_cam;
    public float zoom_limit;
    public float dezoom_limit = 60;
    public float speed;
	// Use this for initialization
	void Start () {
        player_cam = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButton(1))
        {
            if (player_cam.fieldOfView > zoom_limit)
                player_cam.fieldOfView -= speed * Time.deltaTime;
        }
            
        else if (player_cam.fieldOfView < dezoom_limit)
            player_cam.fieldOfView += speed * Time.deltaTime;
    }
}
