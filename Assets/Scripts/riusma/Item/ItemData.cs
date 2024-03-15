using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptablesObjects/Item")]
public class ItemData : Resetable
{
    public bool friendly = false ; 

    [TextArea(10,20)]
    public string Name;


    public string Description;
    public Sprite IconeItem;

    public int valHeal;

    public Effect[] effect;
}
