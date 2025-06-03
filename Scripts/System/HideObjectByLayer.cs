using UnityEngine;

public class HideObjectsByLayer : MonoBehaviour
{
    public string layerName = "HiddenLayer";  // �������� ����, ������� ����� ������

    void Start()
    {
        // �������� ������ ���� �� �����
        int layerIndex = LayerMask.NameToLayer(layerName);

        if (layerIndex != -1)  // ���������, ���������� �� ����
        {
            // ���������� ����� ����� FindObjectsByType
            GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            // �������� �� ���� ��������
            foreach (GameObject obj in allObjects)
            {
                if (obj.layer == layerIndex)  // ���������, �� ����� ���� ������
                {
                    // ��������� ��������� �������
                    Renderer objRenderer = obj.GetComponent<Renderer>();
                    if (objRenderer != null)
                    {
                        objRenderer.enabled = false;  // ��������� ������
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"���� � ������ '{layerName}' �� ������.");
        }
    }
}
