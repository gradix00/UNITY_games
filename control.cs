using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public string typeControl;
    [SerializeField] float turnSmoothVelocity, speedMove;
    [SerializeField] float turnSmoothTime = 0.1f;
    float horizontal, vertical;
    bool run, getObject, isChest;
    public GameObject[] slot = new GameObject[6];
    public GameObject[] slotInventory = new GameObject[6];
    public slot_item_inventory[] slotCloth = new slot_item_inventory[5];

    Rigidbody physicPlayer;
    Animator animPlayer;

    //player item in hand | properties
    public int damage, calories, collectWood, collectStone, collectMetal;
    public string nameThing = null, typeItem = null;
    public bool interact = false;

    //item equip add
    public GameObject item = null;
    [SerializeField] GameObject panelEquipment, listSlot, interactPanel, notInteractPanel, simplePanel, slotsChest;
    bool isInventoryOpen = false;

    void Start()
    {
        physicPlayer = this.gameObject.GetComponent<Rigidbody>();
        animPlayer = this.gameObject.GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        if (typeControl == "keyboard")
        {
            if (Input.GetKey(KeyCode.LeftShift)) { speedMove = 395; run = true; }
            else { speedMove = 145; run = false; }

            horizontal = Input.GetAxis("Horizontal") * speedMove * Time.fixedDeltaTime;
            vertical = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;

            //get item
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(getObject) getItem();
                if (isChest) openChest();
            }

            if (Input.GetKeyDown(KeyCode.Escape)) openAndClosedEquip();
        }
        else
        {

        }

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        float targerAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref turnSmoothVelocity, turnSmoothTime);
        this.transform.rotation = Quaternion.Euler(0, angle, 0);

        if (horizontal != 0 || vertical != 0)
        {
            if(!run) animPlayer.SetInteger("action", 1);
            else animPlayer.SetInteger("action", 2);
            physicPlayer.velocity = new Vector3(horizontal, physicPlayer.velocity.y, vertical);
        }
        else animPlayer.SetInteger("action", 0);
    }

    public void openAndClosedEquip()
    {
        if (!isInventoryOpen) { isInventoryOpen = true; listSlot.SetActive(false); }
        else 
        { 
            isInventoryOpen = false; 
            listSlot.SetActive(true); 
            interactPanel.SetActive(false);
            notInteractPanel.SetActive(false);
            simplePanel.SetActive(false); 
            slotsChest.SetActive(false); 

            if(item != null)
            {
                item.GetComponent<Animator>().Play("chest_closing");
                item = null;
            }

            for (int num = 0; num < slotInventory.Length; num++)
            {
                slotInventory[num].GetComponent<slot_item_inventory>().inChest = null;
            }
        }
        panelEquipment.SetActive(isInventoryOpen);
    }

    public bool checkEmptySlot()
    {
        bool value = false;
        for(int i = 0; i < slot.Length; i++)
        {
            if (slot[i].GetComponent<slot_item>().isEmpty) { value = true; break; }
            else if((slot.Length - 1) == i)
            {
                value = false;
                var fullSlot = GameObject.Find("infoFullSlot");
                fullSlot.GetComponent<Animator>().Play("infoSlot_show");
            }
        }
        return value;
    }

    public void addItemToSlot(string nameThing, string type, int numb, int dam, int cal, int cWood, int cStone, int cMetal, int hp, Sprite icon, bool interact, bool empty)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].GetComponent<slot_item>().isEmpty)
            {
                if (interact)
                {
                    slot[i].GetComponent<slot_item>().nameThing = nameThing;
                    slot[i].GetComponent<slot_item>().typeItem = type;
                    slot[i].GetComponent<slot_item>().numberOfThing = numb;
                    slot[i].GetComponent<slot_item>().damage = dam;
                    slot[i].GetComponent<slot_item>().calories = cal;
                    slot[i].GetComponent<slot_item>().collectWood = cWood;
                    slot[i].GetComponent<slot_item>().collectStone = cStone;
                    slot[i].GetComponent<slot_item>().collectMetal = cMetal;
                    slot[i].GetComponent<slot_item>().addHealth = hp;
                    slot[i].GetComponent<slot_item>().iconSlot.sprite = icon;
                    slot[i].GetComponent<slot_item>().interactItem = interact;
                    slot[i].GetComponent<slot_item>().isEmpty = empty;

                    //pass on other slot
                    slot[i].GetComponent<slot_item>().passOnSlot.isEmpty = empty;
                    slot[i].GetComponent<slot_item>().passOnSlot.iconSlot.sprite = icon;
                    slot[i].GetComponent<slot_item>().passOnSlot.nameThing = nameThing;
                    slot[i].GetComponent<slot_item>().passOnSlot.typeItem = type;
                    slot[i].GetComponent<slot_item>().passOnSlot.numberOfThing = numb;
                    slot[i].GetComponent<slot_item>().passOnSlot.damage = dam;
                    slot[i].GetComponent<slot_item>().passOnSlot.calories = cal;
                    slot[i].GetComponent<slot_item>().passOnSlot.addHealth = hp;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectWood = cWood;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectStone = cStone;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectMetal = cMetal;
                    slot[i].GetComponent<slot_item>().passOnSlot.interactItem = interact;

                    slot[i].GetComponent<slot_item>().numbT.text = slot[i].GetComponent<slot_item>().numberOfThing.ToString();
                    slot[i].GetComponent<slot_item>().passOnSlot.numbT.text = slot[i].GetComponent<slot_item>().passOnSlot.numberOfThing.ToString();
                    break;
                }
                else
                {
                    slot[i].GetComponent<slot_item>().nameThing = nameThing;
                    slot[i].GetComponent<slot_item>().typeItem = type;
                    slot[i].GetComponent<slot_item>().numberOfThing += numb;
                    slot[i].GetComponent<slot_item>().damage = dam;
                    slot[i].GetComponent<slot_item>().calories = cal;
                    slot[i].GetComponent<slot_item>().collectWood = cWood;
                    slot[i].GetComponent<slot_item>().collectStone = cStone;
                    slot[i].GetComponent<slot_item>().collectMetal = cMetal;
                    slot[i].GetComponent<slot_item>().addHealth = hp;
                    slot[i].GetComponent<slot_item>().iconSlot.sprite = icon;
                    slot[i].GetComponent<slot_item>().interactItem = interact;
                    slot[i].GetComponent<slot_item>().isEmpty = empty;

                    //pass on other slot
                    slot[i].GetComponent<slot_item>().passOnSlot.isEmpty = empty;
                    slot[i].GetComponent<slot_item>().passOnSlot.iconSlot.sprite = icon;
                    slot[i].GetComponent<slot_item>().passOnSlot.nameThing = nameThing;
                    slot[i].GetComponent<slot_item>().passOnSlot.typeItem = type;
                    slot[i].GetComponent<slot_item>().passOnSlot.numberOfThing += numb;
                    slot[i].GetComponent<slot_item>().passOnSlot.damage = dam;
                    slot[i].GetComponent<slot_item>().passOnSlot.calories = cal;
                    slot[i].GetComponent<slot_item>().passOnSlot.addHealth = hp;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectWood = cWood;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectStone = cStone;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectMetal = cMetal;
                    slot[i].GetComponent<slot_item>().passOnSlot.interactItem = interact;

                    slot[i].GetComponent<slot_item>().numbT.text = slot[i].GetComponent<slot_item>().numberOfThing.ToString();
                    slot[i].GetComponent<slot_item>().passOnSlot.numbT.text = slot[i].GetComponent<slot_item>().passOnSlot.numberOfThing.ToString();
                    break;
                }
            }
            else
            {
                if (slot[i].GetComponent<slot_item>().nameThing == nameThing && !interact)
                {
                    slot[i].GetComponent<slot_item>().nameThing = nameThing;
                    slot[i].GetComponent<slot_item>().typeItem = type;
                    slot[i].GetComponent<slot_item>().numberOfThing += numb;
                    slot[i].GetComponent<slot_item>().damage = dam;
                    slot[i].GetComponent<slot_item>().calories = cal;
                    slot[i].GetComponent<slot_item>().collectWood = cWood;
                    slot[i].GetComponent<slot_item>().collectStone = cStone;
                    slot[i].GetComponent<slot_item>().collectMetal = cMetal;
                    slot[i].GetComponent<slot_item>().addHealth = hp;
                    slot[i].GetComponent<slot_item>().iconSlot.sprite = icon;
                    slot[i].GetComponent<slot_item>().interactItem = interact;
                    slot[i].GetComponent<slot_item>().isEmpty = empty;

                    //pass on other slot
                    slot[i].GetComponent<slot_item>().passOnSlot.isEmpty = empty;
                    slot[i].GetComponent<slot_item>().passOnSlot.iconSlot.sprite = icon;
                    slot[i].GetComponent<slot_item>().passOnSlot.nameThing = nameThing;
                    slot[i].GetComponent<slot_item>().passOnSlot.typeItem = type;
                    slot[i].GetComponent<slot_item>().passOnSlot.numberOfThing += numb;
                    slot[i].GetComponent<slot_item>().passOnSlot.damage = dam;
                    slot[i].GetComponent<slot_item>().passOnSlot.calories = cal;
                    slot[i].GetComponent<slot_item>().passOnSlot.addHealth = hp;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectWood = cWood;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectStone = cStone;
                    slot[i].GetComponent<slot_item>().passOnSlot.collectMetal = cMetal;
                    slot[i].GetComponent<slot_item>().passOnSlot.interactItem = interact;

                    slot[i].GetComponent<slot_item>().numbT.text = slot[i].GetComponent<slot_item>().numberOfThing.ToString();
                    slot[i].GetComponent<slot_item>().passOnSlot.numbT.text = slot[i].GetComponent<slot_item>().passOnSlot.numberOfThing.ToString();
                    break;
                }
            }       
        }
    }

    public void getItem()
    {
        //Debug.Log($"xd");
        if (getObject)
        {
            bool isEmptySlot = checkEmptySlot();
            if (isEmptySlot)
            {
                for (int i = 0; i < slot.Length; i++)
                {
                    if (slot[i].GetComponent<slot_item>().isEmpty)
                    {
                        if (!item.GetComponent<item>().interactItem && slot[i].GetComponent<slot_item>().nameThing != item.GetComponent<item>().nameItem)
                        {
                            slot[i].GetComponent<slot_item>().addItemToSlot(item.GetComponent<item>());
                            break;
                        }
                        else if (item.GetComponent<item>().interactItem)
                        {
                            slot[i].GetComponent<slot_item>().addItemToSlot(item.GetComponent<item>());
                            break;
                        }
                    }
                    else
                    {
                        if (!item.GetComponent<item>().interactItem && slot[i].GetComponent<slot_item>().nameThing == item.GetComponent<item>().nameItem)
                        {
                            slot[i].GetComponent<slot_item>().addItemToSlot(item.GetComponent<item>());
                            break;
                        }
                    }
                }
            }
            item = null;
            getObject = false;
        }
    }

    public void openChest()
    {
        if (isChest)
        {
            openAndClosedEquip();
            slotsChest.SetActive(true);
            isChest = false;

            for (int num = 0; num < slotInventory.Length; num++)
            {
                slotInventory[num].GetComponent<slot_item_inventory>().inChest = item;
            }
            item.GetComponent<chest_inventory>().unloadItemsChest();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "getItem")
        {
            getObject = true;
            item = col.gameObject;           
        }
        else if (col.gameObject.tag == "chest")
        {
            isChest = true;
            item = col.gameObject;
        }       
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "getItem")
        {
            getObject = false;
            item = null;
        }
        else if (col.gameObject.tag == "chest")
        {
            isChest = false;
            item = null;
        }
    }
}
