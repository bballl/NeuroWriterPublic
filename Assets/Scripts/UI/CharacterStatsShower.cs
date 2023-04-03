using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Attack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsShower : MonoBehaviour
{
    [SerializeField] private RectTransform _framePrefab;

    [SerializeField] private Button _penButton;
    [SerializeField] private Button _pencilButton;
    [SerializeField] private TMP_Text _descriptionText;

    [SerializeField] private CharacterInitData _data;

    private RectTransform _frame;

    public bool IsSelected { get; private set; }

    private void Start()
    {
        IsSelected = false;
        _penButton.onClick.AddListener(ShowAboutPen);
        _pencilButton.onClick.AddListener(ShowAboutPencil);
    }

    private void ShowWeaponStats(CharacterWeaponType type)
    {
        switch(type)
        {
            case CharacterWeaponType.Pen:
                _descriptionText.text = 
                    $"ÇÄÎĞÎÂÜÅ: {CharacterAttributes.Health}" +
                    $"\nÑÊÎĞÎÑÒÜ: {CharacterAttributes.Speed}" +
                    $"\nÁĞÎÍß: {CharacterAttributes.Armor}" +
                    $"\nÑĞÅÄÍÈÉ ÓĞÎÍ: {(CharacterAttributes.PenMinDamage + CharacterAttributes.PenMaxDamage) / 2}" +
                    $"\nÂĞÅÌß ÏÅĞÅÇÀĞßÄÊÈ: {CharacterAttributes.PenRechargeTime}" +
                    $"\nÄÈÑÒÀÍÖÈß ÀÒÀÊÈ: {CharacterAttributes.PenAttackDistance}" +
                    $"\nĞÀÄÈÓÑ ÏÎĞÀÆÅÍÈß: {CharacterAttributes.PenAttackRadius}" +
                    $"\nÊÎËÈ×ÅÑÒÂÎ ÏÎĞÀÆÀÅÌÛÕ ÂĞÀÃÎÂ: {CharacterAttributes.PenTargetsNumber}";
                break;
            case CharacterWeaponType.Pencil:
                _descriptionText.text =
                    $"ÇÄÎĞÎÂÜÅ: {CharacterAttributes.Health}" +
                    $"\nÑÊÎĞÎÑÒÜ: {CharacterAttributes.Speed}" +
                    $"\nÁĞÎÍß: {CharacterAttributes.Armor}" +
                    $"\nÑĞÅÄÍÈÉ ÓĞÎÍ: {(CharacterAttributes.PencilMinDamage + CharacterAttributes.PencilMaxDamage) / 2}" +
                    $"\nÂĞÅÌß ÏÅĞÅÇÀĞßÄÊÈ: {CharacterAttributes.PencilRechargeTime}" +
                    $"\nÄÈÑÒÀÍÖÈß ÀÒÀÊÈ: {CharacterAttributes.PencilAttackDistance}" +
                    $"\nÑÈËÀ ÒÎË×ÊÀ: {CharacterAttributes.PencilImpulseValue}" +
                    $"\nÊÎËÈ×ÅÑÒÂÎ ÏÎĞÀÆÀÅÌÛÕ ÂĞÀÃÎÂ: {CharacterAttributes.PencilTargetsNumber}";
                break;
        }
    }

    private void ShowAboutPen()
    {
        SetFrameToButton(_penButton.transform);
        IsSelected = true;
        CharacterAttributes.GetWeapon(CharacterWeaponType.Pen);
        CharacterAttributes.GameLevelDataInit(_data, false);
        ShowWeaponStats(CharacterWeaponType.Pen);
    }

    private void ShowAboutPencil()
    {
        SetFrameToButton(_pencilButton.transform);
        IsSelected = true;
        CharacterAttributes.GetWeapon(CharacterWeaponType.Pencil);
        CharacterAttributes.GameLevelDataInit(_data, false);
        ShowWeaponStats(CharacterWeaponType.Pencil);
    }

    private void SetFrameToButton(Transform parent)
    {
        if (_frame == null)
            _frame = Instantiate(_framePrefab, parent);

        _frame.transform.SetParent(parent);
        _frame.anchoredPosition = Vector2.zero;
    }

    private void OnDestroy()
    {
        _penButton.onClick.RemoveListener(ShowAboutPen);
        _pencilButton.onClick.RemoveListener(ShowAboutPencil);
    }
}