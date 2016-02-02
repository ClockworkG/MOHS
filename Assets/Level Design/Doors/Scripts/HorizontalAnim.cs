using UnityEngine;
using System.Collections;

public class HorizontalAnim : MonoBehaviour {
	public float right_open = -1;
	private bool open = false;
	public Transform right_pan;
	public Transform left_pan;
    public Light pannel_light1;
    public Light pannel_light2;
    private Vector3 init_right;
	private Vector3 init_left;
	public  float speed;
	private float moved = 0;
	private bool moving = false;
	public bool locked;
    public char axis = 'x';

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
		init_left = left_pan.position;
		init_right = right_pan.position;
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
			moved += delta_mov;
            if (axis == 'z')
            {
                right_pan.Translate(0, 0, right_open * delta_mov);
                left_pan.Translate(0, 0, -right_open * delta_mov);
            }
            else
            {
                right_pan.Translate(right_open * delta_mov, 0, 0);
                left_pan.Translate(-right_open * delta_mov, 0, 0);
            }
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
			moved -= delta_mov;
            if (axis == 'z')
            {
                right_pan.Translate(0, 0, -right_open * delta_mov);
                left_pan.Translate(0, 0, right_open * delta_mov);
            }
			else
            {
                right_pan.Translate(-right_open * delta_mov, 0, 0);
                left_pan.Translate(right_open * delta_mov, 0, 0);
            }
		} 
		else 
		{
			right_pan.position = init_right;
			left_pan.position = init_left;
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
            ChangeLight(Color.red);
	}

    void OnTriggerStay(Collider other)
    {
        if (!locked)
        {
            moving = true;
            open = false;
        }

    }

	void OnTriggerExit(Collider other)
	{
		open = true;
		moving = true;
        ChangeLight(Color.yellow);
	}
}
