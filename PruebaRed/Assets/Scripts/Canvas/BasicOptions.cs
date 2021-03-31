using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicOptions : MonoBehaviour
{
    [SerializeField]
    [Range(-3, 3)]
    private float CameraPosition = 0f;
    public Scrollbar Scroll;
    private GameObject camara;
    public void setCameraPosition(Scrollbar s)
    {
        camara = GameManager.gm.getMyPlayer().transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        camara.transform.localPosition = new Vector3(s.value * 4, camara.transform.localPosition.y, camara.transform.localPosition.z);
    }
    public void PauseResume()
    {
        GameManager.gm.ResumePause();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
