using UnityEngine;
using System.Collections; //для корунтины
using UnityEngine.SceneManagement;
using System.Linq;

public class ItemInteraction : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private Camera mainCamera;
    [SerializeField] private ParticleSystem waterParticles; // эффект полива
    private Coroutine wateringCoroutine;
    private Camera[] allCameras;


    // Переменные для кота
    public AudioSource catMeowSound;

    private int catClickCount = 0;
    private const int maxCatClicks = 3;
    private void Start()
    {
        allCameras = FindObjectsOfType<Camera>();

        //Если выпало из памяти установить InventoryManager в инспекторе
        if (inventoryManager == null)
        {
            inventoryManager = FindFirstObjectByType<InventoryManager>();
            //Debug.LogError("InventoryManager не найден!");
            //return;
        }
        if (waterParticles != null)
        {
            waterParticles.Stop();
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("OneGlass"))
        {
            InventoryItem newItem = new InventoryItem("EmptyGlass",
                                                    inventoryManager.emptyGlassIcon,
                                                    "Пустой стакан",
                                                    "В него можно набрать воды.");
            inventoryManager.AddItemToInventory(newItem);
            Debug.Log("Клик на стакан.");
        }

        if (gameObject.CompareTag("Sink"))
        {
            InventoryItem item = inventoryManager.GetItemFromInventory("EmptyGlass");

            if (item != null)
            {
                // Заменяем EmptyGlass на PureyWater
                inventoryManager.RemoveItemFromInventory("EmptyGlass");
                InventoryItem pureWaterItem = new InventoryItem("PureWater", inventoryManager.pureWaterIcon);
                inventoryManager.AddItemToInventory(pureWaterItem);
            }
            else
            {
                Debug.Log("Нет стакана в инвентаре для использования с раковиной.");
            }
        }

        if (gameObject.CompareTag("Dirty"))
        {
            InventoryItem item = inventoryManager.GetItemFromInventory("EmptyGlass");

            if (item != null)
            {
                // Заменяем EmptyGlass на DirtyWater
                inventoryManager.RemoveItemFromInventory("EmptyGlass");
                InventoryItem dirtyWaterItem = new InventoryItem("DirtyWater", inventoryManager.dirtyWaterIcon);
                inventoryManager.AddItemToInventory(dirtyWaterItem);
            }
            else
            {
                Debug.Log("Нет стакана в инвентаре для использования.");
            }
        }

        if (gameObject.CompareTag("Salt"))
        {
            InventoryItem item = inventoryManager.GetItemFromInventory("EmptyGlass");

            if (item != null)
            {
                // Заменяем EmptyGlass на SaltWater
                inventoryManager.RemoveItemFromInventory("EmptyGlass");
                InventoryItem saltWaterItem = new InventoryItem("SaltWater", inventoryManager.saltWaterIcon);
                inventoryManager.AddItemToInventory(saltWaterItem);
            }
            else
            {
                Debug.Log("Нет стакана в инвентаре для использования с раковиной.");
            }
        }

        // Поливаем цветок (соленая -> грязная -> чистая вода)
        if (gameObject.CompareTag("Flower"))
        {
            // Сначала проверяем грязную воду
            InventoryItem saltWaterItem = inventoryManager.GetItemFromInventory("SaltWater");
            if (saltWaterItem != null)
            {
                inventoryManager.RemoveItemFromInventory("SaltWater");
                WaterLevel.MyStaticLink.AddWaterHP(-50f); // Отнимаем 50 HP

                // Визуальная обратная связь
                if (waterParticles != null)
                {
                    if (wateringCoroutine != null)
                    {
                        StopCoroutine(wateringCoroutine);
                    }
                    wateringCoroutine = StartCoroutine(PlayWateringEffect());
                }

                Debug.Log("Использована соленая вода на цветок. -50 к WaterHP");
            }
            // Затем проверяем грязную воду
            else if (inventoryManager.GetItemFromInventory("DirtyWater") != null)
            {
                InventoryItem dirtyWaterItem = inventoryManager.GetItemFromInventory("DirtyWater");
                inventoryManager.RemoveItemFromInventory("DirtyWater");
                WaterLevel.MyStaticLink.AddWaterHP(5f);

                // Визуальная обратная связь
                if (waterParticles != null)
                {
                    if (wateringCoroutine != null)
                    {
                        StopCoroutine(wateringCoroutine);
                    }
                    wateringCoroutine = StartCoroutine(PlayWateringEffect());
                }

                Debug.Log("Использована грязная вода на цветок. +5 к WaterHP");
            }
            // Затем проверяем чистую воду
            else if (inventoryManager.GetItemFromInventory("PureWater") != null)
            {
                InventoryItem pureWaterItem = inventoryManager.GetItemFromInventory("PureWater");
                inventoryManager.RemoveItemFromInventory("PureWater");
                WaterLevel.MyStaticLink.AddWaterHP(20f);

                // Визуальная обратная связь
                if (waterParticles != null)
                {
                    if (wateringCoroutine != null)
                    {
                        StopCoroutine(wateringCoroutine);
                    }
                    wateringCoroutine = StartCoroutine(PlayWateringEffect());
                }

                Debug.Log("Использована чистая вода на цветок. +20 к WaterHP");
            }
            else
            {
                Debug.Log("Нет воды в инвентаре для полива цветка.");
            }
        }
        if (gameObject.CompareTag("Cat"))
        {
            Debug.Log("Клик на кота.");
            catClickCount++;

            // Воспроизводим звук мяуканья, если он назначен
            if (catMeowSound != null)
            {
                catMeowSound.Play();
            }
            else
            {
                Debug.LogWarning("Звук мяуканья не назначен!");
            }

            Debug.Log($"Клик на кота. Счет: {catClickCount}/{maxCatClicks}");

            if (catClickCount >= maxCatClicks)
            {
                // Устанавливаем причину смерти
                Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
                inv.deathReasonKey = "AngryCat";
                inv.showDeathMessage = true;
                GameDataManager.SaveInventory(inv);

                // Загружаем сцену поражения
                SceneManager.LoadScene("GameOverScene");
            }
        }
        if (gameObject.CompareTag("Vilka"))
        {
            InventoryItem newItem = new InventoryItem("Vilka",
                                                    inventoryManager.vilkaIcon,
                                                    "Вилка",
                                                    "Не суй ее в розетку.");
            inventoryManager.AddItemToInventory(newItem);
            gameObject.SetActive(false);
            Debug.Log("Взять вилку.");
        }
        if (gameObject.CompareTag("Rozetka"))
        {
            InventoryItem VilkaItem = inventoryManager.GetItemFromInventory("Vilka");
            if (VilkaItem != null)
            {
                // Устанавливаем причину смерти
                Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
                inv.deathReasonKey = "Vilka";
                inv.showDeathMessage = true;
                GameDataManager.SaveInventory(inv);

                // Загружаем сцену поражения
                SceneManager.LoadScene("GameOverScene");
            }
        }
        if (gameObject.CompareTag("Antispider"))
        {
            InventoryItem newItem = new InventoryItem("Antispider",
                                                    inventoryManager.antispiderIcon,
                                                    "Средство от насекомых",
                                                    "Используй против вредителей.");
            inventoryManager.AddItemToInventory(newItem);
            gameObject.SetActive(false);
            Debug.Log("Взять средство.");
        }
        if (gameObject.CompareTag("Spider"))
        {
            InventoryItem antispiderItem = inventoryManager.GetItemFromInventory("Antispider");
            if (antispiderItem != null)
            {
                // Удаляем средство из инвентаря
                inventoryManager.RemoveItemFromInventory("Antispider");

                // Скрываем паука
                gameObject.SetActive(false);
                Debug.Log("Паук уничтожен средством от насекомых");
            }
            else
            {
                // Если средства нет - смерть от паука
                Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
                inv.deathReasonKey = "SpiderBite";
                inv.showDeathMessage = true;
                GameDataManager.SaveInventory(inv);

                SceneManager.LoadScene("GameOverScene");
                Debug.Log("Вас укусил ядовитый паук!");
            }
        }
        if (gameObject.CompareTag("Trash"))
        {
            // Удаляем все предметы из инвентаря
            inventoryManager.ClearInventory();
            Debug.Log("Все предметы выброшены в мусорку");
        }
        //другие if
    }

    // Корутина для эффекта полива с задержкой
    private IEnumerator PlayWateringEffect()
    {
        if (waterParticles != null)
        {
            waterParticles.Play();
            yield return new WaitForSeconds(2f); // Ждём 2 секунды
            waterParticles.Stop();
        }
    }
}