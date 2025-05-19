using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Equip,
	OnceUse,
	Resource
}

public enum OnceUseType
{
	Health,
	Hunger,
	Stamina
}

[Serializable]
public class HealUse
{
	public OnceUseType type;
	public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
	[Header("������ ����")]
	public string itemName;
	public string itemDescription;
	public ItemType type;
	public Sprite icon;
	public GameObject dropPrefab;

	[Header("�ִ� ������")]
	public bool canStack;
	public int maxStackAmount;

	[Header("ȸ����")]
	public HealUse[] healUse;

	[Header("���")]
	public GameObject equipPrefab;
}
