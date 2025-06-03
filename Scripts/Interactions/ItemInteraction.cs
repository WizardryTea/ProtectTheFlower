using UnityEngine;
using System.Collections; //��� ���������
using UnityEngine.SceneManagement;
using System.Linq;

public class ItemInteraction : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private Camera mainCamera;
    [SerializeField] private ParticleSystem waterParticles; // ������ ������
    private Coroutine wateringCoroutine;
    private Camera[] allCameras;


    // ���������� ��� ����
    public AudioSource catMeowSound;

    private int catClickCount = 0;
    private const int maxCatClicks = 3;
    private void Start()
    {
        allCameras = FindObjectsOfType<Camera>();

        //���� ������ �� ������ ���������� InventoryManager � ����������
        if (inventoryManager == null)
        {
            inventoryManager = FindFirstObjectByType<InventoryManager>();
            //Debug.LogError("InventoryManager �� ������!");
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
                                                    "������ ������",
                                                    "� ���� ����� ������� ����.");
            inventoryManager.AddItemToInventory(newItem);
            Debug.Log("���� �� ������.");
        }

        if (gameObject.CompareTag("Sink"))
        {
            InventoryItem item = inventoryManager.GetItemFromInventory("EmptyGlass");

            if (item != null)
            {
                // �������� EmptyGlass �� PureyWater
                inventoryManager.RemoveItemFromInventory("EmptyGlass");
                InventoryItem pureWaterItem = new InventoryItem("PureWater", inventoryManager.pureWaterIcon);
                inventoryManager.AddItemToInventory(pureWaterItem);
            }
            else
            {
                Debug.Log("��� ������� � ��������� ��� ������������� � ���������.");
            }
        }

        if (gameObject.CompareTag("Dirty"))
        {
            InventoryItem item = inventoryManager.GetItemFromInventory("EmptyGlass");

            if (item != null)
            {
                // �������� EmptyGlass �� DirtyWater
                inventoryManager.RemoveItemFromInventory("EmptyGlass");
                InventoryItem dirtyWaterItem = new InventoryItem("DirtyWater", inventoryManager.dirtyWaterIcon);
                inventoryManager.AddItemToInventory(dirtyWaterItem);
            }
            else
            {
                Debug.Log("��� ������� � ��������� ��� �������������.");
            }
        }

        if (gameObject.CompareTag("Salt"))
        {
            InventoryItem item = inventoryManager.GetItemFromInventory("EmptyGlass");

            if (item != null)
            {
                // �������� EmptyGlass �� SaltWater
                inventoryManager.RemoveItemFromInventory("EmptyGlass");
                InventoryItem saltWaterItem = new InventoryItem("SaltWater", inventoryManager.saltWaterIcon);
                inventoryManager.AddItemToInventory(saltWaterItem);
            }
            else
            {
                Debug.Log("��� ������� � ��������� ��� ������������� � ���������.");
            }
        }

        // �������� ������ (������� -> ������� -> ������ ����)
        if (gameObject.CompareTag("Flower"))
        {
            // ������� ��������� ������� ����
            InventoryItem saltWaterItem = inventoryManager.GetItemFromInventory("SaltWater");
            if (saltWaterItem != null)
            {
                inventoryManager.RemoveItemFromInventory("SaltWater");
                WaterLevel.MyStaticLink.AddWaterHP(-50f); // �������� 50 HP

                // ���������� �������� �����
                if (waterParticles != null)
                {
                    if (wateringCoroutine != null)
                    {
                        StopCoroutine(wateringCoroutine);
                    }
                    wateringCoroutine = StartCoroutine(PlayWateringEffect());
                }

                Debug.Log("������������ ������� ���� �� ������. -50 � WaterHP");
            }
            // ����� ��������� ������� ����
            else if (inventoryManager.GetItemFromInventory("DirtyWater") != null)
            {
                InventoryItem dirtyWaterItem = inventoryManager.GetItemFromInventory("DirtyWater");
                inventoryManager.RemoveItemFromInventory("DirtyWater");
                WaterLevel.MyStaticLink.AddWaterHP(5f);

                // ���������� �������� �����
                if (waterParticles != null)
                {
                    if (wateringCoroutine != null)
                    {
                        StopCoroutine(wateringCoroutine);
                    }
                    wateringCoroutine = StartCoroutine(PlayWateringEffect());
                }

                Debug.Log("������������ ������� ���� �� ������. +5 � WaterHP");
            }
            // ����� ��������� ������ ����
            else if (inventoryManager.GetItemFromInventory("PureWater") != null)
            {
                InventoryItem pureWaterItem = inventoryManager.GetItemFromInventory("PureWater");
                inventoryManager.RemoveItemFromInventory("PureWater");
                WaterLevel.MyStaticLink.AddWaterHP(20f);

                // ���������� �������� �����
                if (waterParticles != null)
                {
                    if (wateringCoroutine != null)
                    {
                        StopCoroutine(wateringCoroutine);
                    }
                    wateringCoroutine = StartCoroutine(PlayWateringEffect());
                }

                Debug.Log("������������ ������ ���� �� ������. +20 � WaterHP");
            }
            else
            {
                Debug.Log("��� ���� � ��������� ��� ������ ������.");
            }
        }
        if (gameObject.CompareTag("Cat"))
        {
            Debug.Log("���� �� ����.");
            catClickCount++;

            // ������������� ���� ��������, ���� �� ��������
            if (catMeowSound != null)
            {
                catMeowSound.Play();
            }
            else
            {
                Debug.LogWarning("���� �������� �� ��������!");
            }

            Debug.Log($"���� �� ����. ����: {catClickCount}/{maxCatClicks}");

            if (catClickCount >= maxCatClicks)
            {
                // ������������� ������� ������
                Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
                inv.deathReasonKey = "AngryCat";
                inv.showDeathMessage = true;
                GameDataManager.SaveInventory(inv);

                // ��������� ����� ���������
                SceneManager.LoadScene("GameOverScene");
            }
        }
        if (gameObject.CompareTag("Vilka"))
        {
            InventoryItem newItem = new InventoryItem("Vilka",
                                                    inventoryManager.vilkaIcon,
                                                    "�����",
                                                    "�� ��� �� � �������.");
            inventoryManager.AddItemToInventory(newItem);
            gameObject.SetActive(false);
            Debug.Log("����� �����.");
        }
        if (gameObject.CompareTag("Rozetka"))
        {
            InventoryItem VilkaItem = inventoryManager.GetItemFromInventory("Vilka");
            if (VilkaItem != null)
            {
                // ������������� ������� ������
                Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
                inv.deathReasonKey = "Vilka";
                inv.showDeathMessage = true;
                GameDataManager.SaveInventory(inv);

                // ��������� ����� ���������
                SceneManager.LoadScene("GameOverScene");
            }
        }
        if (gameObject.CompareTag("Antispider"))
        {
            InventoryItem newItem = new InventoryItem("Antispider",
                                                    inventoryManager.antispiderIcon,
                                                    "�������� �� ���������",
                                                    "��������� ������ ����������.");
            inventoryManager.AddItemToInventory(newItem);
            gameObject.SetActive(false);
            Debug.Log("����� ��������.");
        }
        if (gameObject.CompareTag("Spider"))
        {
            InventoryItem antispiderItem = inventoryManager.GetItemFromInventory("Antispider");
            if (antispiderItem != null)
            {
                // ������� �������� �� ���������
                inventoryManager.RemoveItemFromInventory("Antispider");

                // �������� �����
                gameObject.SetActive(false);
                Debug.Log("���� ��������� ��������� �� ���������");
            }
            else
            {
                // ���� �������� ��� - ������ �� �����
                Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
                inv.deathReasonKey = "SpiderBite";
                inv.showDeathMessage = true;
                GameDataManager.SaveInventory(inv);

                SceneManager.LoadScene("GameOverScene");
                Debug.Log("��� ������ �������� ����!");
            }
        }
        if (gameObject.CompareTag("Trash"))
        {
            // ������� ��� �������� �� ���������
            inventoryManager.ClearInventory();
            Debug.Log("��� �������� ��������� � �������");
        }
        //������ if
    }

    // �������� ��� ������� ������ � ���������
    private IEnumerator PlayWateringEffect()
    {
        if (waterParticles != null)
        {
            waterParticles.Play();
            yield return new WaitForSeconds(2f); // ��� 2 �������
            waterParticles.Stop();
        }
    }
}