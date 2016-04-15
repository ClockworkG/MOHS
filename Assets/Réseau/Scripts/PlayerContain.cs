using UnityEngine;
using System.Collections;

public class PlayerContain : MonoBehaviour {
    public GameObject player_obj;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
