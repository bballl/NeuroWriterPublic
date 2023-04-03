using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Units.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevButtons : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _plusButton;

    private void Start()
    {
        _resetButton.onClick.AddListener(ResetProgress);
        _plusButton.onClick.AddListener(AddMoney);
    }

    private void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    private void AddMoney()
    {
        int currentCoins = new LoadData().GetIntData(GlobalVariables.Coins);
        new SaveData(GlobalVariables.Coins, currentCoins + 1000);
    }

    private void OnDestroy()
    {
        _resetButton.onClick.RemoveListener(ResetProgress);
        _plusButton.onClick.RemoveListener(AddMoney);
    }
}
