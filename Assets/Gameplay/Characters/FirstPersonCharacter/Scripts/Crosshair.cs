using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Crosshair : MonoBehaviour {
    Rect crosshairRect;
    public Texture crosshairTexture;
	// Use this for initialization
	void Start () {
        float crosshairSize = Screen.width * 0.1f;
        crosshairRect = new Rect(Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void OnGUI()
    {
        if (SceneManager.GetActiveScene().name != "Lobby")
            GUI.DrawTexture(crosshairRect, crosshairTexture);
    }
}
