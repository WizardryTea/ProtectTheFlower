using UnityEngine;

public class BackToMain : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;

    private void OnMouseDown()
    {
        cameraSwitcher.SwitchToMainRoom();
    }
}
