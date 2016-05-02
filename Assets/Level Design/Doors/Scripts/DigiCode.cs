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
    private Canvas digi;
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
        digi = other.gameObject.GetComponentInChildren<Canvas>();
        if (!done)
            mesh.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        mesh.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (elapsed >= 0.1f)
        {
            elapsed = 0;
            if (digi.enabled && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
            {
                    digi.enabled = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                digi.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                other.gameObject.GetComponentInChildren<DigiCodeInterface>().code = code;
                other.gameObject.GetComponentInChildren<DigiCodeInterface>().valid_code = false;
                other.gameObject.GetComponentInChildren<DigiCodeInterface>().scene = scene;
            }
        }
    }
}
