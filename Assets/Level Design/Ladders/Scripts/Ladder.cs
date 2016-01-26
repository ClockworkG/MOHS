using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour {

    private GameObject player;
    private bool in_ladder = false;
    private float delta_move;
    public float speed = 1.0f;

    void Update()
    {
        if (in_ladder)
        {
            delta_move = speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.Z))
                player.transform.Translate(0, delta_move, 0);
            if (Input.GetKey(KeyCode.S))
                player.transform.Translate(0, -delta_move, 0);
            if (Input.GetKey(KeyCode.D))
                player.transform.Translate(delta_move, 0, 0);
            if (Input.GetKey(KeyCode.Q))
                player.transform.Translate(-delta_move, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        in_ladder = true;
        player = other.gameObject;
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        in_ladder = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        player = null;
    }
}
