using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreatorAdjusterRefresher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _desc, _info;
    [SerializeField] private TextMeshProUGUI _descInput, _infoInput;
    [SerializeField] private RectTransform _descRect, _infoRect;

    private PageAdjuster _pageAdjuster;

    private void Start() => _pageAdjuster = GameObject.FindGameObjectWithTag("Content").GetComponent<PageAdjuster>();

    public void SendTextToDesc()
    {
        _desc.text = _descInput.text;
        _pageAdjuster.InitiateAdjustment();
        DescAdjust();
    }

    public void SendTextToInfo()
    {
        _info.text = _infoInput.text;
        _pageAdjuster.InitiateAdjustment();
        InfoAdjust();
    }

    private void DescAdjust()
    {
        float RowNumber = _descInput.text.Length / 52f;

        _descRect.sizeDelta = new Vector2(_descRect.sizeDelta.x, 30 + 16 * (Mathf.RoundToInt(RowNumber))); 
    }
    private void InfoAdjust()
    {
        float RowNumber = _infoInput.text.Length / 52f;

        _infoRect.sizeDelta = new Vector2(_infoRect.sizeDelta.x, 30 + 16 * (Mathf.RoundToInt(RowNumber)));
    }
}
