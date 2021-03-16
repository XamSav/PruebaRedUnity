using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Player _info;
    private float speed = 2f;
    [ReadOnly]
    public bool isControllet = false;
    public bool inGround = false;
    private Rigidbody _rb;
    [SerializeField]
    private float _vel, _force = 5;
    private string _alias;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _info = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
        _alias = _info.GetAlias();
    }
    private void Update()
    {
        if (isControllet)
        {
            Move();
            Jump();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = !Cursor.visible;
            }
        }
    }
    private void LateUpdate()
    {
        if (isControllet)
        {        
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.Rotate(Vector3.up * speed);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.Rotate(Vector3.up * -speed);
            }
        }
    }
    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (v != 0)
        {
            _rb.AddRelativeForce(Vector3.forward * v *_vel * Time.deltaTime);
        }if (h != 0)
        {
            _rb.AddRelativeForce(Vector3.right * h *_vel * Time.deltaTime);
        }
        if (_rb.velocity.x != 0 || _rb.velocity.y != 0 || _rb.velocity.z != 0)
        {
            ClientIO._cl.NewPosition(transform.position, transform.localEulerAngles.y, _alias);
        }
    }
    private void Jump()
    {
        if (inGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inGround = false;
                _rb.AddForce(Vector3.up * _force);
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        inGround = true;
    }

}
