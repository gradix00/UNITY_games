using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slot_item : MonoBehaviour
{
    [Header("Properties slot")]
    public item_database itemDB;
    public bool isEmpty = true, droping, interactItem = false, passOn = false;
    public slot_item_inventory passOnSlot = null;
    public TMP_Text numbT;
    public Sprite none;
    public Image iconSlot, dropBar;
    public string nameThing = null, typeItem = null;
    public int numberOfThing = 0;
    public int damage, calories, addHealth, collectWood, collectStone, collectMetal;

    void Update()
    {
        if (droping && !isEmpty)
        {
            if (dropBar.fillAmount < 1) dropBar.fillAmount += Time.deltaTime;
            else
            {
                dropBar.fillAmount = 0;
                dropItem();
            }
        }
        else dropBar.fillAmount = 0;
    }

    public void isDropingUp() => droping = false;
    public void isDropingDown() => droping = true;

    public void dropItem()
    {
        for (int i = 0; i < itemDB.nameItem.Length; i++)
        {
            if (nameThing == itemDB.nameItem[i])
            {
                var player = GameObject.Find("player_id");
                itemDB.spawnObject(player.transform.position, player.transform.rotation, itemDB.nameItem[i], numberOfThing);

                iconSlot.sprite = none;
                nameThing = null;
                typeItem = null;
                numberOfThing = 0;
                damage = 0;
                calories = 0;
                addHealth = 0;
                collectWood = 0;
                collectStone = 0;
                collectMetal = 0;
                interactItem = false;
                numbT.text = numberOfThing.ToString();
                isEmpty = true;

                //pass on??
                if (passOn)
                {
                    passOnSlot.GetComponent<slot_item_inventory>().isEmpty = isEmpty;
                    passOnSlot.GetComponent<slot_item_inventory>().iconSlot.sprite = iconSlot.sprite;
                    passOnSlot.GetComponent<slot_item_inventory>().nameThing = nameThing;
                    passOnSlot.GetComponent<slot_item_inventory>().typeItem = typeItem;
                    passOnSlot.GetComponent<slot_item_inventory>().numberOfThing = numberOfThing;
                    passOnSlot.GetComponent<slot_item_inventory>().damage = damage;
                    passOnSlot.GetComponent<slot_item_inventory>().calories = calories;
                    passOnSlot.GetComponent<slot_item_inventory>().addHealth = addHealth;
                    passOnSlot.GetComponent<slot_item_inventory>().collectWood = collectWood;
                    passOnSlot.GetComponent<slot_item_inventory>().collectStone = collectStone;
                    passOnSlot.GetComponent<slot_item_inventory>().collectMetal = collectMetal;
                    passOnSlot.GetComponent<slot_item_inventory>().interactItem = interactItem;
                    passOnSlot.GetComponent<slot_item_inventory>().numbT.text = numberOfThing.ToString();
                }
            }
        }
    }

    public void selectItem()
    {
        var anim = this.gameObject.GetComponent<Animator>();
        anim.Play("slotItem_selected");

        if (interactItem)
        {

        }
    }

    public void addItemToSlot(item object_ = null, slot_item_inventory slotInventory = null)
    {
        //play anim
        var anim = this.gameObject.GetComponent<Animator>();
        anim.Play("slotItem_selected");

        //var player = GameObject.Find("player_id");
        isEmpty = false;

        if (object_ != null)
        {
            iconSlot.sprite = object_.iconItem;
            nameThing = object_.nameItem;
            typeItem = object_.typeOfItem;
            numberOfThing += object_.numberOfItem;
            damage = object_.damage;
            calories = object_.calories;
            addHealth = object_.addHealth;
            collectWood = object_.collectWood;
            collectStone = object_.collectStone;
            collectMetal = object_.collectMetal;
            interactItem = object_.interactItem;
        }
        else
        {
            Debug.Log(slotInventory);
            iconSlot.sprite = slotInventory.iconSlot.sprite;
            nameThing = slotInventory.nameThing;
            typeItem = slotInventory.typeItem;
            numberOfThing += slotInventory.numberOfThing;
            damage = slotInventory.damage;
            calories = slotInventory.calories;
            addHealth = slotInventory.addHealth;
            collectWood = slotInventory.collectWood;
            collectStone = slotInventory.collectStone;
            collectMetal = slotInventory.collectMetal;
            interactItem = slotInventory.interactItem;            
        }
        numbT.text = numberOfThing.ToString();

        //pass on??
        if (passOn)
        {
            passOnSlot.GetComponent<slot_item_inventory>().isEmpty = isEmpty;
            passOnSlot.GetComponent<slot_item_inventory>().iconSlot.sprite = iconSlot.sprite;
            passOnSlot.GetComponent<slot_item_inventory>().nameThing = nameThing;
            passOnSlot.GetComponent<slot_item_inventory>().typeItem = typeItem;
            passOnSlot.GetComponent<slot_item_inventory>().numberOfThing = numberOfThing;
            passOnSlot.GetComponent<slot_item_inventory>().damage = damage;
            passOnSlot.GetComponent<slot_item_inventory>().calories = calories;
            passOnSlot.GetComponent<slot_item_inventory>().addHealth = addHealth;
            passOnSlot.GetComponent<slot_item_inventory>().collectWood = collectWood;
            passOnSlot.GetComponent<slot_item_inventory>().collectStone = collectStone;
            passOnSlot.GetComponent<slot_item_inventory>().collectMetal = collectMetal;
            passOnSlot.GetComponent<slot_item_inventory>().interactItem = interactItem;
            passOnSlot.GetComponent<slot_item_inventory>().numbT.text = numberOfThing.ToString();
        }

        object_.destroyItem();
        //player.GetComponent<control>().slotsInPlayerRefresh();
    }
}
