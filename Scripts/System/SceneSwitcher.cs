using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void PlayGame(string MainScene)
    {
        // Создаем новую игру
        Inventory inv = new Inventory();
        inv.newGameReload = false; // Устанавливаем false, так как это будет новое сохранение
        inv.items.Clear();

        // Безопасный доступ к MyStaticLink
        float maxWater = 120f; // Значение по умолчанию
        if (WaterLevel.MyStaticLink != null)
        {
            maxWater = WaterLevel.MyStaticLink.maxWater;
            WaterLevel.MyStaticLink.InitializeWaterLevel();
        }
        inv.waterHP = maxWater;
        //inv.totalPlayTime = 0f; // Сбрасываем время

        // Сохраняем настройки новой игры
        GameDataManager.SaveInventory(inv);

        // Обновляем инвентарь
        if (InventoryManager.MyStaticLink != null)
        {
            InventoryManager.MyStaticLink.playerInventory = inv;
            InventoryManager.MyStaticLink.NewGame();
        }

        // Загружаем сцену
        SceneManager.LoadScene(MainScene);

        /*
        //inv.waterHP = 120f;
        inv.waterHP = WaterLevel.MyStaticLink.maxWater;


        // Сохраняем настройки новой игры
        GameDataManager.SaveInventory(inv);

        // Обновляем статические ссылки
        if (InventoryManager.MyStaticLink != null)
        {
            InventoryManager.MyStaticLink.NewGame();
        }

        if (WaterLevel.MyStaticLink != null)
        {
            WaterLevel.MyStaticLink.InitializeWaterLevel();
            WaterLevel.MyStaticLink.SaveWaterHP();
        }

        // Загружаем сцену
        SceneManager.LoadScene(MainScene);*/
    }

    public void ContinueGame(string MainScene)
    {
        // Загружаем сохранение
        Inventory inv = GameDataManager.LoadInventory();

        // Проверяем, есть ли действительное сохранение
        if (inv != null && !inv.newGameReload)
        {
            // Если есть сохранение - загружаем сцену
            SceneManager.LoadScene(MainScene);
        }
        else
        {
            // Если сохранения нет или это новая игра - создаем новую игру
            Debug.LogWarning("Нет доступных сохранений. Начинаем новую игру.");
            PlayGame(MainScene);
        }
    }

    public void GoToMainMenu(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void ExitGame()
    {
        // Для редактора Unity (в редакторе закроется)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Завершаем игру при запуске приложения
#endif
    }

    public void GoToGameAqua(string AquaGameScene)
    {
        if (WaterLevel.MyStaticLink != null)
        {
            Debug.Log("Сохранение WaterHP из SceneSwitcher."); 
            WaterLevel.MyStaticLink.SaveWaterHP();
        }
        if (InventoryManager.MyStaticLink != null)
        {
            GameDataManager.SaveInventory(InventoryManager.MyStaticLink.playerInventory);
        }
        SceneManager.LoadScene(AquaGameScene);
    }

    public void ReturnFromGameAqua(string MainScene)
    {
        WaterLevel.MyStaticLink.SaveWaterHP();
        Debug.Log($"saved value: {GameDataManager.LoadInventory()?.waterHP}");

        SceneManager.LoadScene(MainScene);
    }
}
