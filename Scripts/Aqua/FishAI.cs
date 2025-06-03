using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float moveSpeed = 4f; // �������� ��������
    public float changeDirectionTime = 3f; // ����� ��� ��������� �����������
    private Vector2 targetPosition; // ������� �������

    private SpriteRenderer spriteRenderer; // ��������� �������

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewTargetPosition();
    }

    void Update()
    {
        // ������� �������� � ����
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ������������ ���� � ������� ����
        RotateFishTowardsTarget();

        // ���� ���� �������� ����, ������ �����������
        if ((Vector2)transform.position == targetPosition)
        {
            SetNewTargetPosition();
        }
    }

    // ������������� ����� ��������� ���� �� �����
    void SetNewTargetPosition()
    {
        // ��������� ���� � �������� ������
        float xPos = Random.Range(-8.5f, 8.5f); // ������� �����!!! -8.5 8.5 �� x �  1 �� 5 �� �
        float yPos = Random.Range(1f, 9f);
        targetPosition = new Vector2(xPos, yPos);
    }

    // ������� ��� �������� ���� � ������� ����
    void RotateFishTowardsTarget()
    {
        Vector2 direction = targetPosition - (Vector2)transform.position; // ��������� �����������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ����������� ����������� � ����
        
        // ������������ ���� � ������� ����
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // ��������� ����������� �������� � �������� ���� ��� ��������� �������
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = direction.x < 0; // �������� ����, ���� ��� ����� �����
        }
    }
}
