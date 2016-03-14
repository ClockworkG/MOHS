using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DigiCodeInterface : MonoBehaviour {
    public string code;
    private string current_code;
    public Text code_render;
    public bool valid_code = false;

    // Use this for initialization
    void Start () {
        current_code = "";
        code_render.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (current_code.Length < 10 && !valid_code)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
                current_code += 0;
            else if (Input.GetKeyDown(KeyCode.Keypad1))
                current_code += 1;
            else if (Input.GetKeyDown(KeyCode.Keypad2))
                current_code += 2;
            else if (Input.GetKeyDown(KeyCode.Keypad3))
                current_code += 3;
            else if (Input.GetKeyDown(KeyCode.Keypad4))
                current_code += 4;
            else if (Input.GetKeyDown(KeyCode.Keypad5))
                current_code += 5;
            else if (Input.GetKeyDown(KeyCode.Keypad6))
                current_code += 6;
            else if (Input.GetKeyDown(KeyCode.Keypad7))
                current_code += 7;
            else if (Input.GetKeyDown(KeyCode.Keypad8))
                current_code += 8;
            else if (Input.GetKeyDown(KeyCode.Keypad9))
                current_code += 9;
            code_render.text = current_code;
        }
        
	}

    public void AddNumber(string num)
    {
        if (current_code.Length < 10 && !valid_code)
        {
            current_code += num;
            code_render.text = current_code;
        }
    }

    public void Delete()
    {
        if (current_code.Length > 0 && !valid_code)
        {
            current_code = "";
            code_render.text = current_code;
        }
        
    }

    public void OK()
    {
        if (!valid_code)
        {
            valid_code = (code == current_code);
            code_render.text = (valid_code) ? "Access granted" : "Access denied";
            current_code = "";
        }
    }
}
