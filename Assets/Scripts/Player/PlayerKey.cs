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

    [Header("LookChange")]
    private bool isOne = true;
	[SerializeField]
	private float threeMinXLook;
	[SerializeField]
	private float threeMaxXLook;
	[SerializeField]
	private float oneMinXLook;
	[SerializeField]
	private float oneMaxXLook;
    [SerializeField]
    private GameObject midPoint; 

	[Header("Jump")]
	[SerializeField]
	private float jumpPower;
    private bool isGround;
	[SerializeField]
	private LayerMask groundLayerMask;

	[Header("Grap")]
    [SerializeField]
    private float maxGrapDistance;
    [SerializeField]
    private LayerMask grapLayerMask;
	private bool isGrap;

	[Header("Character")]
    [SerializeField]
    private Rigidbody rb;
    public bool isControllStop = false;

    // Update is called once per frame

    void Awake()
    {
        minXLook = oneMinXLook;
        maxXLook = oneMaxXLook;
    }

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
        if(isControllStop)
			if (rb.velocity.y <= -0.01f)
				isControllStop = false;
            else
                return;

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
        if (context.phase == InputActionPhase.Started)
        {
            if (isGrap)
            {
                rb.useGravity = true;
                isGrap = false;
            }
            else
            {
                Ray ray = new Ray(transform.position+Vector3.up, Vector3.down);
                Debug.Log("레이는 쐈습니다?");

                if (Physics.Raycast(ray, 2f, groundLayerMask))
                {
                    Debug.Log("오케이 바닥이네");
                    isGround = true;
                }
                else
                {
                    isGround = false;
                }
            }
            if (isGround)
            {
                if (EventPlayer.jump())
                {
                    rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                    isGround = false;
                }
            }
        }
    }

    public void OnLookChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (isOne)
            {
                Debug.Log("시야 변경");
                camera.localPosition = new Vector3(0.2f, 1.8f, -1.8f);
                minXLook = threeMinXLook;
                maxXLook = threeMaxXLook;
                lookSensitivity = 0.05f;
                isOne = false;
                Cursor.lockState = CursorLockMode.None;
                midPoint.SetActive(isOne);
            }
            else
            {
                Debug.Log("시야 변경");
                camera.localPosition = new Vector3(0, 2, 0);
                minXLook = oneMinXLook;
                maxXLook = oneMaxXLook;
                lookSensitivity = 0.2f;
                isOne = true;
				Cursor.lockState = CursorLockMode.Locked;
                midPoint.SetActive(isOne);
			}
        }
    }

    public void OnGrap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // 마우스 좌표에서 일직선으로 나가는 레이를 쏴라.
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxGrapDistance, grapLayerMask)) //레이를 맞추고 out hit=> hit 값을 받아온다.
            {
                Debug.Log("벽을 잡았습니다.");
                rb.velocity = Vector2.zero;
                rb.useGravity = false;
                isGround = true;
                isGrap = true;
            }
        }
    }

    public void JumpZone()
    {
        rb.AddForce(Vector2.up * jumpPower * 3, ForceMode.Impulse);
    }
}
