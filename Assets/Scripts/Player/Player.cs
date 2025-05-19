using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerKey playerkey;
    public Status status;

    public ItemData itemData;

    void Start()
    {
        EventItem.addItem += GetItem;
    }

    public void GetItem(ItemData item)
    {
        itemData = item;
    }
}
