using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRay : MonoBehaviour
{
    [Header("Ȯ���ϴ� ����")]
    public float maxCheckDistance;
    private Camera camera;
    [SerializeField]
    private  LayerMask layerMask;

    [Header("������Ʈ ����")]
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    public TextMeshProUGUI promptText;

    void Start()
    {
        camera = Camera.main;
    }

    void FixedUpdate()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2)); // ���콺 ��ǥ���� ���������� ������ ���̸� ����.
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) //���̸� ���߰� out hit=> hit ���� �޾ƿ´�.
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
            Debug.Log("�׽�Ʈ");
            curInteractable.OnInterect();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    
}
