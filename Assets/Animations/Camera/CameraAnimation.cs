using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraAnimation : MonoBehaviour {
    public Text completion;
    float total_completion;
    Animator Anim;
    int PlayHash = Animator.StringToHash("Play");
    int SettingsHash = Animator.StringToHash("Settings");
    int QuitHash = Animator.StringToHash("Quit");
    int BonusHash = Animator.StringToHash("Bonus");
    // Use this for initialization
    void Start () {
        
        Anim = GetComponent<Animator>();
    }
	public void toSettings()
    {
        Anim.SetBool(SettingsHash, true);
    }

    public void toBonus()
    {
        Anim.SetBool(BonusHash, true);
    }

    public void toQuit()
    {
        Anim.SetBool(QuitHash, true);
    }

	public void toPlay()
    {
        Anim.SetBool(PlayHash, true);
    }

    public void toMain()
    {
        //Anim.SetBool(PlayHash, false);
        Anim.SetBool(SettingsHash, false);
        Anim.SetBool(BonusHash, false);
        Anim.SetBool(QuitHash, false);
    }
}
