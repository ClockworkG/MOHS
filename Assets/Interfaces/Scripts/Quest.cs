using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Quest : MonoBehaviour
{
    public Image hud_img;
    public Text item_name;
    public Text item_desc;
    public Image item_img;
    public Image item_prev;
    public Image item_next;
    public Interact item_list;
    public int index = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateName();
        if (Input.GetKeyUp(KeyCode.Tab))
            Next();
    }

    public void Next()
    {
        int t = item_list.quest.Count;
        if (index <= t - 1)
        {
            if (index == t - 1)
                index = 0;
            else
                index++;
            UpdateName();
        }
        else
            item_name.text = "No item";
    }

    public void Prev()
    {
        int t = item_list.quest.Count;

        if (index >= 0)
        {
            if (index == 0)
                index = t - 1;
            else
                index--;
            UpdateName();
        }
        else
            item_name.text = "No item";
    }

    public void UpdateName()
    {
        int t = item_list.quest.Count;
        if (t != 0)
        {
            item_name.text = item_list.quest[index].item_name;
            item_img.sprite = item_list.quest[index].item_sprite;
            hud_img.sprite = item_list.quest[index].item_sprite;
            if (index + 1 >= t)
                item_next.sprite = item_list.quest[0].item_sprite;
            else
                item_next.sprite = item_list.quest[(index + 1)].item_sprite;
            if (index - 1 < 0)
                item_prev.sprite = item_list.quest[t - 1].item_sprite;
            else
                item_prev.sprite = item_list.quest[(index - 1)].item_sprite;
            item_desc.text = item_list.quest[index].description;
        }
    }
}
