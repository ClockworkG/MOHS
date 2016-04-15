using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
    // Use this for initialization
    public Camera thisCamera;
    public Camera settingsCamera;
    public Camera mainCamera;
    public Camera quitCamera;
    public Camera bonusCamera;

    void Start()
    {
        thisCamera = gameObject.GetComponent<Camera>();
    }

	public void Settings()
    {
        settingsCamera.enabled = true;
        thisCamera.enabled = false;
    }

    public void Bonus()
    {
        bonusCamera.enabled = true;
        thisCamera.enabled = false;
    }

    public void Return()
    {
        mainCamera.enabled = true;
        thisCamera.enabled = false;
    }

    public void Quit()
    {
        thisCamera.enabled = false;
        quitCamera.enabled = true;
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
