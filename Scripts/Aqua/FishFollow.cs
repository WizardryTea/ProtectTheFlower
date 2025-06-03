using UnityEngine;

public class FishFollow : MonoBehaviour
{
    public float moveSpeed = 4f; // —корость плавани€
    public Transform targetFish; // ÷елева€ рыба, за которой будет следовать эта рыба
    public float followDistance = 2f; // –ассто€ние на котором рыба будет следовать за другой рыбой

    private SpriteRenderer spriteRenderer; //  омпонент спрайта

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (targetFish == null)
        {
            Debug.LogError("÷елева€ рыба не назначена (выбрать рыбу-мать в инспекторе)!");
        }
    }

    void Update()
    {
        if (targetFish != null)
        {
            // —ледуем за целевой рыбой
            FollowTargetFish();
        }
    }

    void FollowTargetFish()
    {
        // ¬ычисл€ем рассто€ние и направление между этой рыбой и целевой
        Vector2 direction = targetFish.position - transform.position;
        float distance = direction.magnitude;

        // ≈сли рыба слишком далеко от цели, двигаемс€ к ней
        if (distance > followDistance)
        {
            Vector2 moveDirection = direction.normalized; // Ќаправление движени€
            transform.position = Vector2.MoveTowards(transform.position, targetFish.position, moveSpeed * Time.deltaTime);
        }

        // ѕоворачиваем рыбу в сторону целевой рыбы
        RotateFishTowardsTarget(direction);
    }

    void RotateFishTowardsTarget(Vector2 direction)
    {
        // если не учитывать верх и низ спрайта
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ѕреобразуем направление в угол
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // ѕоворачиваем рыбу в сторону цели

        // ѕровер€ем направление движени€ и отражаем рыбу без изменени€ размера
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = direction.x < 0; // ќтражаем рыбу, если она плывЄт влево
        }
    }
}
