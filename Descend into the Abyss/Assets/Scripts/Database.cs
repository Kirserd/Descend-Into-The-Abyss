using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Database : MonoBehaviour
{
    public List<DatabaseElement> All { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Artifacts { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Characters { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> PlayableCharacters { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Locations { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Campaigns { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Music { get; private set; } = new List<DatabaseElement>();
    public List<DatabaseElement> Creatures{ get; private set; } = new List<DatabaseElement>();
    private void Awake() => InitiateElements();
    private void InitiateElements()
    {
        if (!File.Exists(Application.dataPath + "/DatabaseElements/LoadedFiles.txt"))
            return;

        string[] ElementPaths = File.ReadAllLines(Application.dataPath + "/DatabaseElements/LoadedFiles.txt");
        DatabaseElement[] databaseElements = new DatabaseElement[ElementPaths.Length];
        for (int i = 0; i < ElementPaths.Length; ++i)
        {
            string[] ElementLines = File.ReadAllLines(ElementPaths[i]);
            DataType ElementDataType = ElementLines[1] switch
            {
                "1" => DataType.CHARACTER,
                "2" => DataType.LOCATION,
                "3" => DataType.CAMPAIGN,
                "4" => DataType.MUSIC,
                "5" => DataType.PLAYABLE_CHARACTER,
                "6" => DataType.CREATURES,
                _ => DataType.ARTIFACT,
            };
            databaseElements[i] = new DatabaseElement
            {
                Name = ElementLines[0],
                Type = ElementDataType,
                Icon = ElementLines[2],
                Description = ElementLines[3],
                AdditionalInfo = ElementLines[4]
            };
        }
        foreach (var Element in databaseElements)
        {
            All.Add(Element);
            switch (Element.Type)
            {
                case (DataType.ARTIFACT):
                    Artifacts.Add(Element);
                    break;
                case (DataType.CHARACTER):
                    Characters.Add(Element);
                    break;
                case (DataType.PLAYABLE_CHARACTER):
                    PlayableCharacters.Add(Element);
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
                case (DataType.CREATURES):
                    Creatures.Add(Element);
                    break;
            }
        }
    }
}
