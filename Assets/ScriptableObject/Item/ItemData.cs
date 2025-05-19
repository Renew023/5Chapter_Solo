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
	[Header("아이템 정보")]
	public string itemName;
	public string itemDescription;
	public ItemType type;
	public Sprite icon;
	public GameObject dropPrefab;

	[Header("최대 보유량")]
	public bool canStack;
	public int maxStackAmount;

	[Header("회복량")]
	public HealUse[] healUse;

	[Header("장비")]
	public GameObject equipPrefab;
}
