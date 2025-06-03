using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float moveSpeed = 4f; // Скорость плавания
    public float changeDirectionTime = 3f; // Время для изменения направления
    private Vector2 targetPosition; // Целевая позиция

    private SpriteRenderer spriteRenderer; // Компонент спрайта

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewTargetPosition();
    }

    void Update()
    {
        // Плавное движение к цели
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Поворачиваем рыбу в сторону цели
        RotateFishTowardsTarget();

        // Если рыба достигла цели, меняем направление
        if ((Vector2)transform.position == targetPosition)
        {
            SetNewTargetPosition();
        }
    }

    // Устанавливаем новую случайную цель на сцене
    void SetNewTargetPosition()
    {
        // Случайная цель в пределах экрана
        float xPos = Random.Range(-8.5f, 8.5f); // Границы сцены!!! -8.5 8.5 по x и  1 до 5 по у
        float yPos = Random.Range(1f, 9f);
        targetPosition = new Vector2(xPos, yPos);
    }

    // Функция для поворота рыбы в сторону цели
    void RotateFishTowardsTarget()
    {
        Vector2 direction = targetPosition - (Vector2)transform.position; // Вычисляем направление
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Преобразуем направление в угол
        
        // Поворачиваем рыбу в сторону цели
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Проверяем направление движения и отражаем рыбу без изменения размера
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = direction.x < 0; // Отражаем рыбу, если она плывёт влево
        }
    }
}
