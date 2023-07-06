using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public float _damage = 15;

    public Slider _armorSlider;
    public Slider _vampSlider;
    public Material _ownMaterial;

    [SerializeField]
    private Slider _healthEnemy;
    [SerializeField]
    private Slider _armorEnemy;
    [SerializeField]
    private Slider _vampEnemy;
    [SerializeField]
    private Slider _healthSlider;

    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private Material _enemyMaterial;
    [SerializeField]
    private AudioClip _damageSound;

    private Buff _buff;
    private AudioSource _source;
    private RoundManager _roundManager;

    void Start()
    {
        _roundManager = new RoundManager();
        _buff = GetComponent<Buff>();
        _source = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        StartCoroutine(AttackPause());
    }
    public IEnumerator ChangeColor(float r, float g, float b, Material material)
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 15; i++)
        {
            material.color = new Color(material.color.r + r, material.color.g + g, material.color.b + b);
            yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < 15; i++)
        {
            material.color = new Color(material.color.r - r, material.color.g - g, material.color.b - b);
            yield return new WaitForSeconds(0.005f);
        }
    }

    private IEnumerator AttackPause()
    {
        yield return new WaitForSeconds(0.5f);

        BuffsCheck();

        var netDamage = _damage * (1 - _armorEnemy.value / 100);
        _healthEnemy.value -= netDamage;
        _healthSlider.value += netDamage * _vampSlider.value / 100;

        _source.PlayOneShot(_damageSound);

        if (netDamage > 0)
        {
            StartCoroutine(ChangeColor(0.2f, 0, 0, _enemyMaterial));
        }

        DeathCheck();
    }

    private void DeathCheck()
    {
        if (_healthEnemy.value <= 0)
        {
            _gameOverText.SetActive(true);
            StartCoroutine(DeathPause());
        }
    }

    private IEnumerator DeathPause()
    {
        yield return new WaitForSeconds(1f);
        _roundManager.NewGame();
    }

    private void BuffsCheck()
    {
        if (_buff.vampDecrOn)
            _vampEnemy.value -= 10;
        else if (_buff.armorDecrOn)
            _armorEnemy.value -= 25;
    }
}
