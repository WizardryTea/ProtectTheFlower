using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;
    

    private void OnMouseDown()
    {
        if (CompareTag("DoorL")) // ������� � �����
        {
            cameraSwitcher.SwitchToKitchen();
        }
        else if (CompareTag("DoorR")) // ������� � ������� �������
        {
            cameraSwitcher.SwitchToGrannyRoom();
        }
        else if (CompareTag("DoorToMain")) // ����������� � ������� �������
        {
            cameraSwitcher.SwitchToMainRoom();
        }
    }
}
