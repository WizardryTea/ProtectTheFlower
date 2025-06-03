using UnityEngine;

public class AquaTap : MonoBehaviour
{
    /*--------------------------------*/
    //audioSource.panStereo — это свойство, которое управляет балансом звука между левым и правым каналом.
    //Значение от -1 до 1, где -1 — звук полностью в левом канале, 1 — в правом, а 0 — сбалансированный звук.
    /*--------------------------------*/
    public AudioSource audioSource;  // Источник звука
    public float maxDistance = 10f;  // Максимальное расстояние для полного панорамирования

    void Start()
    {
        // Останавливаем звук, если он не должен играть сразу
        audioSource.Stop();
    }

    void Update()
    {
        // Получаем позицию мыши на экране
        Vector3 mousePosition = Input.mousePosition;

        // Преобразуем позицию мыши в мировые координаты
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f; // Убедимся, что Z-координата остаётся 0, так как это 2D сцена

        // Получаем координаты объекта на сцене (например, SoundObject)
        Vector3 soundPosition = transform.position;

        // Вычисляем разницу по X между позицией мыши и позицией звука
        float distanceFromCenter = Mathf.Clamp((mousePosition.x - soundPosition.x) / maxDistance, -1f, 1f);

        // Применяем панораму в зависимости от расстояния
        audioSource.panStereo = distanceFromCenter;

        // Если кликнули мышью, включаем звук
        if (Input.GetMouseButtonDown(0))  // 0 - для левого клика
        {
            if (!audioSource.isPlaying)  // Запускаем звук, если он не играет
            {
                audioSource.Play();
            }
        }
    }
}
