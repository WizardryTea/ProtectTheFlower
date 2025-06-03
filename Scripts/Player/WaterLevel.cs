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
    public float WaterHP; //изменяющийся параметр
    public TextMeshProUGUI waterTextUI; //Ссылка на текст чтобы показывать значение

    [Header("Other")]
    public float DifficultCounter = 1f; // Можно изменить в инспекторе

    public static WaterLevel MyStaticLink; // Статическая ссылка
    private Coroutine waterCoroutine;
    private static bool b_NewGameInit = true;

    void Awake()
    {
        // Убедимся, что только один экземпляр существует
        if (MyStaticLink != null && MyStaticLink != this)
        {
            Destroy(gameObject);
        }
        else
        {
            MyStaticLink = this;
            DontDestroyOnLoad(gameObject); // Сохраняем между сценами
        }

        //MyStaticLink = this; // Присваиваем ссылку при создании

        /*Использование
        WaterLevel.MyStaticLink.InitializeWaterLevel();
        WaterLevel.MyStaticLink.SaveWaterHP();
        */
    }

    void Start()
    {
        //gameStartTime = Time.time;

        // Получаем компонент Image
        WaterBar = GetComponent<Image>();

        if (WaterBar == null)
        {
            Debug.LogError("WaterBar компонент не найден на GameObject!");
            return; // Если компонента нет, останавливаем выполнение скрипта
        }
        //LoadWaterHP();

        if (b_NewGameInit == true)
        {
            InitializeWaterLevel();
            //SaveWaterHP();
            //Debug.LogError("ИНИЦИАЛИЗИРОВАЛИ СНОВА");
            b_NewGameInit = false;
        }
        else
        {
            LoadWaterHP();
        }

    }
    void OnEnable()
    {
        Time.timeScale = 1f; //Иначе при повторном запуске новой игры с главного меню стопорится время + чинится входом-выходом из паузы
        //InitializeWaterLevel(); // Инициализация уровня воды или сброс

        //ЗАГРУЗИТЬ в инвентарь
        LoadWaterHP();

        //Если данных нет то инициализируем макс значением
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
        // Обновляем шкалу воды
        if (WaterBar != null)
        {
            WaterBar.fillAmount = WaterHP / maxWater;
            if (waterTextUI != null)
                // Если надо изменить текст на капле WaterHP
                // waterTextUI.text = $"WaterHP: {WaterHP}/{maxWater}";
                waterTextUI.text = $"{WaterHP}";
        }
    }

    // Инициализация уровня воды (сброс здоровья)
    public void InitializeWaterLevel()
    {
        WaterHP = maxWater; // Начинаем с максимальным количеством воды
        if (waterCoroutine != null)
        {
            StopCoroutine(waterCoroutine); // Останавливаем старую Coroutine, если она была
        }
        waterCoroutine = StartCoroutine(DeductWaterHP()); // Запуск Coroutine для уменьшения воды
    }

    // Coroutine для уменьшения воды каждую секунду с учетом DifficultCounter
    IEnumerator DeductWaterHP()
    {
        while (WaterHP > 0)
        {
            if (WaterHP > DifficultCounter)
            {
                WaterHP -= DifficultCounter; // Уменьшаем на величину DifficultCounter
            }
            else
            {
                WaterHP = 0;
                WaterHPGameOver();
                yield break; // Останавливаем Coroutine
            }
            yield return new WaitForSeconds(1f); // Задержка на 1 секунду
        }
        if (WaterHP <= 0)
        {
            WaterHPGameOver();
        }
    }

    void WaterHPGameOver()
    {

        Debug.Log("Цветок завял! Загружается сцена поражения");
        Inventory inventory = GameDataManager.LoadInventory();
        if (inventory == null)
        {
            inventory = new Inventory();
        }

        Debug.Log("Смерть: засуха");

        // Устанавливаем причину смерти
        Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
        inv.deathReasonKey = "Drought";
        inv.showDeathMessage = true;
        GameDataManager.SaveInventory(inv);
        //InitializeWaterLevel();
        // Загружаем сцену поражения
        SceneManager.LoadScene("GameOverScene");
    }
    /*----для инвентаря*/
    public void SaveWaterHP()
    {
        Inventory inv = GameDataManager.LoadInventory();
        if (inv == null)
        {
            Debug.LogError("Inventory не загружен! Возможно, нет сохранения.");
            inv = new Inventory(); // или создай новый инвентарь
        }
        inv.waterHP = WaterHP;
        Debug.Log($"Сохранение WaterHP: {WaterHP}");
        GameDataManager.SaveInventory(inv);
    }

    public void LoadWaterHP()
    {
        Inventory inv = GameDataManager.LoadInventory();
        WaterHP = inv.waterHP;
        Debug.Log($"Загрузка WaterHP: {WaterHP}");
    }

    public void ShowWaterHP()
    {
        Inventory inv = GameDataManager.LoadInventory();
        Debug.Log($"WaterHP = {WaterHP}");
    }
    //Для полива
    public void AddWaterHP(float amount)
    {
        WaterHP += amount;
        // Убедимся, что не превысили максимум
        if (WaterHP > maxWater)
        {
            WaterHP = maxWater;
        }
        SaveWaterHP(); // Сохраняем изменения
        UpdateWaterUI(); // Обновляем UI
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