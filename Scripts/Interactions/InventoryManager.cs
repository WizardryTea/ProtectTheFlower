using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Sprite emptyGlassIcon;
    public Sprite dirtyWaterIcon;
    public Sprite pureWaterIcon; // Новый объект в инвентаре = новая иконка
    public Sprite saltWaterIcon;
    public Sprite vilkaIcon;
    public Sprite antispiderIcon;

    // Массив слотов UI
    public Image[] inventorySlots;  // Слоты для отображения иконок предметов в инвентаре

    public static InventoryManager MyStaticLink; // Статическая ссылка

    private void Start()
    {
        playerInventory = GameDataManager.LoadInventory(); // Загружаем инвентарь из файла
        UpdateInventoryUI();

        //NewGame(); // Сброс инвентаря при старте
    }
    void Awake()
    {
        MyStaticLink = this; // Присваиваем ссылку при создании
        /*Использование
        InventoryManager.MyStaticLink.NewGame();
        */
    }

    public void NewGame()
    {
        // Очистка инвентаря
        playerInventory.items.Clear();

        // Обновление UI и сохранение
        UpdateInventoryUI();
        GameDataManager.SaveInventory(playerInventory);
    }


    // Добавление предмета в инвентарь
    public void AddItemToInventory(InventoryItem item)
    {
        playerInventory.AddItem(item);
        GameDataManager.SaveInventory(playerInventory); // Сохраняем инвентарь в файл
        UpdateInventoryUI();
    }

    // Удаление предмета из инвентаря
    public void RemoveItemFromInventory(string itemName)
    {
        playerInventory.RemoveItem(itemName);
        GameDataManager.SaveInventory(playerInventory); // Сохраняем инвентарь в файл
        UpdateInventoryUI();
    }

    // Получение предмета из инвентаря
    public InventoryItem GetItemFromInventory(string itemName)
    {
        return playerInventory.GetItem(itemName);
    }

    // Обновление UI инвентаря
    private void UpdateInventoryUI()
    {
        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            Debug.LogError("Inventory slots are not assigned!");
            return;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Очищаем все слоты
            inventorySlots[i].sprite = null;
            inventorySlots[i].gameObject.SetActive(false);  // Скрываем слот, если он пустой
        }

        // Обновляем только те слоты, в которых есть предметы
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            if (i < inventorySlots.Length)
            {
                inventorySlots[i].sprite = playerInventory.items[i].itemIcon;
                inventorySlots[i].gameObject.SetActive(true); // Показываем слот, если он заполнен
            }
        }
    }

    // В классе InventoryManager добавьте этот метод
    public void ClearInventory()
    {
        if (playerInventory != null)
        {
            playerInventory.items.Clear(); // Очищаем список предметов
            UpdateInventoryUI(); // Обновляем интерфейс
            GameDataManager.SaveInventory(playerInventory); // Не забываем сохранить изменения
        }
    }
}
