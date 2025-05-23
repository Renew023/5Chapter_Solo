using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
	public Inventory inventory;

    [Header("구성 요소")]
	[SerializeField] ItemType type;
	[SerializeField] Image icon;
	[SerializeField] private TextMeshProUGUI stackText;
	public Outline outline;

	[Header("아이템 칸 정보")]
	public int index;
	public int stack;
	public bool equipped;
	public bool selected;

	private void Awake()
	{
		outline = GetComponent<Outline>();
	}

	private void Start()
	{
		outline.enabled = equipped || selected;
	}

	public void Set()
	{
		icon.gameObject.SetActive(true);
		icon.sprite = item.icon;
		stackText.text = stack > 1 ? stack.ToString() : string.Empty;
	}

	public void Clear()
	{
		icon.gameObject.SetActive(false);
		stackText.text = string.Empty;
	}

	public void OnClickButton()
	{
		inventory.PrevSelectItem();
		inventory.SelectItem(index);
	}
}
