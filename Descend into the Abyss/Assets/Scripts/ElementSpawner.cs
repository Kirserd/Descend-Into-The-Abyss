using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.IO;
public enum DataType
{
    ALL = 100,
    ARTIFACT = 0,
    CHARACTER,
    LOCATION,
    CAMPAIGN,
    MUSIC,
    PLAYABLE_CHARACTER,
    CREATURES
}
public class ElementSpawner : MonoBehaviour
{
    private readonly int START_POSITION_Y = 0;
    private readonly int ELEMENT_SIZE_Y = 80;

    private DataType _dataType;
    [SerializeField] private Database _database;
    [SerializeField] private RectTransform _content;
    [SerializeField] private TextMeshProUGUI _type;
    [SerializeField] private TMP_Dropdown _dropType;
        
    private int _currentPositionY;
    private void Awake()
    {
        _dataType = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType;
        _dropType.value = _dataType switch
        {
            DataType.CHARACTER => 2,
            DataType.PLAYABLE_CHARACTER => 0,
            DataType.LOCATION => 3,
            DataType.CAMPAIGN => 4,
            DataType.CREATURES => 5,
            DataType.ALL => 0,
            _ => 1
        };
        _currentPositionY = START_POSITION_Y;
    }
    private void Start() => SpawnElements();

    public void SetDataSort()
    {
        _dataType = _type.text switch
        {
            "Character" => DataType.CHARACTER,
            "PlayableCharacter" => DataType.PLAYABLE_CHARACTER,
            "Location" => DataType.LOCATION,
            "Campaign" => DataType.CAMPAIGN,
            "Creature" => DataType.CREATURES,
            "Without sorting" => DataType.ALL,
            _ => DataType.ARTIFACT
        };

        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType = _dataType;
        ResetElements();
    }

    private void ResetElements()
    {
        _currentPositionY = START_POSITION_Y;
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        SpawnElements();
    }

    private void SpawnElements()
    {
        _ = new List<DatabaseElement>();
        List<DatabaseElement> Database = _dataType switch
        {
            DataType.ALL => _database.All,
            DataType.ARTIFACT => _database.Artifacts,
            DataType.CHARACTER => _database.Characters,
            DataType.PLAYABLE_CHARACTER => _database.PlayableCharacters,
            DataType.LOCATION => _database.Locations,
            DataType.CAMPAIGN => _database.Campaigns,
            DataType.MUSIC => _database.Music,
            DataType.CREATURES => _database.Creatures,
            _ => null,
        };
        if (Database == null)
        {
            Debug.LogError("No database loaded");
            return;
        }
        RectTransform Rt = GetComponent<RectTransform>();
        Rt.sizeDelta = new Vector2(0, -(150 * Database.Count));

        List<string> NDatabase = new List<string>();
        for (int i = 0; i < Database.Count; i++)
        {
            NDatabase.Add(Database[i].Name);
        }
        string[] SDatabase = File.ReadAllLines(Application.dataPath + "/DatabaseElements/LoadedFiles.txt");
        string[] SortedSDatabase = new string[Database.Count];

        int k = 0;
        foreach (var element in SDatabase)
        {
            string[] Name = File.ReadAllLines(Application.dataPath + element);
            if (NDatabase.Contains(Name[0]))
            {
                SortedSDatabase[k] = Application.dataPath + element;
                k++;
            }
        }
        foreach (var element in SortedSDatabase)
        {
            GameObject ElementPrefab = null;
            GameObject[] Prefabs = Resources.LoadAll<GameObject>("Prefabs");
            foreach (var Prefab in Prefabs)
            {
                if (Prefab.name == "Element")
                {
                    ElementPrefab = Prefab;
                    break;
                }
            }
            GameObject ElementObject = Instantiate(ElementPrefab);
            ElementObject.GetComponent<ElementLink>().SetLinkTo(element);
            ElementObject.transform.SetParent(gameObject.transform);
            ElementObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, _currentPositionY, 0);
            ElementObject.GetComponent<RectTransform>().localScale = Vector3.one;
            TextMeshProUGUI ElementText = ElementObject.GetComponentInChildren<TextMeshProUGUI>();
            _content.sizeDelta = new Vector2(_content.rect.width, -_currentPositionY + 80);
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, 0);
            string[] Name = File.ReadAllLines(element);
            ElementText.text = Name[0];
            _currentPositionY -= ELEMENT_SIZE_Y;
        }
    }
}
