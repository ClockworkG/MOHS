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
