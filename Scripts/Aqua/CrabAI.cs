using UnityEngine;
using System.Collections;

public class CrabAI : MonoBehaviour
{
    public float moveSpeed = 3f;  // �������� ������������
    public float minX = -5f;      // ����� ������� ������� ��������
    public float maxX = 5f;       // ������ ������� ������� ��������
    public float minY = 1f;       // ������ ������� �������� (����������� �� Y)
    public float maxY = 5f;       // ������� ������� �������� (����������� �� Y)

    private bool movingRight = true; // ����������� ��������
    private SpriteRenderer spriteRenderer; // ��� ��������� �������

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeDirectionRoutine()); // ������ ����� �����������
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // ������� ����� � ������� �����������
        float move = (movingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(move, 0, 0);

        // ����������� �� X, ����� ���� �� ������� �� �����
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

        // ����������� �� Y (����� ���� �� ������ ���� ��� ���� ������)
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f)); // ���� �� 1 �� 5 ������
            movingRight = !movingRight; // ������ �����������
            FlipSprite();
            yield return new WaitForSeconds(Random.Range(1f, 7f)); // ����� 1-7 ������
        }
    }

    void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !movingRight; // �������� ������ ��� ����� �����������
        }
    }
}
