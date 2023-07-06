using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigitSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void ChangeDigit()
    {
        _text.text = _slider.value.ToString();
    }
}
