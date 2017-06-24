using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ItemRespawnController : MonoBehaviour 
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
        GameObject item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        item.name = itype.ToString();

        ItemController itemCtr = item.GetComponent<ItemController>();
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
