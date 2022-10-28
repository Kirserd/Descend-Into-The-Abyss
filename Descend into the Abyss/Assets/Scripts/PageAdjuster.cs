using UnityEngine;
using UnityEngine.UI;
public class PageAdjuster : MonoBehaviour
{
    [SerializeField] RectTransform _descriptionBlock, _additionalInfoBlock, _descriptionText;
    [SerializeField] RectTransform _content;

    [SerializeField] private float _scaler;

    private void Update() => InitiateAdjustment();

    public void InitiateAdjustment()
    {
        _descriptionText.gameObject.GetComponent<ContentSizeFitter>().SetLayoutVertical();

        float DescriptionTextHeight = _descriptionText.sizeDelta.y * _scaler;
        float PositionShift =  DescriptionTextHeight - 810;

        _additionalInfoBlock.anchoredPosition = _descriptionBlock.anchoredPosition - new Vector2(0, PositionShift);
        _additionalInfoBlock.anchoredPosition = new Vector2(0, _additionalInfoBlock.anchoredPosition.y);
    }
}
