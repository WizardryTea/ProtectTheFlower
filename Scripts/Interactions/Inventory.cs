using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory

{
    /*��������� ���������� � ����*/
    //public float totalPlayTime = 0f; // ����� ����� ���� � ��������
    public List<InventoryItem> items = new List<InventoryItem>();
    public float waterHP = 120f;
    public bool newGameReload = true; // ���� ������������

    // ���� ��� ������� ������
    public string deathReasonKey = "Default";
    public bool showDeathMessage = true;

    // ����� ��� ���������� �������� � ���������
    public void AddItem(InventoryItem item)
    {
        if (items.Count < 6)  // �������� 6 ������
        {
            items.Add(item);
        }
    }

    // ����� ��� ������ �������� � ���������
    public InventoryItem GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    // ����� ��� �������� ��������
    public void RemoveItem(string itemName)
    {
        InventoryItem item = GetItem(itemName);
        if (item != null)
        {
            items.Remove(item);
        }
    }
}
