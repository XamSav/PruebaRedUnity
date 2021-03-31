using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMultiplayer : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
            PlayerController _pc = GetComponentInParent<PlayerController>();
            if (!_pc.isControllet)
            {
                this.gameObject.SetActive(false);
            }
    }
}
