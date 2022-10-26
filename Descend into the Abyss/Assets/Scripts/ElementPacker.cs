using UnityEngine;
using System.Collections;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class ElementPacker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name, _desc, _info, _type;
    [SerializeField] private Image _iconVisualizator;
    private Sprite _icon;
    public void SetIcon()
    {
        string LoadedImagePath = ImageLoader.LoadImage();
        Sprite LoadedTexture = Resources.Load<Sprite>(LoadedImagePath);
        _icon = LoadedTexture;
        _iconVisualizator.sprite = LoadedTexture;
    }

    private DataType GetDataType()
    {
        return _type.text switch
        {
            "Character" => DataType.CHARACTER,
            "PlayableCharacter" => DataType.PLAYABLE_CHARACTER,
            "Location" => DataType.LOCATION,
            "Campaign" => DataType.CAMPAIGN,
            _ => DataType.ARTIFACT,
        };
    }

    public void PackElement()
    {
        if (ElementValidate() == false)
            return;
        
        DatabaseElement Element = new()
        {
            Name = _name.text,
            Description = _desc.text,
            Type = GetDataType(),
            AdditionalInfo = _info.text,
            Icon = _icon
        };
        CreateNewDatabaseElement(Element);
        StartCoroutine(TimerToReturn());
    }
    private bool ElementValidate()
    {
        if (_name == null || _name.text == "")
            return false;
        if (_desc == null || _desc.text == "")
            return false;
        if (_info == null || _info.text == "")
            return false;
        if (_icon == null)
            return false;
        
        try
        {
            Sprite IconCheck = _icon;
        }
        catch (MissingReferenceException)
        {
            return false;
        }

        return true;
    }
    private static void CreateNewDatabaseElement(DatabaseElement Element)
    {
        AssetDatabase.CreateAsset(Element, "Assets/Resources/Elements/" + "0" + Element.Name.Replace(" ", "") + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = Element; 
    }
    private IEnumerator TimerToReturn()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("Content").GetComponent<PageShifter>().GoToMain();
    }
}
