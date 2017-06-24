using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ItemChangeController : MonoBehaviour 
{
    ItemInventoryController itemInv;
    ReactiveProperty<ItemInventoryController.ITEM> nowItem
                                            = new ReactiveProperty<ItemInventoryController.ITEM>(ItemInventoryController.ITEM.NONE);
    List<GameObject> itemPrefabs = new List<GameObject>();

	// Use this for initialization
	void Start () 
    {
        itemInv = GameObject.FindWithTag("GameController").GetComponent<ItemInventoryController>();

        // アイテムプレファブのロード
        for (int i = 0; i < itemInv.itemLength; i++)
        {
            string path = "Items/" + (ItemInventoryController.ITEM)i;
            GameObject item = Instantiate(Resources.Load(path)) as GameObject;
            itemPrefabs.Add( item);

            nowItem.Where( x => i == (int)x)
                   .Subscribe( _=> item.SetActive(true));
			nowItem.Where(x => i != (int)x)
                   .Subscribe(_ => item.SetActive(false));
        }
            
	}

    void Update()
    {
        if( Input.mouseScrollDelta.y > 0)
        {
            nowItem.Value = itemInv.GetNextItem(nowItem.Value);
            Debug.Log(nowItem.ToString());
        }
    }
}
