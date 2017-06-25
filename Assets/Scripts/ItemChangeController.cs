using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ItemChangeController : MonoBehaviour
{
    ItemInventoryController itemInv;
    ReactiveProperty<ItemInventoryController.ITEM> nowItem
                                            = new ReactiveProperty<ItemInventoryController.ITEM>(ItemInventoryController.ITEM.NONE);
    ItemInventoryController.ITEM backItem = ItemInventoryController.ITEM.NONE;
    List<GameObject> itemPrefabs = new List<GameObject>();

    [SerializeField]
    Transform itemHand;

	// Use this for initialization
	void Start () 
    {
        itemInv = GameObject.FindWithTag("GameController").GetComponent<ItemInventoryController>();

        // アイテムプレファブのロード
        for (int i = 0; i < itemInv.itemLength; i++)
        {
            string path = "Items/" + (ItemInventoryController.ITEM)i;
            GameObject item = Instantiate(Resources.Load(path)) as GameObject;
            itemPrefabs.Add(item);

            item.SetActive(false);

            this.UpdateAsObservable()
                .Subscribe(_ => item.transform.SetPositionAndRotation( itemHand.position, this.transform.rotation))
                .AddTo(this.gameObject);
        }

		nowItem.Subscribe(x =>
        {
            itemPrefabs[(int)x].SetActive(true);
	        itemPrefabs[(int)backItem].SetActive(false);
        });
	}

    void Update()
    {
        if( Input.mouseScrollDelta.y > 0)
        {
            backItem = nowItem.Value;
            nowItem.Value = itemInv.GetNextItem(nowItem.Value);
            Debug.Log(nowItem.ToString());
        }
    }
}
