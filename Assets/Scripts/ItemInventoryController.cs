using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemInventoryController : MonoBehaviour 
{
    public enum ITEM
    {
        NONE = 0,
        LIGHT,
        WATCH,
        RADIO
    }

    Dictionary<ITEM, int> inventory = new Dictionary<ITEM, int>();
    public int itemLength{
        get{
            return Enum.GetNames(typeof(ITEM)).Length;
        }
    }

	// Use this for initialization
	void Start () 
    {

        inventory.Add( ITEM.NONE, 1);
		inventory.Add( ITEM.LIGHT, 0);	
        inventory.Add( ITEM.RADIO, 0);
        inventory.Add( ITEM.WATCH, 0);
	}

    public ITEM GetNextItem( ITEM nowElement)
	{
        for (int i = 1; i < itemLength; i++)
        {
            int index = (i + (int)nowElement) % itemLength;
            ITEM itype = (ITEM)index;

            if( inventory[itype] > 0)
            {
                Debug.Log((int)itype);
                return itype;
            }
        }

        return ITEM.NONE;
	}

    public void AddItem( ITEM item)
    {
        inventory[item] += 1;
        Debug.Log("Add");
    }

	void RemoveItem(ITEM item)
	{
        if( inventory[item] <= 0)
        {
            Debug.LogError("ItemInventoryController : " + item.ToString() + " is 0.");
            return;
        }
		inventory[item] -= 1;
	}
}
