using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SolarScreen : MonoBehaviour {
    public int num;
    public Text txt;
    public bool globalScreen;
    public SolarRotation panel;
    public int result;
    public bool solved = false;
    public SolarScreen[] screen;
    public DigiCode digi;
    public Light doorLight;
    public Light neon;
    private int op;
	// Use this for initialization
	void Start () {
        if (globalScreen)
            txt.text = "Voltage needed : " + result.ToString() + " V";
        else
            panel = GameObject.Find("Solar" + num.ToString()).GetComponent<SolarRotation>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!globalScreen)
        {
            result = panel.Direction[panel.CDirection];
            txt.text = "\nPanel num. " + panel.m_num.ToString() + "\nVoltage : " + result.ToString() + " V";
        }
        else
        {
            op = screen[0].result * screen[1].result * screen[2].result * screen[3].result;
            txt.text = "Voltage needed : " + result.ToString() + " V\nCurrent voltage : " + op.ToString() + " V";
        }
        solved = (result == op);
        if (globalScreen)
        {
            if (solved)
            {
                digi.enabled = true;
                doorLight.enabled = true;
            }
            if (op <= result)
            {
                neon.enabled = true;
                neon.intensity = (float)(op / result);
            }
                
            else
                neon.enabled = false;
        }

    }
}
