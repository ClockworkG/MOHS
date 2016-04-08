using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {
    public GameObject first_floor;
    public GameObject underground;
    public HorizontalAnim door;

    void OnTriggerEnter(Collider other)
    {
        first_floor.SetActive(true);
        underground.SetActive(false);
        door.locked = true;
    }
}
