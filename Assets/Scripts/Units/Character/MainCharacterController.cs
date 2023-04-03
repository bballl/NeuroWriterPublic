using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Sounds;
using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Attack;
using Assets.Scripts.Units.Character.Movement;
using Assets.Scripts.Units.Character.PoolObjects;
using GameBoxProject;
using UnityEngine;

/// <summary>
/// ���������� ���������.
/// </summary>
public class MainCharacterController : MonoBehaviour, IDamagable
{
    private CharacterInitData _data;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private SpriteRenderer _shadowSprite;
    [SerializeField] private Animator _animator;
    
    private CharacterMovement _characterMovement;
    private CharacterAnimatorController _animatorController;
    private CharacterSoundController _soundController;

    public Health Health { get; private set; }
    public Armor Armor { get; private set; }

    public float CurrentHP => Health.Hp;

    private void Awake()
    {
        _data = SceneContent.Instance._characterData;

        _characterMovement = new CharacterMovement(_rigidbody, _weaponTransform);
        _animatorController = new CharacterAnimatorController(_animator);
        _soundController = new CharacterSoundController(transform);
        new CharacterWeaponStateController(_weaponTransform);

        CharacterAttributes.GameLevelDataInit(_data);

        Health = new Health(CharacterAttributes.Health);
        Armor = new Armor(CharacterAttributes.Armor);

        Debug.Log($"�������� CurrentHP �� ������: {CurrentHP}");
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void OnDestroy()
    {
        Debug.Log("��������� ������!");
        Observer.GameLevelFinishedEvent?.Invoke();
    }

    /// <summary>
    /// �������� ��������� � ��� ������.
    /// </summary>
    private void Move()
    {
        float horizontalDirection = _characterMovement.Move();

        if (horizontalDirection == 0 ) 
        {
            _animatorController.ChangeAnimation(false);
        }
        else
            _animatorController.ChangeAnimation(true);

        SetSpriteDirection(horizontalDirection);
    }

    /// <summary>
    /// ��������� �����.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (CharacterAttributes.IsInvulnerAbilityActivated)
        {
            Debug.Log("�������� ��������. ��������� ����������� Invulnerability. ���� = 0");
            return;
        }
        
        float resultDamage = damage - Armor.CurrentArmor;

        if(resultDamage > 0)
        {
            Health.TakeDamage(resultDamage);
            _soundController.TakingDamage();
            Debug.Log($"������� ����: {resultDamage}");
        }
    }

    /// <summary>
    /// ������������� ����������� ���������� ������� � ����������� �� ����������� �������� ���������.
    /// </summary>
    private void SetSpriteDirection(float direction)
    {
        if (direction > 0)
        {
            _characterSprite.flipX = false;
            _shadowSprite.flipX = false;
        }
            

        if (direction < 0)
        {
            _characterSprite.flipX = true;
            _shadowSprite.flipX = true;
        }
    }
}
