using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ItemRespawnController : IItem
{

    [SerializeField]
    GameObject itemPrefab;

    ItemInventoryController itemInv;

	// Use this for initialization
	void Start () 
    {
        itemInv = this.GetComponent<ItemInventoryController>();

        RespawnItem( ItemInventoryController.ITEM.LIGHT);
	}
	
    void RespawnItem( ItemInventoryController.ITEM itype)
    {
        GameObject item = LoadItem(itype);
        item.name = itype.ToString();

        ItemController itemCtr = item.AddComponent<ItemController>();
		itemCtr.SetItem(itype);

        itemCtr.nowGet
               .Where( d => d == true)
               .Subscribe( _=> 
        {
            Destroy(item);
            itemInv.AddItem(itype);
        });

    }
}
