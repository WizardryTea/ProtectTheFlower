using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture; //Cursor.png �� Inspector
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    // ����� "�������� ����" ������� (��������, �����)

    private void Start()
    {
        // ��������� �������
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);

        // ������ ����������� ������
        // Cursor.visible = false;
    }

    // ��� ������ ������� ��� ������
    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}