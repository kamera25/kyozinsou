using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IItem : MonoBehaviour 
{

    protected GameObject LoadItem( ItemInventoryController.ITEM i)
    {
        string path = "Items/" + i.ToString();
        return Instantiate(Resources.Load(path)) as GameObject;
    }

}
