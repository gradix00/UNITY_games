using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    [Header("item in the world")]
    public bool interactItem = false;

    [Header("Properties item in equipment")]
    public Sprite iconItem = null;
    public string nameItem = null, typeOfItem = null;
    public int numberOfItem = 0;
    public int damage, calories, addHealth, collectWood, collectStone, collectMetal;

    void Start()
    {
        var thisItem = this.gameObject.GetComponent<Rigidbody>();
        thisItem.isKinematic = false;
    }

    void OnColliderEnter(Collision col)
    {
        if(col.gameObject.tag == "terrain")
        {
            var thisItem = this.gameObject.GetComponent<Rigidbody>();
            thisItem.isKinematic = true;
        }
    }

    public void destroyItem()
    {
        Destroy(this.gameObject);
    }
}
