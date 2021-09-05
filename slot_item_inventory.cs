using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slot_item_inventory : MonoBehaviour
{
    public GameObject inChest = null;
    [Header("Properties slot")]
    public item_database itemDB;
    public bool isEmpty = true, transfering, interactItem = false, passOn, transferChest, reguireChest = true, moveCloth;
    public TMP_Text numbT;
    public Sprite none;
    public Image iconSlot, transferBar;
    public string nameThing = null, typeItem = null;
    public int numberOfThing = 0;
    public int damage, calories, addHealth, collectWood, collectStone, collectMetal;

    //window interact/ not interact object in action panel
    [SerializeField] GameObject interactPanel, notInteractPanel, simplePanel, dressUp, otherAction;
    [SerializeField] panelItemInfo panelInfo;   

    //pass on other slot in player
    public GameObject otherSlot = null;

    void Update()
    {
        if (inChest != null && reguireChest)
        {
            if (transfering && !isEmpty)
            {
                if (transferBar.fillAmount < 1) transferBar.fillAmount += Time.deltaTime;
                else
                {
                    transferBar.fillAmount = 0;
                    if (!transferChest) transferItem();
                    else transferItemToEquipment();
                }
            }
            else transferBar.fillAmount = 0;
        }
        else
        {
            if (transfering && !isEmpty)
            {
                if (transferBar.fillAmount < 1) transferBar.fillAmount += Time.deltaTime;
                else
                {
                    transferBar.fillAmount = 0;
                    if (!transferChest) transferItem();
                    else transferItemToEquipment();                   
                }
            }
            else transferBar.fillAmount = 0;
        }
    }

    public void isTransferUp() => transfering = false;
    public void isTransferDown() => transfering = true;

    void transferItemToEquipment()
    {
        for (int i = 0; i < itemDB.nameItem.Length; i++)
        {
            if (nameThing == itemDB.nameItem[i])
            {
                var player = GameObject.Find("player_id");
                bool isEmptySlots = player.GetComponent<control>().checkEmptySlot();

                if (isEmptySlots)
                {
                    player.GetComponent<control>().addItemToSlot(nameThing, typeItem, numberOfThing, damage, calories, collectWood, collectStone, collectMetal, addHealth, iconSlot.sprite, interactItem, isEmpty);
                    if(!moveCloth) player.GetComponent<control>().item.GetComponent<chest_inventory>().removeItem(nameThing);
                    else
                    {
                        var cloth = GameObject.Find("system equipment");
                        cloth.GetComponent<system_cloth>().removeCloth(nameThing);
                    }

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
                        otherSlot.GetComponent<slot_item>().isEmpty = isEmpty;
                        otherSlot.GetComponent<slot_item>().iconSlot.sprite = iconSlot.sprite;
                        otherSlot.GetComponent<slot_item>().nameThing = nameThing;
                        otherSlot.GetComponent<slot_item>().typeItem = typeItem;
                        otherSlot.GetComponent<slot_item>().numberOfThing = numberOfThing;
                        otherSlot.GetComponent<slot_item>().damage = damage;
                        otherSlot.GetComponent<slot_item>().calories = calories;
                        otherSlot.GetComponent<slot_item>().addHealth = addHealth;
                        otherSlot.GetComponent<slot_item>().collectWood = collectWood;
                        otherSlot.GetComponent<slot_item>().collectStone = collectStone;
                        otherSlot.GetComponent<slot_item>().collectMetal = collectMetal;
                        otherSlot.GetComponent<slot_item>().interactItem = interactItem;
                        otherSlot.GetComponent<slot_item>().numbT.text = numberOfThing.ToString();
                    }
                }
            }
        }
    }

    void transferItem()
    {
        if (inChest != null)
        {
            for (int i = 0; i < itemDB.nameItem.Length; i++)
            {
                if (nameThing == itemDB.nameItem[i])
                {
                    bool isEmptySlots = inChest.GetComponent<chest_inventory>().checkEmptySlots();

                    if (isEmptySlots)
                    {
                        inChest.GetComponent<chest_inventory>().getInfoItem(nameThing, typeItem, numberOfThing, damage, calories, collectWood, collectStone, collectMetal, addHealth, iconSlot.sprite, interactItem, isEmpty);

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
                            otherSlot.GetComponent<slot_item>().isEmpty = isEmpty;
                            otherSlot.GetComponent<slot_item>().iconSlot.sprite = iconSlot.sprite;
                            otherSlot.GetComponent<slot_item>().nameThing = nameThing;
                            otherSlot.GetComponent<slot_item>().typeItem = typeItem;
                            otherSlot.GetComponent<slot_item>().numberOfThing = numberOfThing;
                            otherSlot.GetComponent<slot_item>().damage = damage;
                            otherSlot.GetComponent<slot_item>().calories = calories;
                            otherSlot.GetComponent<slot_item>().addHealth = addHealth;
                            otherSlot.GetComponent<slot_item>().collectWood = collectWood;
                            otherSlot.GetComponent<slot_item>().collectStone = collectStone;
                            otherSlot.GetComponent<slot_item>().collectMetal = collectMetal;
                            otherSlot.GetComponent<slot_item>().interactItem = interactItem;
                            otherSlot.GetComponent<slot_item>().numbT.text = numberOfThing.ToString();
                        }
                    }
                }
            }
        }
    }

    public void selectItem()
    {
        var anim = this.gameObject.GetComponent<Animator>();
        anim.Play("slotItem_selected");

        if (!isEmpty)
        {
            if (interactItem)
            {
                if (typeItem != "chest" && typeItem != "cloth")
                {
                    interactPanel.SetActive(true);
                    notInteractPanel.SetActive(false);
                    simplePanel.SetActive(false);

                    if (typeItem == "weapon") otherAction.SetActive(true);
                    else otherAction.SetActive(false);

                    //send info to script manage visual text ui
                    panelInfo.nameInt.text = nameThing;
                    panelInfo.damage.text = damage.ToString();
                    panelInfo.collecting.text = ($"{collectWood}/{collectStone}/{collectMetal}");
                    panelInfo.iconItemInt.sprite = iconSlot.sprite;
                }
                else
                {
                    interactPanel.SetActive(false);
                    notInteractPanel.SetActive(false);
                    simplePanel.SetActive(true);

                    panelInfo.nameSimple.text = nameThing;
                    panelInfo.type.text = typeItem;
                    panelInfo.iconItemSimple.sprite = iconSlot.sprite;

                    if (typeItem == "cloth" && !moveCloth)
                    {
                        dressUp.SetActive(true);

                        var cloth = GameObject.Find("system equipment");
                        cloth.GetComponent<system_cloth>().prop = gameObject.GetComponent<slot_item_inventory>();
                    }
                    else dressUp.SetActive(false);
                }
            }
            else
            {
                interactPanel.SetActive(false);
                notInteractPanel.SetActive(true);
                simplePanel.SetActive(false);

                //send info to script manage visual text ui
                panelInfo.nameNotInt.text = nameThing;
                panelInfo.calories.text = calories.ToString();
                panelInfo.addHp.text = addHealth.ToString();
                panelInfo.iconItemNotInt.sprite = iconSlot.sprite;
            }
        }
        else
        {
            interactPanel.SetActive(false);
            notInteractPanel.SetActive(false);
            simplePanel.SetActive(false);
        }
    }

    public void addItemToSlot(item object_)
    {
        var player = GameObject.Find("player_id");
        isEmpty = false;
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
        numbT.text = numberOfThing.ToString();
        object_.destroyItem();
        //player.GetComponent<control>().slotsInPlayerRefresh();
    }
}
