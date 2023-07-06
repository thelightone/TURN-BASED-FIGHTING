using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Buff : MonoBehaviour
{
    public bool armorDecrOn;
    public bool vampDecrOn;

    public TMP_Text _countSlot1;
    public TMP_Text _countSlot2;

    [SerializeField]
    private RoundManager _roundManager;
    [SerializeField]
    private TMP_Text _buffSlot2;
    [SerializeField]
    private TMP_Text _buffSlot1;

    private float _attackBuffEnd;
    private float _armorBuffEnd;
    private float _destr2BuffEnd;
    private float _vampSBuffEnd;
    private float _vampDBuffEnd;
    private float _prevArmor;

    private bool _buffClicked;

    private UnitController _unitContr;

    private List<string> _buffs = new List<string>
        {
        "Double damage",
        "Armor self",
        "Armor destruction",
        "Vampirism self",
        "Vampirism decrease"
        };

    void Start()
    {
        _buffClicked = false;
        _unitContr = GetComponent<UnitController>();
    }

    public void ClickBuff()
    {
        if (!_buffClicked && _buffs.Count > 3)
        {
            _buffClicked = true;
            StartCoroutine(_unitContr.ChangeColor(0, 0.2f, 0, _unitContr._ownMaterial));

            ChooseBuff();
        }
    }

    public void NewRoundBuff()
    {
        _buffClicked = false;
    }

    public void CancelBuff()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (_roundManager._round)
            {
                case var value when value == _attackBuffEnd:
                    _attackBuffEnd = 0;
                    _unitContr._damage /= 2;
                    _buffs.Add("Double damage");
                    UnshowBuff("Double damage");
                    break;
                case var value when value == _armorBuffEnd:
                    _armorBuffEnd = 0;
                    _unitContr._armorSlider.value -= 50;
                    _buffs.Add("Armor self");
                    UnshowBuff("Armor self");
                    break;
                case var value when value == _destr2BuffEnd:
                    _destr2BuffEnd = 0;
                    armorDecrOn = false;
                    _buffs.Add("Armor destruction");
                    UnshowBuff("Armor destruction");
                    break;
                case var value when value == _vampSBuffEnd:
                    _vampSBuffEnd = 0;
                    _unitContr._armorSlider.value = _prevArmor;
                    _unitContr._vampSlider.value -= 50;
                    _buffs.Add("Vampirism self");
                    UnshowBuff("Vampirism self");
                    break;
                case var value when value == _vampDBuffEnd:
                    _vampDBuffEnd = 0;
                    vampDecrOn = false;
                    _buffs.Add("Vampirism decrease");
                    UnshowBuff("Vampirism decrease");
                    break;
            }
        }
    }

    private void DoubleDamage()
    {
        _unitContr._damage *= 2;
        _attackBuffEnd = _roundManager._round + 3;
    }

    private void ArmorSelf()
    {
        _unitContr._armorSlider.value += 50;
        _armorBuffEnd = _roundManager._round + 2;
    }

    private void ArmorDestruction()
    {
        armorDecrOn = true;
        _destr2BuffEnd = _roundManager._round + 2;
    }

    private void VampirismSelf()
    {
        _prevArmor = _unitContr._armorSlider.value;
        _unitContr._armorSlider.value -= 25;
        _unitContr._vampSlider.value += 50;
        _vampSBuffEnd = _roundManager._round + 1;
    }

    private void VampirismDecrease()
    {
        vampDecrOn = true;
        _vampDBuffEnd = _roundManager._round + 2;
    }

    private void ChooseBuff()
    {
        switch (_buffs[Random.Range(0, _buffs.Count)])
        {
            case "Double damage":
                DoubleDamage();
                ShowBuff("Double damage", _attackBuffEnd);
                _buffs.Remove("Double damage");
                break;
            case "Armor self":
                ArmorSelf();
                ShowBuff("Armor self", _armorBuffEnd);
                _buffs.Remove("Armor self");
                break;
            case "Armor destruction":
                ArmorDestruction();
                ShowBuff("Armor destruction", _destr2BuffEnd);
                _buffs.Remove("Armor destruction");
                break;
            case "Vampirism self":
                VampirismSelf();
                ShowBuff("Vampirism self", _vampSBuffEnd);
                _buffs.Remove("Vampirism self");
                break;
            case "Vampirism decrease":
                VampirismDecrease();
                ShowBuff("Vampirism decrease", _vampDBuffEnd);
                _buffs.Remove("Vampirism decrease");
                break;
        }
    }

    private void ShowBuff(string text, float endRound)
    {
        if (_buffSlot1.text == "")
        {
            _buffSlot1.text = text;
            _countSlot1.text = (endRound - _roundManager._round).ToString();
        }
        else
        {
            _buffSlot2.text = text;
            _countSlot2.text = (endRound - _roundManager._round).ToString();
        }
    }

    private void UnshowBuff(string text)
    {
        if (_buffSlot1.text == text)
        {
            _buffSlot1.text = "";
            _countSlot1.text = "";
        }

        if (_buffSlot2.text == text)
        {

            _buffSlot2.text = "";
            _countSlot2.text = "";
        }
    }
}
