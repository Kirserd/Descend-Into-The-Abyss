using UnityEngine;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
    public List<DatabaseElement> Artifacts { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Characters { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> CharacterSheet { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Locations { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Campaigns { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Music { get; private set; } = new List<DatabaseElement>();
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
                    Artifacts.Add(Element);
                    break;
                case (DataType.CHARACTER):
                    Characters.Add(Element);
                    break;
                case (DataType.LOCATION):
                    Locations.Add(Element);
                    break;
                case (DataType.CAMPAIGN):
                    Campaigns.Add(Element);
                    break;
                case (DataType.MUSIC):
                    Music.Add(Element);
                    break;
            }
        }
    }
}
