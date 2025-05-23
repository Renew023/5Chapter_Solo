using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRay : MonoBehaviour
{
    [Header("확인하는 조건")]
    public float maxCheckDistance;
    private Camera camera;
    [SerializeField]
    private  LayerMask layerMask;

    [Header("오브젝트 정보")]
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    public TextMeshProUGUI promptText;

    void Start()
    {
        camera = Camera.main;
    }

    void FixedUpdate()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2)); // 마우스 좌표에서 일직선으로 나가는 레이를 쏴라.
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) //레이를 맞추고 out hit=> hit 값을 받아온다.
        {
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();
                SetPromptText();
            }
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInterect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            Debug.Log("테스트");
            curInteractable.OnInterect();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    
}
