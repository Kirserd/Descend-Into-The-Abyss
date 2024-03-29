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
    private TextMeshProUGUI _notificatorText;
    private string _startName, _startIcon;

    private void Start()
    {
        _notificatorText = _notificator.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
            "Creature" => DataType.CREATURES,
            _ => DataType.ARTIFACT,
        };
    }

    public void PackElement()
    {
        if (ElementValidate() == false)
        {
            _notificatorText.text = "Fill in all the fields!" +
                "\n" + "----------------------------------------------------------------------------------------------------------------" +
                "\n" + "In the other case, you cannot add the element!";
            _notificator.SetTrigger("Error");
            return;
        }

        _icon = _imageLoader.PackImage(_name.text);

        if (IconValidate() == false)
        {
            _notificatorText.text = "Upload image!" +
                "\n" + "----------------------------------------------------------------------------------------------------------------" +
                "\n" + "In the other case, you cannot add the element!";
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
        string path = "/DatabaseElements";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string ElementName = Element.Name.Replace(' ', '_').Replace('"', '_').Replace('.', '_');
        string allPath = Application.dataPath + path + "/LoadedFiles.txt";
        string pathDesc = path + "/" + ElementName + "_desc.txt";
        string pathInfo = path + "/" + ElementName + "_info.txt";
        path += "/" + ElementName + ".txt";
        string content = Element.Name + "\n"
                        + ((int)Element.Type).ToString() + "\n"
                        + Element.Icon + "\n"
                        + pathDesc + "\n"
                        + pathInfo + "\n";

        if (File.Exists(path))
        {
            _notificator.SetTrigger("Error");
            _notificatorText.text = "File with this name already exists!" +
                "\n" + "----------------------------------------------------------------------------------------------------------------" +
                "\n" + "Please change file name.";
            return;
        }
        File.WriteAllText(Application.dataPath + path, content);
        File.WriteAllText(Application.dataPath + pathDesc, Element.Description);
        File.WriteAllText(Application.dataPath + pathInfo, Element.AdditionalInfo);

        if (File.Exists(allPath))
            File.AppendAllText(allPath, path + "\n");
        else
            File.WriteAllText(allPath, path + "\n");
    }
    private IEnumerator TimerToReturn()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.FindGameObjectWithTag("Content").GetComponent<PageShifter>().GoToMain();
    }
}
