using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
    private static string gameDataFilePath = "Assets/Scripts/Files/GameData.json";

    // Метод для сохранения инвентаря в файл
    public static void SaveInventory(Inventory inventory)
    {
        string json = JsonUtility.ToJson(inventory);
        File.WriteAllText(gameDataFilePath, json);
    }

    // Метод для загрузки инвентаря из файла
    public static Inventory LoadInventory()
    {
        if (File.Exists(gameDataFilePath))
        {
            string json = File.ReadAllText(gameDataFilePath);
            return JsonUtility.FromJson<Inventory>(json);
        }
        else
        {
            return new Inventory(); // Если файла нет, создаём новый инвентарь
        }
    }
}
