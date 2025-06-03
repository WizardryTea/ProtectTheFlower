using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Sprite emptyGlassIcon;
    public Sprite dirtyWaterIcon;
    public Sprite pureWaterIcon; // ����� ������ � ��������� = ����� ������
    public Sprite saltWaterIcon;
    public Sprite vilkaIcon;
    public Sprite antispiderIcon;

    // ������ ������ UI
    public Image[] inventorySlots;  // ����� ��� ����������� ������ ��������� � ���������

    public static InventoryManager MyStaticLink; // ����������� ������

    private void Start()
    {
        playerInventory = GameDataManager.LoadInventory(); // ��������� ��������� �� �����
        UpdateInventoryUI();

        //NewGame(); // ����� ��������� ��� ������
    }
    void Awake()
    {
        MyStaticLink = this; // ����������� ������ ��� ��������
        /*�������������
        InventoryManager.MyStaticLink.NewGame();
        */
    }

    public void NewGame()
    {
        // ������� ���������
        playerInventory.items.Clear();

        // ���������� UI � ����������
        UpdateInventoryUI();
        GameDataManager.SaveInventory(playerInventory);
    }


    // ���������� �������� � ���������
    public void AddItemToInventory(InventoryItem item)
    {
        playerInventory.AddItem(item);
        GameDataManager.SaveInventory(playerInventory); // ��������� ��������� � ����
        UpdateInventoryUI();
    }

    // �������� �������� �� ���������
    public void RemoveItemFromInventory(string itemName)
    {
        playerInventory.RemoveItem(itemName);
        GameDataManager.SaveInventory(playerInventory); // ��������� ��������� � ����
        UpdateInventoryUI();
    }

    // ��������� �������� �� ���������
    public InventoryItem GetItemFromInventory(string itemName)
    {
        return playerInventory.GetItem(itemName);
    }

    // ���������� UI ���������
    private void UpdateInventoryUI()
    {
        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            Debug.LogError("Inventory slots are not assigned!");
            return;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // ������� ��� �����
            inventorySlots[i].sprite = null;
            inventorySlots[i].gameObject.SetActive(false);  // �������� ����, ���� �� ������
        }

        // ��������� ������ �� �����, � ������� ���� ��������
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            if (i < inventorySlots.Length)
            {
                inventorySlots[i].sprite = playerInventory.items[i].itemIcon;
                inventorySlots[i].gameObject.SetActive(true); // ���������� ����, ���� �� ��������
            }
        }
    }

    // � ������ InventoryManager �������� ���� �����
    public void ClearInventory()
    {
        if (playerInventory != null)
        {
            playerInventory.items.Clear(); // ������� ������ ���������
            UpdateInventoryUI(); // ��������� ���������
            GameDataManager.SaveInventory(playerInventory); // �� �������� ��������� ���������
        }
    }
}
