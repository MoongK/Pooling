using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrollList : MonoBehaviour {

    public List<Item> itemList;
    public Text myGoldDisplay;
    public Transform contentPanel;
    public GameObject buttonPrefab;

    public ShopScrollList otherShop;
    public SimpleObjectPool buttonPool;
    public float gold = 20f;


    void Start () {
        RefreshDisplay();	
	}

    public void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold : " + gold.ToString();
        RemoveButtons();
        AddButtons();
    }

    public void RemoveButtons()
    {
        //Workable 1
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            var c = transform.GetChild(i);
            if (buttonPool != null)
                buttonPool.ReturnObject(c.gameObject);
            else
                Destroy(c.gameObject);
        }

        // Error code
        //foreach (Transform c in transform)
        //{
        //    if(buttonPool != null)
        //        buttonPool.ReturnObject(c.gameObject);
        //    else
        //        Destroy(c.gameObject);
        //}
    }

    void AddButtons()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newButton;

            if (buttonPool != null)
            {
                newButton = buttonPool.GetObject();
                newButton.transform.SetParent(contentPanel);
            }
            else
            {
                newButton = Instantiate(buttonPrefab, contentPanel);
            }
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }

    internal void TryTransferItemToOtherShop(Item item)
    {
        if(otherShop.gold >= item.price)
        {
            gold += item.price;
            otherShop.gold -= item.price;

            AddItem(item, otherShop);
            RemoveItem(item, this);

            RefreshDisplay();
            otherShop.RefreshDisplay();
            print("enough gold");
        }
        else
        {
            print("note enough gold");
        }
    }

    private void RemoveItem(Item _item, ShopScrollList shopScrollList)
    {
        shopScrollList.itemList.Remove(_item);
        //int index = shopScrollList.itemList.FindIndex(item => item == _item);
        //shopScrollList.itemList.RemoveAt(index);
    }

    private void AddItem(Item item, ShopScrollList otherShop)
    {
        otherShop.itemList.Add(item);
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public float price = 1f;
}