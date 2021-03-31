using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class LanzaScript : MonoBehaviour
{
    private GameObject _Padre;
    [SerializeField]
    private Vector3 Crecimiento = new Vector3(0, 1, 0);
    [SerializeField]
    private Vector3 Movimiento = new Vector3(0, 0, 0.5f);
    private void Start()
    {
        //PlayerController _pc = GetComponentInParent<PlayerController>();
        //if (_pc.isControllet)
        //{
            _Padre = this.gameObject.transform.parent.gameObject;
        //}
    }
    private void OnCollisionEnter(Collision col)
    {
        string alias = col.gameObject.name;
        if(col.gameObject.tag == "Player" && alias != _Padre.name)
        {
            ClientIO._cl.Death(alias);
            this.transform.localScale += Crecimiento;
            this.transform.localPosition += Movimiento;
        }
    }
}
