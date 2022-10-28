using UnityEngine;
using TMPro;

public class CreatorAdjusterRefresher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _desc, _info;
    [SerializeField] private TextMeshProUGUI _descInput, _infoInput;
    [SerializeField] private RectTransform _descRect, _infoRect;

    private PageAdjuster _pageAdjuster;


    private void Start() => _pageAdjuster = GameObject.FindGameObjectWithTag("Content").GetComponent<PageAdjuster>();

    private void Update()
    {
        SendTextToDesc();
        SendTextToInfo();
    }
    public void SendTextToDesc()
    {
        _desc.text = _descInput.text;
        _pageAdjuster.InitiateAdjustment();
    }

    public void SendTextToInfo()
    {
        _info.text = _infoInput.text;
        _pageAdjuster.InitiateAdjustment();
    }
}