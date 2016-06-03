using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    public Text current_chapter;
    public float speed;
    public float elapsed;
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
            default:
                current_chapter.text = "";
                break;
        }
    }

    void FixedUpdate()
    {
        if (current_chapter.color.a < 0)
            this.enabled = false;
        if (elapsed < 5f)
            elapsed += Time.fixedDeltaTime;
        else
            current_chapter.color = new Color(current_chapter.color.r, current_chapter.color.g, current_chapter.color.b, current_chapter.color.a - speed);
        

    }
}
