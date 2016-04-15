using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DigiCode : MonoBehaviour {
    private bool activated = false;
    public VerticalAnim vert;
    public HorizontalAnim horiz;
    public string code = "";
    private float elapsed = 0f;
    public MeshRenderer mesh;
    private bool done = false;
    public string scene;
	// Use this for initialization
	void Start () {
        if (code == "")
            code = "0000";
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        elapsed += 0.1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!done)
            mesh.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        mesh.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKeyUp(KeyCode.E) && elapsed >= 0.2f && !done)
        {
            elapsed = 0;
            activated = !activated;
            other.gameObject.GetComponentInChildren<Canvas>().enabled = activated;
            Cursor.visible = activated;
            if (activated)
            {
                Cursor.lockState = CursorLockMode.None;
                other.gameObject.GetComponentInChildren<DigiCodeInterface>().code = code;
                other.gameObject.GetComponentInChildren<DigiCodeInterface>().valid_code = false;
                other.gameObject.GetComponentInChildren<DigiCodeInterface>().scene = scene;
            } 
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                done = other.gameObject.GetComponentInChildren<DigiCodeInterface>().valid_code;
                if (vert != null)
                    vert.locked = !done;
                else
                    horiz.locked = !done;
                    
            }        
        }
    }
}
