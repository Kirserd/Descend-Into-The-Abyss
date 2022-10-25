using UnityEngine;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
    public List<DatabaseElement> _artifacts { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> _characters { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> _locations { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> _campaigns { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> _music { get; private set; } = new List<DatabaseElement>();
    private void Awake() => InitiateElements();
    private void InitiateElements()
    {
        Object[] preDatabaseElements = Resources.LoadAll("Elements");
        DatabaseElement[] databaseElements = new DatabaseElement[preDatabaseElements.Length];
        for (int i = 0; i < preDatabaseElements.Length; ++i)
        {
            databaseElements[i] = (preDatabaseElements[i] as ScriptableObject) as DatabaseElement;
        }
        foreach (var Element in databaseElements)
        {
            switch (Element.Type)
            {
                case (DataType.ARTIFACT):
                    _artifacts.Add(Element);
                    break;
                case (DataType.CHARACTER):
                    _characters.Add(Element);
                    break;
                case (DataType.LOCATION):
                    _locations.Add(Element);
                    break;
                case (DataType.CAMPAIGN):
                    _campaigns.Add(Element);
                    break;
                case (DataType.MUSIC):
                    _music.Add(Element);
                    break;
            }
        }
    }
}
