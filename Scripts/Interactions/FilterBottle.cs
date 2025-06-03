using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  //пространство имен для TextMeshPro

public class FilterBottle : MonoBehaviour
{
    public InventoryManager inventoryManager; // ссылка на менеджер инвентаря
    public TMP_Text volumeBottle; // текст, отображающий текущий объем ("1/5")
    public GameObject filterBottleObject; // объект бутылки, который будет виден при заполнении
    private int currentVolume = 0; // текущий объем воды в бутылке
    private bool isFiltering = false; // флаг, который указывает, идет ли фильтрация

    public Vector3 textOffset = new Vector3(0, 2, 0); // Сдвиг текста вверх на 2 единицы по оси Y

    private void Update()
    {
        // Обновляем позицию текста так, чтобы он был над объектом
        if (volumeBottle != null)
        {
            volumeBottle.transform.position = Camera.main.WorldToScreenPoint(transform.position + textOffset);
        }
    }

    // Метод, который вызывается при клике на FilterBottle
    private void OnMouseDown()
    {
        if (currentVolume < 5)
        {
            // Проверка на наличие DirtyWater в инвентаре
            InventoryItem dirtyWaterItem = inventoryManager.GetItemFromInventory("DirtyWater");

            if (dirtyWaterItem != null && currentVolume < 5)
            {
                // Убираем DirtyWater из инвентаря
                inventoryManager.RemoveItemFromInventory("DirtyWater");

                // Увеличиваем текущий объем воды в бутылке
                currentVolume++;

                // Обновляем текст объема
                UpdateVolumeText();

                // Если бутылка заполнена, начинаем таймер
                if (currentVolume == 5 && !isFiltering)
                {
                    StartCoroutine(StartFiltering());
                }
            }
            else
            {
                Debug.LogWarning("Нет DirtyWater для фильтрации!");
            }
        }
        else
        {
            // Если бутылка заполнена, забираем PureWater
            InventoryItem pureWaterItem = new InventoryItem("PureWater", inventoryManager.pureWaterIcon);
            inventoryManager.AddItemToInventory(pureWaterItem);

            // Сбрасываем объем
            currentVolume = 0;

            // Обновляем UI
            UpdateVolumeText();
        }
    }

    // Обновление текста с объемом
    private void UpdateVolumeText()
    {
        volumeBottle.text = $"{currentVolume}/5"; // Отображаем текущий объем и максимальный (5/5)
    }

    // Метод для старта таймера
    private IEnumerator StartFiltering()
    {
        isFiltering = true;

        // Задержка на 5 секунд
        yield return new WaitForSeconds(5f);

        // После 5 секунд очистим флаг, и бутылка будет готова для забора PureWater
        isFiltering = false;
        Debug.Log("Бутылка готова для забора PureWater");
    }
}
