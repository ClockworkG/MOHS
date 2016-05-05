using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NeonScript : MonoBehaviour {
    public Material unlit;
    public Material lit;
    public Light pointLight;
    public MeshRenderer tube;
    void FixedUpdate()
    {
        if (pointLight.enabled)
            tube.material = lit;
        else
            tube.material = unlit;
    }
}
