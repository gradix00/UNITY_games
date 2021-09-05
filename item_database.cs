using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_database : MonoBehaviour
{
    public string[] nameItem = new string[1];
    public GameObject[] prefabItem = new GameObject[1];

    public void spawnObject(Vector3 pos, Quaternion rot, string name, int number)
    {
        for(int num = 0; num < nameItem.Length; num++)
        {
            if(nameItem[num] == name)
            {
                var obj = prefabItem[num];
                pos.y -= 0.21f;
                obj.GetComponent<item>().numberOfItem = number;
                Instantiate(obj, pos, rot);
                obj.GetComponent<item>().numberOfItem = 1;
            }
        }
    }
}
