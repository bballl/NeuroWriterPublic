using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _helpText;
    [field: SerializeField] public Button NextButton { get; private set; }

    public void Construct(string message)
    {
        _helpText.text = message;
        NextButton.interactable = true;
    }
}
