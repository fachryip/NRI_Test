using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private RectTransform Prefab;
    [SerializeField] private RectTransform Canvas;
    [SerializeField] private RectTransform Content;
    [SerializeField] private ScrollRect ScrollRect;

    private List<RectTransform> _buttonList;

    private void Awake()
    {
        _buttonList = new List<RectTransform>();
    }

    private void Start()
    {
        var rect = Prefab.GetComponent<RectTransform>();
        var size = rect.sizeDelta.y;

        var monthSize = 12;
        Content.sizeDelta = new Vector2(Content.sizeDelta.x, size * monthSize);

        for (int i = 1; i <= monthSize; i++)
        {
            var button = Instantiate(Prefab.gameObject, Content.transform);

            var text = button.transform.GetChild(0).GetComponent<Text>();
            text.text = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
            button.name = text.text;

            var buttonRect = button.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(Content.anchoredPosition.x, (i - 1) * -size);
            _buttonList.Add(buttonRect);
        }
    }

    public void OnScroll(Vector2 scrollDelta)
    {
        var boundUp = -_buttonList[0].anchoredPosition.y;
        var boundDown = -(_buttonList[_buttonList.Count - 1].anchoredPosition.y + Canvas.rect.height);
        //Debug.Log($"{scrollDelta.y} {Content.anchoredPosition.y}  {boundUp} {boundDown}");
        if (Content.anchoredPosition.y < boundUp)
        {
            var lastItem = _buttonList[_buttonList.Count - 1];
            _buttonList.RemoveAt(_buttonList.Count - 1);
            lastItem.anchoredPosition = _buttonList[0].anchoredPosition + Vector2.up * Prefab.rect.height;
            _buttonList.Insert(0, lastItem);
        }
        else if (Content.anchoredPosition.y > boundDown)
        {
            var firstItem = _buttonList[0];
            _buttonList.RemoveAt(0);
            firstItem.anchoredPosition = _buttonList[_buttonList.Count - 1].anchoredPosition - Vector2.up * Prefab.rect.height;
            _buttonList.Add(firstItem);
        }
    }
}
