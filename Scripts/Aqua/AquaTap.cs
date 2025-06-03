using UnityEngine;

public class AquaTap : MonoBehaviour
{
    /*--------------------------------*/
    //audioSource.panStereo � ��� ��������, ������� ��������� �������� ����� ����� ����� � ������ �������.
    //�������� �� -1 �� 1, ��� -1 � ���� ��������� � ����� ������, 1 � � ������, � 0 � ���������������� ����.
    /*--------------------------------*/
    public AudioSource audioSource;  // �������� �����
    public float maxDistance = 10f;  // ������������ ���������� ��� ������� ���������������

    void Start()
    {
        // ������������� ����, ���� �� �� ������ ������ �����
        audioSource.Stop();
    }

    void Update()
    {
        // �������� ������� ���� �� ������
        Vector3 mousePosition = Input.mousePosition;

        // ����������� ������� ���� � ������� ����������
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f; // ��������, ��� Z-���������� ������� 0, ��� ��� ��� 2D �����

        // �������� ���������� ������� �� ����� (��������, SoundObject)
        Vector3 soundPosition = transform.position;

        // ��������� ������� �� X ����� �������� ���� � �������� �����
        float distanceFromCenter = Mathf.Clamp((mousePosition.x - soundPosition.x) / maxDistance, -1f, 1f);

        // ��������� �������� � ����������� �� ����������
        audioSource.panStereo = distanceFromCenter;

        // ���� �������� �����, �������� ����
        if (Input.GetMouseButtonDown(0))  // 0 - ��� ������ �����
        {
            if (!audioSource.isPlaying)  // ��������� ����, ���� �� �� ������
            {
                audioSource.Play();
            }
        }
    }
}
