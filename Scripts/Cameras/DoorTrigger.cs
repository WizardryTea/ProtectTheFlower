using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;
    

    private void OnMouseDown()
    {
        if (CompareTag("DoorL")) // Переход в кухню
        {
            cameraSwitcher.SwitchToKitchen();
        }
        else if (CompareTag("DoorR")) // Переход в комнату бабушки
        {
            cameraSwitcher.SwitchToGrannyRoom();
        }
        else if (CompareTag("DoorToMain")) // Возвращение в главную комнату
        {
            cameraSwitcher.SwitchToMainRoom();
        }
    }
}
