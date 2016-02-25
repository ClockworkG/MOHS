using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Quest : MonoBehaviour {
    public Text item_name;
    public Text item_desc;
    public Image item_img;
    public Interact item_list;
    private int index = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateName();
	}

    public void Next()
    {
        int t = item_list.quest.Count;
        if (t > 0)
        {
            index = (index + 1) % t;
            UpdateName();
        }
        else
            item_name.text = "No item";
    }

    public void Prev()
    {
        int t = item_list.quest.Count;

        if (t > 0)
        {
            index = ((index - 1) % t);
            UpdateName();
        }
        else
            item_name.text = "No item";
    }

    public void UpdateName()
    {
        item_name.text = item_list.quest[index].item_name;
        item_img.sprite = item_list.quest[index].item_sprite;
        item_desc.text = item_list.quest[index].description;
    }
}
