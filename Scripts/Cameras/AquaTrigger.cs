using UnityEngine;

public class AquaTrigger : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher; // ������
    public string aquaGameScene = "AquaGameScene"; // �������� ����� ��� AquaGame

    private void OnMouseDown()
    {
        //Debug.Log("���� �� �������: " + gameObject.name);

        if (sceneSwitcher != null)
        {
            sceneSwitcher.GoToGameAqua(aquaGameScene); // ������� � ����� Aqua
        }
        else
        {
            Debug.LogError("SceneSwitcher �� �������� � AquaTrigger!");
        }
    }
}
