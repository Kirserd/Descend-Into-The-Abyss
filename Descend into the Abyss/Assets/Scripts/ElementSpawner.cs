using UnityEngine;
using System.Collections.Generic;
using TMPro;

public enum DataType
{
    ARTIFACT,
    CHARACTER,
    LOCATION,
    CAMPAIGN,
    MUSIC,
    PLAYABLE_CHARACTER
}
public class ElementSpawner : MonoBehaviour
{
    private readonly int START_POSITION_Y = 0;
    private readonly int ELEMENT_SIZE_Y = 80;

    [SerializeField] private Database _database;
    [SerializeField] private RectTransform _content;

    private int _currentPositionY;
    private void Awake() => _currentPositionY = START_POSITION_Y;
    private void Start() => SpawnElements(GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType);

    private void SpawnElements(DataType DataType)
    {
        _ = new List<DatabaseElement>();
        List<DatabaseElement> Database = DataType switch
        {
            DataType.ARTIFACT => _database.Artifacts,
            DataType.CHARACTER => _database.Characters,
            DataType.LOCATION => _database.Locations,
            DataType.CAMPAIGN => _database.Campaigns,
            DataType.MUSIC => _database.Music,
            _ => null,
        };
        if (Database == null)
        {
            Debug.LogError("No database loaded");
            return;
        }
        RectTransform Rt = GetComponent<RectTransform>();
        Rt.sizeDelta = new Vector2(0, -(150 * Database.Count));
        
        foreach (var element in Database)
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
            if(ElementPrefab == null)
            {
                Debug.LogError("No prefab found");
                return;
            }
            GameObject ElementObject = Instantiate(ElementPrefab);
            ElementObject.GetComponent<ElementLink>().SetLinkTo(element);
            ElementObject.transform.SetParent(gameObject.transform);
            ElementObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, _currentPositionY, 0);
            ElementObject.GetComponent<RectTransform>().localScale = Vector3.one;
            TextMeshProUGUI ElementText = ElementObject.GetComponentInChildren<TextMeshProUGUI>();
            _content.sizeDelta = new Vector2(_content.rect.width, -_currentPositionY + 80);
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, 0);
            ElementText.text = element.Name;
            _currentPositionY -= ELEMENT_SIZE_Y;
        }
    }
}
