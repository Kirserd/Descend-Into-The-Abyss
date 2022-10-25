using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DatabaseElement", order = 1)]
public class DatabaseElement : ScriptableObject
{
    public DataType Type;
    public string Name;
    public Sprite Icon;
    public string Description;
    public string AdditionalInfo;
}
