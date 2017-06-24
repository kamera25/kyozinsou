using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ItemController : MonoBehaviour 
{

    [SerializeField]
    ItemInventoryController.ITEM item;

    public ReactiveProperty<bool> nowGet = new ReactiveProperty<bool>(false);

    public void SetItem( ItemInventoryController.ITEM i)
    {
        item = i;
    }

    private void OnTriggerStay(Collider other)
    {
        if( Input.GetButton("Jump") && other.CompareTag("Player"))  
        {
            nowGet.Value = true;
        }
    }
}
