using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  //������������ ���� ��� TextMeshPro

public class FilterBottle : MonoBehaviour
{
    public InventoryManager inventoryManager; // ������ �� �������� ���������
    public TMP_Text volumeBottle; // �����, ������������ ������� ����� ("1/5")
    public GameObject filterBottleObject; // ������ �������, ������� ����� ����� ��� ����������
    private int currentVolume = 0; // ������� ����� ���� � �������
    private bool isFiltering = false; // ����, ������� ���������, ���� �� ����������

    public Vector3 textOffset = new Vector3(0, 2, 0); // ����� ������ ����� �� 2 ������� �� ��� Y

    private void Update()
    {
        // ��������� ������� ������ ���, ����� �� ��� ��� ��������
        if (volumeBottle != null)
        {
            volumeBottle.transform.position = Camera.main.WorldToScreenPoint(transform.position + textOffset);
        }
    }

    // �����, ������� ���������� ��� ����� �� FilterBottle
    private void OnMouseDown()
    {
        if (currentVolume < 5)
        {
            // �������� �� ������� DirtyWater � ���������
            InventoryItem dirtyWaterItem = inventoryManager.GetItemFromInventory("DirtyWater");

            if (dirtyWaterItem != null && currentVolume < 5)
            {
                // ������� DirtyWater �� ���������
                inventoryManager.RemoveItemFromInventory("DirtyWater");

                // ����������� ������� ����� ���� � �������
                currentVolume++;

                // ��������� ����� ������
                UpdateVolumeText();

                // ���� ������� ���������, �������� ������
                if (currentVolume == 5 && !isFiltering)
                {
                    StartCoroutine(StartFiltering());
                }
            }
            else
            {
                Debug.LogWarning("��� DirtyWater ��� ����������!");
            }
        }
        else
        {
            // ���� ������� ���������, �������� PureWater
            InventoryItem pureWaterItem = new InventoryItem("PureWater", inventoryManager.pureWaterIcon);
            inventoryManager.AddItemToInventory(pureWaterItem);

            // ���������� �����
            currentVolume = 0;

            // ��������� UI
            UpdateVolumeText();
        }
    }

    // ���������� ������ � �������
    private void UpdateVolumeText()
    {
        volumeBottle.text = $"{currentVolume}/5"; // ���������� ������� ����� � ������������ (5/5)
    }

    // ����� ��� ������ �������
    private IEnumerator StartFiltering()
    {
        isFiltering = true;

        // �������� �� 5 ������
        yield return new WaitForSeconds(5f);

        // ����� 5 ������ ������� ����, � ������� ����� ������ ��� ������ PureWater
        isFiltering = false;
        Debug.Log("������� ������ ��� ������ PureWater");
    }
}
