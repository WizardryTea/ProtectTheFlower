using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void PlayGame(string MainScene)
    {
        // ������� ����� ����
        Inventory inv = new Inventory();
        inv.newGameReload = false; // ������������� false, ��� ��� ��� ����� ����� ����������
        inv.items.Clear();

        // ���������� ������ � MyStaticLink
        float maxWater = 120f; // �������� �� ���������
        if (WaterLevel.MyStaticLink != null)
        {
            maxWater = WaterLevel.MyStaticLink.maxWater;
            WaterLevel.MyStaticLink.InitializeWaterLevel();
        }
        inv.waterHP = maxWater;
        //inv.totalPlayTime = 0f; // ���������� �����

        // ��������� ��������� ����� ����
        GameDataManager.SaveInventory(inv);

        // ��������� ���������
        if (InventoryManager.MyStaticLink != null)
        {
            InventoryManager.MyStaticLink.playerInventory = inv;
            InventoryManager.MyStaticLink.NewGame();
        }

        // ��������� �����
        SceneManager.LoadScene(MainScene);

        /*
        //inv.waterHP = 120f;
        inv.waterHP = WaterLevel.MyStaticLink.maxWater;


        // ��������� ��������� ����� ����
        GameDataManager.SaveInventory(inv);

        // ��������� ����������� ������
        if (InventoryManager.MyStaticLink != null)
        {
            InventoryManager.MyStaticLink.NewGame();
        }

        if (WaterLevel.MyStaticLink != null)
        {
            WaterLevel.MyStaticLink.InitializeWaterLevel();
            WaterLevel.MyStaticLink.SaveWaterHP();
        }

        // ��������� �����
        SceneManager.LoadScene(MainScene);*/
    }

    public void ContinueGame(string MainScene)
    {
        // ��������� ����������
        Inventory inv = GameDataManager.LoadInventory();

        // ���������, ���� �� �������������� ����������
        if (inv != null && !inv.newGameReload)
        {
            // ���� ���� ���������� - ��������� �����
            SceneManager.LoadScene(MainScene);
        }
        else
        {
            // ���� ���������� ��� ��� ��� ����� ���� - ������� ����� ����
            Debug.LogWarning("��� ��������� ����������. �������� ����� ����.");
            PlayGame(MainScene);
        }
    }

    public void GoToMainMenu(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void ExitGame()
    {
        // ��� ��������� Unity (� ��������� ���������)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ��������� ���� ��� ������� ����������
#endif
    }

    public void GoToGameAqua(string AquaGameScene)
    {
        if (WaterLevel.MyStaticLink != null)
        {
            Debug.Log("���������� WaterHP �� SceneSwitcher."); 
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
