using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class system_cloth : MonoBehaviour
{
    public slot_item_inventory prop;
    public string[] name = new string[5];
    public string[] typeCloth = new string[5];
    public GameObject[] playerInWorld = new GameObject[5];
    public GameObject[] playerInEquipment = new GameObject[5];
    public slot_item_inventory[] slot = new slot_item_inventory[12];
    [SerializeField] GameObject simplePanel;

    //body parts
    [SerializeField] slot_item_inventory trousers, shoes, chest, gloves, head;

    public void setCloth(string n)
    {
        for(int num = 0; num < name.Length; num++)
        {
            if(n == name[num])
            {
                playerInWorld[num].SetActive(true);
                playerInEquipment[num].SetActive(true);
            }
        }
        simplePanel.SetActive(false);
    }

    public void removeCloth(string n)
    {
        for (int num = 0; num < name.Length; num++)
        {
            if (n == name[num])
            {
                playerInWorld[num].SetActive(false);
                playerInEquipment[num].SetActive(false);
            }
        }
        simplePanel.SetActive(false);
    }

    public string getTypeCloth(string n)
    {
        var t = "";  
        for (int num = 0; num < name.Length; num++) 
        {
            if (name[num] == n) { t = typeCloth[num]; break; }
        }
        return t;
    }

    public void dressUpPlayer()
    {
        bool succes = false;

        string nameCloth = prop.nameThing;
        string type = getTypeCloth(nameCloth);
        if (type == "head" && head.isEmpty)
        {
            setCloth(prop.nameThing);
            head.isEmpty = prop.isEmpty;
            head.iconSlot.sprite = prop.iconSlot.sprite;
            head.nameThing = prop.nameThing;
            head.typeItem = prop.typeItem;
            head.numberOfThing = prop.numberOfThing;
            head.damage = prop.damage;
            head.calories = prop.calories;
            head.addHealth = prop.addHealth;
            head.collectWood = prop.collectWood;
            head.collectStone = prop.collectStone;
            head.collectMetal = prop.collectMetal;
            head.interactItem = prop.interactItem;
            head.numbT.text = prop.numberOfThing.ToString();
            succes = true;
        }
        else if (type == "chest" && chest.isEmpty)
        {
            setCloth(nameCloth);
            chest.isEmpty = prop.isEmpty;
            chest.iconSlot.sprite = prop.iconSlot.sprite;
            chest.nameThing = prop.nameThing;
            chest.typeItem = prop.typeItem;
            chest.numberOfThing = prop.numberOfThing;
            chest.damage = prop.damage;
            chest.calories = prop.calories;
            chest.addHealth = prop.addHealth;
            chest.collectWood = prop.collectWood;
            chest.collectStone = prop.collectStone;
            chest.collectMetal = prop.collectMetal;
            chest.interactItem = prop.interactItem;
            chest.numbT.text = prop.numberOfThing.ToString();
            succes = true;
        }
        else if (type == "trousers" && trousers.isEmpty)
        {
            setCloth(nameCloth);
            trousers.isEmpty = prop.isEmpty;
            trousers.iconSlot.sprite = prop.iconSlot.sprite;
            trousers.nameThing = prop.nameThing;
            trousers.typeItem = prop.typeItem;
            trousers.numberOfThing = prop.numberOfThing;
            trousers.damage = prop.damage;
            trousers.calories = prop.calories;
            trousers.addHealth = prop.addHealth;
            trousers.collectWood = prop.collectWood;
            trousers.collectStone = prop.collectStone;
            trousers.collectMetal = prop.collectMetal;
            trousers.interactItem = prop.interactItem;
            trousers.numbT.text = prop.numberOfThing.ToString();
            succes = true;
        }
        else if (type == "gloves" && gloves.isEmpty)
        {
            setCloth(nameCloth);
            gloves.isEmpty = prop.isEmpty;
            gloves.iconSlot.sprite = prop.iconSlot.sprite;
            gloves.nameThing = prop.nameThing;
            gloves.typeItem = prop.typeItem;
            gloves.numberOfThing = prop.numberOfThing;
            gloves.damage = prop.damage;
            gloves.calories = prop.calories;
            gloves.addHealth = prop.addHealth;
            gloves.collectWood = prop.collectWood;
            gloves.collectStone = prop.collectStone;
            gloves.collectMetal = prop.collectMetal;
            gloves.interactItem = prop.interactItem;
            gloves.numbT.text = prop.numberOfThing.ToString();
            succes = true;
        }
        else if (type == "shoes" && shoes.isEmpty)
        {
            setCloth(nameCloth);
            shoes.isEmpty = prop.isEmpty;
            shoes.iconSlot.sprite = prop.iconSlot.sprite;
            shoes.nameThing = prop.nameThing;
            shoes.typeItem = prop.typeItem;
            shoes.numberOfThing = prop.numberOfThing;
            shoes.damage = prop.damage;
            shoes.calories = prop.calories;
            shoes.addHealth = prop.addHealth;
            shoes.collectWood = prop.collectWood;
            shoes.collectStone = prop.collectStone;
            shoes.collectMetal = prop.collectMetal;
            shoes.interactItem = prop.interactItem;
            shoes.numbT.text = prop.numberOfThing.ToString();
            succes = true;
        }
        else
        {
            var fullSlot = GameObject.Find("infoFullSlot");
            fullSlot.GetComponent<Animator>().Play("infoSlot_show");
            succes = false;
        }

        if (succes)
        {       
            prop.iconSlot.sprite = prop.none;
            prop.nameThing = null;
            prop.typeItem = null;
            prop.numberOfThing = 0;
            prop.damage = 0;
            prop.calories = 0;
            prop.addHealth = 0;
            prop.collectWood = 0;
            prop.collectStone = 0;
            prop.collectMetal = 0;
            prop.interactItem = false;
            prop.numbT.text = prop.numberOfThing.ToString();
            prop.isEmpty = true;

            //pass on??
            prop.otherSlot.GetComponent<slot_item>().isEmpty = true;
            prop.otherSlot.GetComponent<slot_item>().iconSlot.sprite = prop.iconSlot.sprite;
            prop.otherSlot.GetComponent<slot_item>().nameThing = null;
            prop.otherSlot.GetComponent<slot_item>().typeItem = null;
            prop.otherSlot.GetComponent<slot_item>().numberOfThing = 0;
            prop.otherSlot.GetComponent<slot_item>().damage = 0;
            prop.otherSlot.GetComponent<slot_item>().calories = 0;
            prop.otherSlot.GetComponent<slot_item>().addHealth = 0;
            prop.otherSlot.GetComponent<slot_item>().collectWood = 0;
            prop.otherSlot.GetComponent<slot_item>().collectStone = 0;
            prop.otherSlot.GetComponent<slot_item>().collectMetal = 0;
            prop.otherSlot.GetComponent<slot_item>().interactItem = false;
            prop.otherSlot.GetComponent<slot_item>().numbT.text = prop.numberOfThing.ToString();
        }
    }
}
