using UnityEngine;

public class HideObjectsByLayer : MonoBehaviour
{
    public string layerName = "HiddenLayer";  // Название слоя, который нужно скрыть

    void Start()
    {
        // Получаем индекс слоя по имени
        int layerIndex = LayerMask.NameToLayer(layerName);

        if (layerIndex != -1)  // Проверяем, существует ли слой
        {
            // Используем новый метод FindObjectsByType
            GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            // Проходим по всем объектам
            foreach (GameObject obj in allObjects)
            {
                if (obj.layer == layerIndex)  // Проверяем, на каком слое объект
                {
                    // Отключаем рендеринг объекта
                    Renderer objRenderer = obj.GetComponent<Renderer>();
                    if (objRenderer != null)
                    {
                        objRenderer.enabled = false;  // Отключаем рендер
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"Слой с именем '{layerName}' не найден.");
        }
    }
}
