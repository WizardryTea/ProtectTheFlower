using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
    private static string gameDataFilePath = "Assets/Scripts/Files/GameData.json";

    // ����� ��� ���������� ��������� � ����
    public static void SaveInventory(Inventory inventory)
    {
        string json = JsonUtility.ToJson(inventory);
        File.WriteAllText(gameDataFilePath, json);
    }

    // ����� ��� �������� ��������� �� �����
    public static Inventory LoadInventory()
    {
        if (File.Exists(gameDataFilePath))
        {
            string json = File.ReadAllText(gameDataFilePath);
            return JsonUtility.FromJson<Inventory>(json);
        }
        else
        {
            return new Inventory(); // ���� ����� ���, ������ ����� ���������
        }
    }
}
