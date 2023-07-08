using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private UnitConfig _unitConfig;
    [SerializeField]
    private PlayerController _enemy;
    [SerializeField]
    private AudioClip _damageSound;
    [SerializeField]
    private SkinnedMeshRenderer _meshRenderer;

    private Animator _animator;
    private AudioSource _source;

    private float _health;
    private float _armor;
    private float _vamp;

    public  float health 
    {
        get 
        {
            return _health; 
        }
        set 
        {
            if (value > 100) _health = 100;
            else if (value < 0) _health = 0;
            else _health = value;
        } 
    }
    public float armor
    {
        get
        {
            return _armor;
        }
        set
        {
            if (value > 100) _armor = 100;
            else if (value < 0) _armor = 0;
            else _armor = value;
        }
    }
    public float vamp
    {
        get
        {
            return _vamp;
        }
        set
        {
            if (value > 100) _vamp = 100;
            else if (value < 0) _vamp = 0;
            else _vamp = value;
        }
    }

    public float _damageHealth;
    public float _damageArmor;
    public float _damageVamp;

    public static UnityEvent _changed = new UnityEvent();

    void Start()
    {
        health = _unitConfig.health;
        armor = _unitConfig.armor;
        vamp = _unitConfig.vamp;
        _damageHealth = _unitConfig.damageHealth;
        _damageArmor = _unitConfig.damageArmor;
        _damageVamp = _unitConfig.damageVamp;

        _source = GetComponent<AudioSource>();
        _animator= GetComponent<Animator>();
    }

    private void ReceiveDamage(float damage, float damageArmor, float damageVamp)
    {
        var netDamage = damage * (1 - _armor / 100);
        health -= netDamage;

        _enemy.HealthVamp(netDamage);
        _source.PlayOneShot(_damageSound);
        ColorChange();
        _changed?.Invoke();
    }

    private void HealthVamp(float netDamage)
    {
        health += netDamage * _vamp / 100;
    }

    private void ColorChange()
    {
        var mainColor = _meshRenderer.material.color;
        _meshRenderer.material.DOColor(Color.red, 0.01f);
        _meshRenderer.material.DOColor(mainColor, 0.9f);
    }

    public void Buff()
    {
        _changed?.Invoke();
    }

    public void Attack()
    {
        _animator.SetTrigger("Trigger");
        _enemy.ReceiveDamage(_damageHealth, _damageArmor, _damageVamp);
    }
}
