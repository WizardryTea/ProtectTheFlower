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
        {"Drought", "�� ��������� �������� ������!"},
        {"AngryCat", "��� ���������� � ������ ��� �����!"},
        {"Default", "�� ��������. �������� ��� ���!"},
        {"Fire", "�����! �� ������ ��������� �����."},
        {"Vilka", "�� ��������? ����� � �������?"},
        {"SpiderBite", "��� ������ �������� ����! � ��������� ��� �������������� ��������� �� ��������� ����� ��� ��������."},
    };

    void Start()
    {
        // ��������� ������
        restartButton?.onClick.AddListener(RestartGame);
        //mainMenuButton?.onClick.AddListener(GoToMainMenu);

        // �������� � ����������� ���������
        DisplayDeathMessage();
    }

    void DisplayDeathMessage()
    {
        Inventory inv = GameDataManager.LoadInventory();

        if (inv == null)
        {
            Debug.Log("������ ��� inv == null");
            deathMessageText.text = deathMessages["Default"];
            //playTimeText.text = "����� ����: ����������";
            return;
        }

        // ��������� ����� ����� ����
        if (WaterLevel.MyStaticLink != null)
        {
            //inv.totalPlayTime += WaterLevel.MyStaticLink.GetCurrentPlayTime();
            GameDataManager.SaveInventory(inv);
        }

        string messageKey = inv.showDeathMessage ? inv.deathReasonKey : "Default";
        deathMessageText.text = deathMessages.TryGetValue(messageKey, out var message)
            ? message
            : deathMessages["Default"];

        // ���������� ����� ����
        //if (playTimeText != null)
        //{
        //    playTimeText.text = FormatPlayTime(inv.totalPlayTime);
        //}

        // ���������� ���� ������ ���������
        inv.showDeathMessage = false;
        GameDataManager.SaveInventory(inv);
    }

    private string FormatPlayTime(float seconds)
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(seconds);
        return string.Format("����� ����: {0:D2}:{1:D2}:{2:D2}",
            time.Hours,
            time.Minutes,
            time.Seconds);
    }

    void RestartGame()
    {
        // ����� ��������� ��� ����� ����
        var inv = new Inventory();
        inv.newGameReload = false; // ������������� false, ��� ��� ��� ����� ����� ����������
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