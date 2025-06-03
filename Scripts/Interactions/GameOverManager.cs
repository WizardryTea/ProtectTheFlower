using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI deathMessageText;
    public TextMeshProUGUI playTimeText;
    public Button restartButton;
    public Button mainMenuButton;

    private Dictionary<string, string> deathMessages = new Dictionary<string, string>()
    {
        {"Drought", "Не забывайте поливать цветок!"},
        {"AngryCat", "Кот разозлился и сожрал все цветы!"},
        {"Default", "Ты проиграл. Попробуй еще раз!"},
        {"Fire", "Пожар! Вы забыли выключить плиту."},
        {"Vilka", "Ты серьезно? Вилка в розетку?"},
        {"SpiderBite", "Вас укусил ядовитый паук! В следующий раз воспользуйтесь средством от насекомых чтобы его прогнать."},
    };

    void Start()
    {
        // Настройка кнопок
        restartButton?.onClick.AddListener(RestartGame);
        //mainMenuButton?.onClick.AddListener(GoToMainMenu);

        // Загрузка и отображение сообщения
        DisplayDeathMessage();
    }

    void DisplayDeathMessage()
    {
        Inventory inv = GameDataManager.LoadInventory();

        if (inv == null)
        {
            Debug.Log("Смерть при inv == null");
            deathMessageText.text = deathMessages["Default"];
            //playTimeText.text = "Время игры: неизвестно";
            return;
        }

        // Обновляем общее время игры
        if (WaterLevel.MyStaticLink != null)
        {
            //inv.totalPlayTime += WaterLevel.MyStaticLink.GetCurrentPlayTime();
            GameDataManager.SaveInventory(inv);
        }

        string messageKey = inv.showDeathMessage ? inv.deathReasonKey : "Default";
        deathMessageText.text = deathMessages.TryGetValue(messageKey, out var message)
            ? message
            : deathMessages["Default"];

        // Отображаем время игры
        //if (playTimeText != null)
        //{
        //    playTimeText.text = FormatPlayTime(inv.totalPlayTime);
        //}

        // Сбрасываем флаг показа сообщения
        inv.showDeathMessage = false;
        GameDataManager.SaveInventory(inv);
    }

    private string FormatPlayTime(float seconds)
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(seconds);
        return string.Format("Время игры: {0:D2}:{1:D2}:{2:D2}",
            time.Hours,
            time.Minutes,
            time.Seconds);
    }

    void RestartGame()
    {
        // Сброс инвентаря для новой игры
        var inv = new Inventory();
        inv.newGameReload = false; // Устанавливаем false, так как это будет новое сохранение
        inv.waterHP = WaterLevel.MyStaticLink?.maxWater ?? 120f;
        GameDataManager.SaveInventory(inv);

        inv.deathReasonKey = "Default";
        inv.showDeathMessage = false;

        SceneManager.LoadScene("MainScene");
    }

    //void GoToMainMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}
}