using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptablesObjects/Item")]
public class ItemData : Resetable
{
    public string Name;
    public string Description;
    public GameObject ButtonPrefab;
    public Sprite IconeItem;
}
