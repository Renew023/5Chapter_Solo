using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform slotPanel;

    [Header("���� ������ ����")]
    public Transform dropPosition;

    [Header("���õ� ������")]
    [SerializeField] private TextMeshProUGUI selectedItemName;
    [SerializeField] private TextMeshProUGUI selectedItemDescription;
    [SerializeField] private TextMeshProUGUI selectedStatName;
    [SerializeField] private TextMeshProUGUI selectedStatValue;
    [SerializeField] private Button useButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unEquipButton;
    [SerializeField] private Button dropButton;

    [Header("���õ� ������ ����")]
    [SerializeField] private ItemData selectedItem;
    [SerializeField] private int selectedItemIndex = 0;

    [Header("������ ������ ����")]
    [SerializeField] private int curEquipIndex = 0;

    [Header("������ �ְ� �ޱ�")]
    public Player player;

    void Start()
    {
        slots = new ItemSlot[slotPanel.childCount];
        EventItem.addItem += AddItem;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        useButton.onClick.AddListener(OnUseButton);
        equipButton.onClick.AddListener(OnEquipButton);
        unEquipButton.onClick.AddListener(OnUnEquipButton);
        dropButton.onClick.AddListener(OnDropButton);

		gameObject.SetActive(false);
        ClearSelectedItemWindow();
    }
    public void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        unEquipButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    public void AddItem(ItemData data)
    {
        if (data.canStack)
        {
			ItemSlot slot = GetItemStack(data);
			if (slot != null)
			{
				slot.stack++;
				UpdateItemUI();
                data = null;
				return;
			}
		}
		ItemSlot emptySlot = GetEmptySlot(); //�� ���� �ּ�
		// �ִٸ� 
		if (emptySlot != null) //������ ����ִٸ�
		{
			emptySlot.item = data;  //������ ���� �ְ�
			emptySlot.stack = 1; //���� �־��ְ�
			UpdateItemUI(); //������ �����ְ�
            data = null; //������ �������� null
			return;
		}

		ThrowItem(data);
        data = null;
	}
    //Toggle, isOpen ����
	public void UpdateItemUI()
    {
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item != null)
			{
				slots[i].Set();
			}
			else
			{
				slots[i].Clear();
			}
		}
	}

    public ItemSlot GetItemStack(ItemData data)
	{
		for (int i = 0; i < slots.Length; i++) // ������ ���鼭 ������ ���Ѵ�.
		{
			if (slots[i].item == data && slots[i].stack < data.maxStackAmount)
			{
				return slots[i];
			}
		}
		return null;
	}

	public ItemSlot GetEmptySlot() //�� ���� �����´�.
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
			{
				return slots[i];
			}
		}

		return null;
	}

    private void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.Range(0f, 360f)));
    }

    #region ������ ����
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
        
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;
        slots[index].outline.enabled = true;

        for (int i = 0; i < selectedItem.onceUse.Length; i++)
        {
            selectedStatName.text += selectedItem.onceUse[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.onceUse[i].value.ToString() + "\n";
        }
        ShowButton();
	}
    public void PrevSelectItem()
    {
        slots[selectedItemIndex].outline.enabled = false;
    }

    public void ShowButton()

    {
		useButton.gameObject.SetActive(selectedItem.type == ItemType.OnceUse);
		equipButton.gameObject.SetActive(selectedItem.type == ItemType.Equip && !slots[selectedItemIndex].equipped);
		unEquipButton.gameObject.SetActive(selectedItem.type == ItemType.Equip && slots[selectedItemIndex].equipped);
		dropButton.gameObject.SetActive(true);
	}
    #endregion

    public void OnUseButton()
    {
        player.UseItem(selectedItem);
		RemoveSelectedItem();
	}

    public void OnDropButton()
    {
        UnEquip(selectedItemIndex);
        ThrowItem(selectedItem);
        RemoveSelectedItem();
    }

	void RemoveSelectedItem()
	{
		slots[selectedItemIndex].stack--;

		if (slots[selectedItemIndex].stack <= 0)
		{
			selectedItem = null;
			slots[selectedItemIndex].item = null;
			selectedItemIndex = -1;
			ClearSelectedItemWindow();
		}
		UpdateItemUI();
	}

    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;
		slots[selectedItemIndex].outline.enabled = true;
		curEquipIndex = selectedItemIndex;
        player.equip.EquipNew(selectedItem);
        UpdateItemUI();

        SelectItem(selectedItemIndex); // region ������ ����
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        slots[index].outline.enabled = false;
        player.equip.UnEquip();
        UpdateItemUI();
        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
	}
}
