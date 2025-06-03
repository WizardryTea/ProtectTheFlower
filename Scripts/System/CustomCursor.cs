using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture; //Cursor.png из Inspector
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    // Точка "активной зоны" курсора (например, центр)

    private void Start()
    {
        // Установка курсора
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);

        // Скрыть стандартный курсор
        // Cursor.visible = false;
    }

    // Для сброса курсора при выходе
    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}