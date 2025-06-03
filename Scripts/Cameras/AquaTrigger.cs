using UnityEngine;

public class AquaTrigger : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher; // Ссылка
    public string aquaGameScene = "AquaGameScene"; // Название сцены для AquaGame

    private void OnMouseDown()
    {
        //Debug.Log("Клик по объекту: " + gameObject.name);

        if (sceneSwitcher != null)
        {
            sceneSwitcher.GoToGameAqua(aquaGameScene); // Переход к сцене Aqua
        }
        else
        {
            Debug.LogError("SceneSwitcher не назначен в AquaTrigger!");
        }
    }
}
