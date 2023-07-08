using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigitSlider : MonoBehaviour
{
    private Slider _healthSlider;
    private Slider _armorSlider;
    private Slider _vampSlider;
    private TMP_Text _healthCount;
    private TMP_Text _armorCount;
    private TMP_Text _vampCount;

    [SerializeField]
    private PlayerController _player;


    void Start()
    {
        _healthSlider = gameObject.transform.GetChild(0).GetComponent<Slider>();
        _armorSlider = gameObject.transform.GetChild(1).GetComponent<Slider>();
        _vampSlider = gameObject.transform.GetChild(2).GetComponent<Slider>();
        _healthCount = _healthSlider.transform.GetChild(2).GetComponent<TMP_Text>();
        _armorCount = _armorSlider.transform.GetChild(2).GetComponent<TMP_Text>();
        _vampCount = _vampSlider.transform.GetChild(2).GetComponent<TMP_Text>();
        PlayerController._changed.AddListener(UpdateNumbers);
    }

    private void UpdateNumbers()
    {
        _healthSlider.value = _player.health;
        _armorSlider.value = _player.armor;
        _vampSlider.value = _player.vamp;

        _healthCount.text = Math.Round(_player.health).ToString();
        _armorCount.text = Math.Round(_player.armor).ToString();
        _vampCount.text = Math.Round(_player.vamp).ToString();
    }
}
