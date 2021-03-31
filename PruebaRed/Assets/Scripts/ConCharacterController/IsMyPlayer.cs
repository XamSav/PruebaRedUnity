using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMyPlayer : MonoBehaviour
{
    private bool isControllet = false;

    public bool getIsControllet()
    {
        return isControllet;
    }
    public void setIsController(bool s)
    {
        isControllet = s;
    }
}
