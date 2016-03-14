using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {
    public GameObject first_floor;
    public GameObject underground;
    public HorizontalAnim door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        first_floor.SetActive(true);
        underground.SetActive(false);
        door.locked = true;
    }
}
