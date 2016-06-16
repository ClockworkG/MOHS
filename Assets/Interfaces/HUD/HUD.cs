using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    public Text current_chapter;
    public float speed;
    public float elapsed;
    public bool hud_enabled = true;
    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Alpha":
                current_chapter.text = "Chapter 1 : Awakening";
                break;
            case "Beta":
                current_chapter.text = "Chapter 2 : Sickness";
                break;
            case "Gamma":
                current_chapter.text = "Chapter 3 : Lapide Luteo";
                break;
            case "Delta":
                current_chapter.text = "Chapter 4 : Error 404";
                break;
            default:
                current_chapter.text = "";
                break;
        }
    }

    void FixedUpdate()
    {
        if (hud_enabled && Input.GetKeyDown(KeyCode.F1))
        {
            gameObject.transform.parent.GetComponent<Canvas>().enabled = false;
            hud_enabled = false;
        }
        else if (!hud_enabled && Input.GetKeyDown(KeyCode.F1))
        {
            gameObject.transform.parent.GetComponent<Canvas>().enabled = true;
            hud_enabled = true;
        }
        if (current_chapter.color.a < 0)
            return;
        if (elapsed < 5f)
            elapsed += Time.fixedDeltaTime;
        else
            current_chapter.color = new Color(current_chapter.color.r, current_chapter.color.g, current_chapter.color.b, current_chapter.color.a - speed);
        

    }
}
