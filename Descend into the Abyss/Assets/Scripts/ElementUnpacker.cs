using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

[System.Obsolete]
public class ElementUnpacker : MonoBehaviour
{
    private string _packedElement;
    [SerializeField] private TextMeshProUGUI _name, _name2, _description, _additionalInfo;
    [SerializeField] private RawImage _image;
    [SerializeField] private ImageLoader _imageLoader;
    private void Start()
    {
        Unpack();
        Visualize();
    }
    private void Unpack() => _packedElement = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentElement;
    private void Visualize()
    {
        string[] PackedElement = File.ReadAllLines(_packedElement);
        _name.text = PackedElement[0];
        _name2.text = PackedElement[0];
        _description.text = File.ReadAllText(PackedElement[3]);
        _additionalInfo.text = File.ReadAllText(PackedElement[4]);
        _imageLoader.VisualizeImageOn( _image, PackedElement[2]);
        GetComponent<PageAdjuster>().InitiateAdjustment();
    }
}
