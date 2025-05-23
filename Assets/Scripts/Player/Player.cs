using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public PlayerKey playerkey;
    public Status status;
    public Equipment equip;

    public ItemData itemData;

    void Start()
    {
    }

    public void GetItem(ItemData item)
    {
        itemData = item;
    }

    public void UseItem(ItemData item)
    {
        for (int i = 0; i < item.onceUse.Length; i++)
        {
            switch (item.onceUse[i].type)
            {
                case OnceUseType.Health:
                    status.health.Add(item.onceUse[i].value);
                    break;
                case OnceUseType.Hunger:
					status.hunger.Add(item.onceUse[i].value);
					break;
                case OnceUseType.Stamina:
					status.stamina.Add(item.onceUse[i].value);
					break;
                case OnceUseType.JumpPower:
                    playerkey.jumpPower += item.onceUse[i].value;
                    break;
                case OnceUseType.MoveSpeed:
					playerkey.moveSpeed += item.onceUse[i].value;
					break;
            }
            if (item.onceUse[i].isTimer)
            {
                StartCoroutine(ItemTimer(item.onceUse[i].type, item.onceUse[i].time, item.onceUse[i].value));
            }
        }
        
    }

    IEnumerator ItemTimer(OnceUseType type, float timer, float value)
    {
        yield return new WaitForSeconds(timer);
		switch (type)
		{
			case OnceUseType.Health:
				status.health.Sub(value);
				break;
			case OnceUseType.Hunger:
				status.hunger.Sub(value);
				break;
			case OnceUseType.Stamina:
				status.stamina.Sub(value);
				break;
			case OnceUseType.JumpPower:
                playerkey.jumpPower -= value;
				break;
			case OnceUseType.MoveSpeed:
                playerkey.moveSpeed -= value;
				break;
		}
	}
}
