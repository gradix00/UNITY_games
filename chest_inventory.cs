using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chest_inventory : MonoBehaviour
{
    public string[] name = new string[12];
    public string[] typeItem = new string[12];
    public bool[] interact = new bool[12];
    public bool[] isEmpty = new bool[12];
    public int[] numberOfItem = new int[12];
    public int[] damage = new int[12];
    public int[] calories = new int[12];
    public int[] addHealth = new int[12];
    public int[] collectWood = new int[12];
    public int[] collectStone = new int[12];
    public int[] collectMetal = new int[12];
    public Sprite[] iconItem = new Sprite[12];
    public Sprite none;

    //slots
    public slot_item_inventory[] slots = new slot_item_inventory[12];

    void Start()
    {
        for(int i = 0; i < name.Length; i++)
        {
            var obj = GameObject.Find("system equipment");
            slots[i] = obj.GetComponent<system_cloth>().slot[i];
        }
    }

    public void unloadItemsChest()
    {
        for(int num = 0; num < name.Length; num++)
        {
            if(name[num] != "")
            {
                slots[num].nameThing = name[num];
                slots[num].typeItem = typeItem[num];
                slots[num].numberOfThing = numberOfItem[num];
                slots[num].damage = damage[num];
                slots[num].calories = calories[num];
                slots[num].collectWood = collectWood[num];
                slots[num].collectStone = collectStone[num];
                slots[num].collectMetal = collectMetal[num];
                slots[num].addHealth = addHealth[num];
                slots[num].iconSlot.sprite = iconItem[num];
                slots[num].interactItem = interact[num];
                slots[num].numbT.text = numberOfItem[num].ToString();
                slots[num].isEmpty = isEmpty[num];                
            }
        }
    }

    public void removeItem(string nameItem)
    {
        int id = 0;
        for (int i = 0; i < name.Length; i++)
        {
            if (name[i] == nameItem) { id = i; break; }
        }

        name[id] = null;
        typeItem[id] = null;
        numberOfItem[id] = 0;
        damage[id] = 0;
        calories[id] = 0;
        collectWood[id] = 0;
        collectStone[id] = 0;
        collectMetal[id] = 0;
        addHealth[id] = 0;
        iconItem[id] = none;
        interact[id] = false;
        isEmpty[id] = true;
    }

    public bool checkEmptySlots()
    {
        bool value = false;
        for(int i = 0; i < name.Length; i++)
        {
            if (name[i] == "") { value = true; break; }
            else if((name.Length - 1) == i)
            {
                value = false;
                var fullSlot = GameObject.Find("infoFullSlot");
                fullSlot.GetComponent<Animator>().Play("infoSlot_show");
            }
        }
        return value;
    }

    public void getInfoItem(string n, string type, int numb, int dam, int cal, int cWood, int cStone, int cMetal, int hp, Sprite icon, bool interactable, bool empty)
    {
        for (int i = 0; i < name.Length; i++)
        {
            if (isEmpty[i])
            {
                name[i] = n;
                typeItem[i] = type;
                numberOfItem[i] = numb;
                damage[i] = dam;
                calories[i] = cal;
                collectWood[i] = cWood;
                collectStone[i] = cStone;
                collectMetal[i] = cMetal;
                addHealth[i] = hp;
                iconItem[i] = icon;
                interact[i] = interactable;
                isEmpty[i] = empty;
                break;
            }
            else
            {
                if(name[i] == n && !interactable)
                {
                    name[i] = n;
                    typeItem[i] = type;
                    numberOfItem[i] += numb;
                    damage[i] = dam;
                    calories[i] = cal;
                    collectWood[i] = cWood;
                    collectStone[i] = cStone;
                    collectMetal[i] = cMetal;
                    addHealth[i] = hp;
                    iconItem[i] = icon;
                    interact[i] = interactable;
                    isEmpty[i] = empty;
                    break;
                }
            }
        }
        unloadItemsChest();
    }
}
