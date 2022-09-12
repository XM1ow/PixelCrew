using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<Item,int> itemDic = new Dictionary<Item,int>();
    // ���ﱣ����һ����Ϸ���õ���������Ʒ Item��Ӧ������Ŀ
    public int LifePoint = 3;
    // ����ֵ ��Ԥ������ϵͳ��д��GameManager��
    public static GameManager Instance()
    {
        if (instance == null)
            Debug.Log("Null Gamemanager");
        return instance;
    }

    private GameManager() { }
    void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    public void AddItem(Item item)
    {
        // if item is a NEW item,then add it to the dic
        if (!itemDic.ContainsKey(item))
        {
            itemDic.Add(item, 1);
            InventoryManager.addNewItem(item);
        }
        // else add the count of item
        else
        {
            itemDic[item]++;
        }
        // display fresh
        InventoryManager.refreshItem();
    }

    public string getItemAmount(Item item)
    {
        int amount = 0;
        if (itemDic.ContainsKey(item))
        {
            amount = itemDic[item];
        }
        return amount.ToString();
    }
}
