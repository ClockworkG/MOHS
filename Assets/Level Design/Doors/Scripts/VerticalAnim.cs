using UnityEngine;
using System.Collections;

public class VerticalAnim : MonoBehaviour {
	private bool open = false;
	public Transform pan;
	private Vector3 init;
	private Vector3 init_scale;
    public Light pannel_light1;
    public Light pannel_light2;
    public  float speed;
	private float moved = 0;
	private bool moving = false;
	public bool locked;

    public bool Opened
    {
        get { return open; }
    }

    public bool Locked
    {
        set
        {
            locked = value;           
        }
    }

    void ChangeLight(Color col)
    {
        pannel_light1.color = col;
        pannel_light2.color = col;
    }

    void Start()
	{
		init = pan.position;
		init_scale = pan.localScale;
        ChangeLight(Color.yellow);
    }

	void Update () 
	{
		if (!open && moving)
			Open ();
		else if (moving)
			Close ();
	}

	void Open()
	{
		if (moved < 1.5f) 
		{
			float delta_mov = speed * Time.deltaTime;
			float delta_scale = delta_mov / speed;
			moved += delta_mov;
			pan.Translate (0, delta_mov, 0);
			pan.localScale = new Vector3 (init_scale.x, pan.localScale.y - delta_scale, init_scale.z);
		}
		else 
		{
			open = true;
			moving = false;
		}
	}

	void Close()
	{
		if (moved > 0) 
		{
			float delta_mov = speed * Time.deltaTime;
			float delta_scale = delta_mov / speed;
			moved -= delta_mov;
			pan.Translate (0, -delta_mov, 0);
			pan.localScale = new Vector3 (init_scale.x, pan.localScale.y + delta_scale, init_scale.z);
		} 
		else 
		{
			pan.position = init;
			open = false;
			moving = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (!locked)
        {
            ChangeLight(Color.green);
            moving = true;
        }
        else
        {
            ChangeLight(Color.red);
        }
	}

	void OnTriggerExit(Collider other)
	{
        ChangeLight(Color.yellow);
		open = true;
		moving = true;
	}
}
