using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    #region GENERAL
    private CharacterController _cc;
    private Player _info;
    public bool isControllet = false;
    private string _alias;
    [Range(0, 5)]
    public float SensibilidadRaton = 15f;
    public bool isPaused;
    #endregion
    #region Jump
    public bool isGrounded;
    [SerializeField]
    private float _gravity = -9.81f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 5f;
    [SerializeField]
    private float _force = 5;
    
    #endregion
    #region Move
    [SerializeField]
    private float _vel = 12f;
    private Vector3 velocity;
    #endregion
    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _info = GetComponent<Player>();
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
                GameManager.gm.ResumePause();
            }
        }
    }
    private void LateUpdate()
    {
        if (isControllet)
        {
            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }
            if (Input.GetAxis("Mouse X") > 0 && !isPaused)
            {
                transform.Rotate(Vector3.up * SensibilidadRaton);
            }
            if (Input.GetAxis("Mouse X") < 0 && !isPaused)
            {
                transform.Rotate(Vector3.up * -SensibilidadRaton);
            }
        }
    }
    private void Move()
    {
        if (!isPaused)
        {
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");
            Vector3 move = transform.right * h + transform.forward * v;
            _cc.Move(move * _vel * Time.deltaTime);
        }
        ClientIO._cl.NewPosition(transform.position, transform.localEulerAngles.y, _alias);
    }
    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (Input.GetButtonDown("Jump") && isGrounded && !isPaused)
        {
            velocity.y = Mathf.Sqrt(_force * -2f * _gravity);
        }
        velocity.y += _gravity * Time.deltaTime;

        _cc.Move(velocity * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
    }

}
