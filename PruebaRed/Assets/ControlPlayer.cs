using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public Player info;
    private GameObject _youPlayer;
    public bool isControll;
    // Start is called before the first frame update
    void Awake()
    {
        isControll = false;
        SetControll();
    }

    public void SetControll()
    {
        _youPlayer = GameObject.Find(info.alias);
        _youPlayer.GetComponent<PlayerController>().isControllet = true;
    }
}
