using UnityEngine;
using System.Collections;

public class WinnerListener : Listener {

    public Camera kudzuCamera;
    public GameObject Kudzu;
    public Camera SequoiaCamera;
    public GameObject GoImage;
    bool showtexture = false;

    public override void notify()
    {
        showtexture = true;
        kudzuCamera.GetComponent<KudzuCamera>().enabled = false;
        kudzuCamera.transform.parent = Kudzu.transform;
        Vector3 camloc = Kudzu.transform.position;
        camloc.z = -1f;
        kudzuCamera.transform.position = camloc;
        GoImage.GetComponent<SpriteRenderer>().enabled = true;
    }
}
