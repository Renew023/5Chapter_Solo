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
	[Header("������ ����")]
	public string itemName;
	public string itemDescription;
	public ItemType type;
	public Sprite icon;
	public GameObject dropPrefab;

	[Header("�ִ� ������")]
	public bool canStack;
	public int maxStackAmount;

	[Header("������ ��� ��")]
	public OnceUse[] onceUse;

	[Header("���")]
	public GameObject equipPrefab;
}
