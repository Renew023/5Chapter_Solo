using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKey : MonoBehaviour
{
    [Header("Move")]
    [SerializeField]
    private float moveSpeed;
    private Vector2 curMoveInput;

    [Header("Look")]
	[SerializeField]
	private Transform camera;
	[SerializeField]
	private float minXLook;
	[SerializeField]
	private float maxXLook;
    private float camCurX;
	[SerializeField]
	private float lookSensitivity;
    private Vector2 mouseDelta;

    [Header("Jump")]
	[SerializeField]
	private float jumpPower;

    [Header("Character")]
    [SerializeField]
    private Rigidbody rb;

    // Update is called once per frame

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EventZone.JumpZone += JumpZone;
    }
    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        Look();
    }

    public void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y+ transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;
    }

    public void Look()
    {
        camCurX += mouseDelta.y * lookSensitivity;
        camCurX = Mathf.Clamp(camCurX, minXLook, maxXLook);
        camera.localEulerAngles = new Vector3(-camCurX, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (EventPlayer.jump())
        {
            if (context.phase == InputActionPhase.Started)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    public void OnInterect(InputAction.CallbackContext context)
    {

    }

    public void JumpZone()
    {
        rb.AddForce(Vector2.up * jumpPower * 3, ForceMode.Impulse);
    }
}
