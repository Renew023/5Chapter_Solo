using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInterect();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.itemName} \n {data.itemDescription}";
        return str;
    }

	public void OnInterect()
    {
        EventItem.addItem(data);
        Debug.Log("æ∆¿Ã≈€ »πµÊ π◊ ªË¡¶!");
        Destroy(this.gameObject);
    }
}
