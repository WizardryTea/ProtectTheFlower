using UnityEngine;

public class FishFollow : MonoBehaviour
{
    public float moveSpeed = 4f; // �������� ��������
    public Transform targetFish; // ������� ����, �� ������� ����� ��������� ��� ����
    public float followDistance = 2f; // ���������� �� ������� ���� ����� ��������� �� ������ �����

    private SpriteRenderer spriteRenderer; // ��������� �������

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (targetFish == null)
        {
            Debug.LogError("������� ���� �� ��������� (������� ����-���� � ����������)!");
        }
    }

    void Update()
    {
        if (targetFish != null)
        {
            // ������� �� ������� �����
            FollowTargetFish();
        }
    }

    void FollowTargetFish()
    {
        // ��������� ���������� � ����������� ����� ���� ����� � �������
        Vector2 direction = targetFish.position - transform.position;
        float distance = direction.magnitude;

        // ���� ���� ������� ������ �� ����, ��������� � ���
        if (distance > followDistance)
        {
            Vector2 moveDirection = direction.normalized; // ����������� ��������
            transform.position = Vector2.MoveTowards(transform.position, targetFish.position, moveSpeed * Time.deltaTime);
        }

        // ������������ ���� � ������� ������� ����
        RotateFishTowardsTarget(direction);
    }

    void RotateFishTowardsTarget(Vector2 direction)
    {
        // ���� �� ��������� ���� � ��� �������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ����������� ����������� � ����
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // ������������ ���� � ������� ����

        // ��������� ����������� �������� � �������� ���� ��� ��������� �������
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = direction.x < 0; // �������� ����, ���� ��� ����� �����
        }
    }
}
