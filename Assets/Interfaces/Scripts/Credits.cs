using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
    public Canvas credits_canvas;

    void Start()
    {
        credits_canvas.enabled = false;
    }

    public void ShowCanvas()
    {
        credits_canvas.enabled = true;
    }

    public void Return()
    {
        credits_canvas.enabled = false;
    }
}
