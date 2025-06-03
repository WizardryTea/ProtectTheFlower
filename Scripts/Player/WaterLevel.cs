using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class WaterLevel : MonoBehaviour
{
    [Header("Water Settings")]
    private Image WaterBar;
    public float maxWater = 120f;
    public float WaterHP; //������������ ��������
    public TextMeshProUGUI waterTextUI; //������ �� ����� ����� ���������� ��������

    [Header("Other")]
    public float DifficultCounter = 1f; // ����� �������� � ����������

    public static WaterLevel MyStaticLink; // ����������� ������
    private Coroutine waterCoroutine;
    private static bool b_NewGameInit = true;

    void Awake()
    {
        // ��������, ��� ������ ���� ��������� ����������
        if (MyStaticLink != null && MyStaticLink != this)
        {
            Destroy(gameObject);
        }
        else
        {
            MyStaticLink = this;
            DontDestroyOnLoad(gameObject); // ��������� ����� �������
        }

        //MyStaticLink = this; // ����������� ������ ��� ��������

        /*�������������
        WaterLevel.MyStaticLink.InitializeWaterLevel();
        WaterLevel.MyStaticLink.SaveWaterHP();
        */
    }

    void Start()
    {
        //gameStartTime = Time.time;

        // �������� ��������� Image
        WaterBar = GetComponent<Image>();

        if (WaterBar == null)
        {
            Debug.LogError("WaterBar ��������� �� ������ �� GameObject!");
            return; // ���� ���������� ���, ������������� ���������� �������
        }
        //LoadWaterHP();

        if (b_NewGameInit == true)
        {
            InitializeWaterLevel();
            //SaveWaterHP();
            //Debug.LogError("���������������� �����");
            b_NewGameInit = false;
        }
        else
        {
            LoadWaterHP();
        }

    }
    void OnEnable()
    {
        Time.timeScale = 1f; //����� ��� ��������� ������� ����� ���� � �������� ���� ���������� ����� + ������� ������-������� �� �����
        //InitializeWaterLevel(); // ������������� ������ ���� ��� �����

        //��������� � ���������
        LoadWaterHP();

        //���� ������ ��� �� �������������� ���� ���������
        if (WaterHP <= 0)
        {
            WaterHP = maxWater;
        }

        if (waterCoroutine != null)
            StopCoroutine(waterCoroutine);
        waterCoroutine = StartCoroutine(DeductWaterHP());
    }

    void Update()
    {
        // ��������� ����� ����
        if (WaterBar != null)
        {
            WaterBar.fillAmount = WaterHP / maxWater;
            if (waterTextUI != null)
                // ���� ���� �������� ����� �� ����� WaterHP
                // waterTextUI.text = $"WaterHP: {WaterHP}/{maxWater}";
                waterTextUI.text = $"{WaterHP}";
        }
    }

    // ������������� ������ ���� (����� ��������)
    public void InitializeWaterLevel()
    {
        WaterHP = maxWater; // �������� � ������������ ����������� ����
        if (waterCoroutine != null)
        {
            StopCoroutine(waterCoroutine); // ������������� ������ Coroutine, ���� ��� ����
        }
        waterCoroutine = StartCoroutine(DeductWaterHP()); // ������ Coroutine ��� ���������� ����
    }

    // Coroutine ��� ���������� ���� ������ ������� � ������ DifficultCounter
    IEnumerator DeductWaterHP()
    {
        while (WaterHP > 0)
        {
            if (WaterHP > DifficultCounter)
            {
                WaterHP -= DifficultCounter; // ��������� �� �������� DifficultCounter
            }
            else
            {
                WaterHP = 0;
                WaterHPGameOver();
                yield break; // ������������� Coroutine
            }
            yield return new WaitForSeconds(1f); // �������� �� 1 �������
        }
        if (WaterHP <= 0)
        {
            WaterHPGameOver();
        }
    }

    void WaterHPGameOver()
    {

        Debug.Log("������ �����! ����������� ����� ���������");
        Inventory inventory = GameDataManager.LoadInventory();
        if (inventory == null)
        {
            inventory = new Inventory();
        }

        Debug.Log("������: ������");

        // ������������� ������� ������
        Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
        inv.deathReasonKey = "Drought";
        inv.showDeathMessage = true;
        GameDataManager.SaveInventory(inv);
        //InitializeWaterLevel();
        // ��������� ����� ���������
        SceneManager.LoadScene("GameOverScene");
    }
    /*----��� ���������*/
    public void SaveWaterHP()
    {
        Inventory inv = GameDataManager.LoadInventory();
        if (inv == null)
        {
            Debug.LogError("Inventory �� ��������! ��������, ��� ����������.");
            inv = new Inventory(); // ��� ������ ����� ���������
        }
        inv.waterHP = WaterHP;
        Debug.Log($"���������� WaterHP: {WaterHP}");
        GameDataManager.SaveInventory(inv);
    }

    public void LoadWaterHP()
    {
        Inventory inv = GameDataManager.LoadInventory();
        WaterHP = inv.waterHP;
        Debug.Log($"�������� WaterHP: {WaterHP}");
    }

    public void ShowWaterHP()
    {
        Inventory inv = GameDataManager.LoadInventory();
        Debug.Log($"WaterHP = {WaterHP}");
    }
    //��� ������
    public void AddWaterHP(float amount)
    {
        WaterHP += amount;
        // ��������, ��� �� ��������� ��������
        if (WaterHP > maxWater)
        {
            WaterHP = maxWater;
        }
        SaveWaterHP(); // ��������� ���������
        UpdateWaterUI(); // ��������� UI
    }

    private void UpdateWaterUI()
    {
        if (WaterBar != null)
        {
            WaterBar.fillAmount = WaterHP / maxWater;
            if (waterTextUI != null)
                waterTextUI.text = $"{WaterHP}";
        }
    }

    // public float GetCurrentPlayTime()
    // {
    //    return Time.time - gameStartTime;
    //}
    //------
}