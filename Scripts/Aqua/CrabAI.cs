using UnityEngine;
using System.Collections;

public class CrabAI : MonoBehaviour
{
    public float moveSpeed = 3f;  // Скорость передвижения
    public float minX = -5f;      // Левая граница области движения
    public float maxX = 5f;       // Правая граница области движения
    public float minY = 1f;       // Нижняя граница движения (ограничение по Y)
    public float maxY = 5f;       // Верхняя граница движения (ограничение по Y)

    private bool movingRight = true; // Направление движения
    private SpriteRenderer spriteRenderer; // Для отражения спрайта

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeDirectionRoutine()); // Запуск смены направления
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Двигаем краба в текущем направлении
        float move = (movingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(move, 0, 0);

        // Ограничение по X, чтобы краб не выбегал за экран
        if (transform.position.x >= maxX)
        {
            movingRight = false;
            FlipSprite();
        }
        else if (transform.position.x <= minX)
        {
            movingRight = true;
            FlipSprite();
        }

        // Ограничение по Y (чтобы краб не улетал выше или ниже границ)
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f)); // Ждем от 1 до 5 секунд
            movingRight = !movingRight; // Меняем направление
            FlipSprite();
            yield return new WaitForSeconds(Random.Range(1f, 7f)); // Бежим 1-7 секунд
        }
    }

    void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !movingRight; // Отражаем спрайт при смене направления
        }
    }
}
