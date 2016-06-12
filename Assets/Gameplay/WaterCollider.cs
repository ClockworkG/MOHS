using UnityEngine;
using System.Collections;

public class WaterCollider : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flare")
            Destroy(other.transform.parent.gameObject);
        else if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<JetPack>().enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<JetPack>().enabled = true;
    }

}
