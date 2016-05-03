using UnityEngine;
using UnityEngine.Networking;

public class CharacterAnimationScript : NetworkBehaviour
{
    public float Vertical;
    public float Horizontal;
    Animator Anim;
    CharacterController Controller;
    int HorizontalHash = Animator.StringToHash("Horizontal");
    int VerticalHash = Animator.StringToHash("Vertical");
    int SprintHash = Animator.StringToHash("Sprint");
    int WalkHash = Animator.StringToHash("Walk");
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
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        if (Horizontal!=0 || Vertical!=0)
            Anim.SetBool(WalkHash, true);
        else
            Anim.SetBool(WalkHash, false);
        Anim.SetFloat(VerticalHash, Vertical);
        Anim.SetFloat(HorizontalHash, Horizontal);
        if (Input.GetKey(KeyCode.LeftShift) && Vertical == 1.0)
            Anim.SetBool(SprintHash, true);
        else
            Anim.SetBool(SprintHash, false);
    }
}

