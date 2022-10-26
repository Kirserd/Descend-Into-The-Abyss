using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.IO;

[System.Obsolete]
public class ElementPacker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name, _desc, _info, _type;
    [SerializeField] private RawImage _iconVisualizator;
    [SerializeField] private Animator _notificator;
    [SerializeField] private ImageLoader _imageLoader;

    private string _icon;

    private string _startName, _startIcon;

    private void Start()
    {
        _startName = _name.text;
        _startIcon = _icon;
    }
    public void SetIcon()
    {
        _imageLoader.LoadImagePath();
        _imageLoader.VisualizeImageOn(_iconVisualizator, _imageLoader.LoadImagePath());
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
        {
            _notificator.SetTrigger("Error");
            return;
        }

        _icon = _imageLoader.PackImage(_name.text);

        if (IconValidate() == false)
        {
            _notificator.SetTrigger("Error");
            return;
        }
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
        if (_name == null || _name.text.Length < 2 || _name.text == _startName || _desc == null || _desc.text.Length < 2 || _info == null || _info.text.Length < 2 )
            return false;

        return true;
    }
    private bool IconValidate()
    {
        if (_icon == null || _icon == "" || _icon == _startIcon)
            return false;
        return true;
    }
    private void CreateNewDatabaseElement(DatabaseElement Element)
    {
        string path = Application.dataPath + "/DatabaseElements";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string allPath = path + "/LoadedFiles.txt";
        path += "/" + Element.Name.Replace(' ', '_') + ".txt";
        string content = Element.Name + "\n"
                        + ((int)Element.Type).ToString() + "\n"
                        + Element.Icon + "\n"
                        + Element.Description + "\n"
                        + Element.AdditionalInfo + "\n";

        if (File.Exists(path))
            return;

        File.WriteAllText(path, content);

        if (File.Exists(allPath))
            File.AppendAllText(allPath, path + "\n");
        else
            File.WriteAllText(allPath, path + "\n");
    }
    private IEnumerator TimerToReturn()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("Content").GetComponent<PageShifter>().GoToMain();
    }
}
