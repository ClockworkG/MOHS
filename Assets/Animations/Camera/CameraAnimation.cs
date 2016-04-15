using UnityEngine;
using System.Collections;

public class CameraAnimation : MonoBehaviour {
    Animator Anim;
    int PlayHash = Animator.StringToHash("Play");
    // Use this for initialization
    void Start () {
        Anim = GetComponent<Animator>();
    }
	
	public void You_Play()
    {
        Anim.SetBool(PlayHash, true);
    }
}
