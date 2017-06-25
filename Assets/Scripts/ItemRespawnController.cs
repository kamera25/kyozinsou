using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ItemRespawnController : IItem
{

    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    int itemNum = 4;

    ItemInventoryController itemInv;

	// Use this for initialization
	void Start () 
    {
        itemInv = this.GetComponent<ItemInventoryController>();

        List<Transform> respawnPoss = new List<Transform>();
        respawnPoss = GameObject.FindGameObjectsWithTag("RespawnPoint")
                                .Select(s => s.transform)
                                .ToList();

        for (int i = 0; i < itemNum; i++)
        {
            GameObject item = RespawnItem((ItemInventoryController.ITEM)i);
            item.transform.position = respawnPoss[Random.Range(0, respawnPoss.Count)].position;
        }

	}
	
    GameObject RespawnItem( ItemInventoryController.ITEM itype)
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

        return item;
    }
}
