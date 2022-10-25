using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ElementUnpacker : MonoBehaviour
{
    private DatabaseElement PackedElement;
    [SerializeField] private TextMeshProUGUI _name, _name2, _description, _additionalInfo;
    [SerializeField] private Image _image;
    private void Start()
    {
        Unpack();
        Visualize();
    }
    private void Unpack() => PackedElement = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentElement;
    private void Visualize()
    {
        _name.text = PackedElement.Name;
        _name2.text = PackedElement.Name;
        _description.text = PackedElement.Description;
        _additionalInfo.text = PackedElement.AdditionalInfo;
        _image.sprite = PackedElement.Icon;
        GetComponent<PageAdjuster>().InitiateAdjustment();
    }
}
