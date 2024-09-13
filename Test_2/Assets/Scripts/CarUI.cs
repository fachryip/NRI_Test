using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private Text Text;
    [SerializeField] private RectTransform Box;
    [SerializeField] private RectTransform Gallery;
    [SerializeField] private RawImage Image;

    private RawImage _image2;

    private void Start()
    {
        _image2 = Instantiate(Image.gameObject, Gallery.transform).GetComponent<RawImage>();
        Image.transform.localPosition = new Vector3(-45, 0, 0);
        _image2.transform.localPosition = new Vector3(45, 0, 0);
    }

    public void SetCarInfo(int idx, CarData data)
    {
        Text.text = $"{data.name}\n\nDescription\n{data.description}\n\nGallery\n";
        string keyimage1 = string.Empty;
        string keyimage2 = string.Empty;

        if(idx == 0)
        {
            Box.sizeDelta = new Vector2(200, 350);
            Gallery.anchoredPosition = new Vector3(0, -122, 0);
            keyimage1 = "hb1";
            keyimage2 = "hb2";
        } else if(idx == 1)
        {
            Box.sizeDelta = new Vector2(200, 280);
            Gallery.anchoredPosition = new Vector3(0, -90, 0);
            keyimage1 = "sd1";
            keyimage2 = "sd2";
        }

        Image.texture = Resources.Load<Texture>($"Images/{data.name}/{keyimage1}");
        _image2.texture = Resources.Load<Texture>($"Images/{data.name}/{keyimage2}");
    }
}