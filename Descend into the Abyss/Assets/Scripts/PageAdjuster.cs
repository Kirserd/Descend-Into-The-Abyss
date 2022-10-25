using UnityEngine;
using TMPro;

public class PageAdjuster : MonoBehaviour
{
    [SerializeField] RectTransform _descriptionBlock;
    [SerializeField] RectTransform _additionalInfoBlock;

    [SerializeField] TextMeshProUGUI _descriptionText;
    [SerializeField] TextMeshProUGUI _additionalInfoText;

    [SerializeField] RectTransform _content;

    public void InitiateAdjustment()
    {
        float DescriptionTextHeight = (_descriptionText.text.Length / 51.5f) * 40 + 10;
        float PositionShift =  DescriptionTextHeight - 591;

        _additionalInfoBlock.anchoredPosition = _descriptionBlock.anchoredPosition - new Vector2(0, PositionShift);
        _additionalInfoBlock.anchoredPosition = new Vector2(0, _additionalInfoBlock.anchoredPosition.y);
    }

}
