using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Sprite itemIcon;
    public string ruName = "";
    [TextArea] public string ruDescription = "";
    //[TextArea] public string descriptionEn = "";

    //public InventoryItem(string name, Sprite icon)
    public InventoryItem(string name = "",
                        Sprite icon = null,
                        string ruName = "",
                        string ruDesc = "")
    {
        itemName = name ?? "";
        itemIcon = icon;
        ruName = ruName ?? ""; 
        ruDescription = ruDesc ?? "";
        //descriptionEn = descEn ?? "";
    }
}
