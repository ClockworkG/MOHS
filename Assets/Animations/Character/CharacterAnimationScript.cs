using UnityEngine;
using UnityEngine.Networking;

public class CharacterAnimationScript : NetworkBehaviour
{
    public float Speed;
    Animator Anim;
    CharacterController Controller;
    int SpeedHash = Animator.StringToHash("Speed");
    int SprintHash = Animator.StringToHash("Sprint");
    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        Speed = Input.GetAxis("Vertical");
        Anim.SetFloat(SpeedHash, Speed);
        if (Input.GetKey(KeyCode.LeftShift) && Speed == 1.0)
            Anim.SetBool(SprintHash, true);
        else
            Anim.SetBool(SprintHash, false);
    }
}

