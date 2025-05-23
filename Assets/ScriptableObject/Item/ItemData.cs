using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Equip,
	OnceUse,
	Resource,
	Interactable
}

public enum OnceUseType
{
	Health,
	Hunger,
	Stamina,
	MoveSpeed,
	JumpPower
}

[Serializable]
public class OnceUse
{
	public OnceUseType type;
	public float value;

	public bool isTimer;
	public float time;
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

	[Header("아이템 사용 시")]
	public OnceUse[] onceUse;

	[Header("장비")]
	public GameObject equipPrefab;
}
