using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	public GameObject curEquip;
	public Transform equipTarget;

	void Awake()
	{
		
	}

	public void EquipNew(ItemData data)
	{
		UnEquip();
		curEquip = Instantiate(data.equipPrefab, equipTarget);
	}

	public void UnEquip()
	{
		if (curEquip != null)
		{
			Destroy(curEquip.gameObject);
			curEquip = null;
		}
	}
}
