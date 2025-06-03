using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory

{
    /*Сохраняем содержимое в файл*/
    //public float totalPlayTime = 0f; // Общее время игры в секундах
    public List<InventoryItem> items = new List<InventoryItem>();
    public float waterHP = 120f;
    public bool newGameReload = true; // Флаг перезагрузки

    // Поля для системы смерти
    public string deathReasonKey = "Default";
    public bool showDeathMessage = true;

    // Метод для добавления элемента в инвентарь
    public void AddItem(InventoryItem item)
    {
        if (items.Count < 6)  // Максимум 6 слотов
        {
            items.Add(item);
        }
    }

    // Метод для поиска предмета в инвентаре
    public InventoryItem GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    // Метод для удаления предмета
    public void RemoveItem(string itemName)
    {
        InventoryItem item = GetItem(itemName);
        if (item != null)
        {
            items.Remove(item);
        }
    }
}
