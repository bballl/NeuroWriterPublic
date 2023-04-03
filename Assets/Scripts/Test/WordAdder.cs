using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Ability;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class WordAdder : MonoBehaviour
    {
        [SerializeField] private WordPanel _extraDamagePanel;
        [SerializeField] private WordPanel _invulnerPanel;
        [SerializeField] private WordPanel _dronePanel;

        private Dictionary<AbilityType, WordPanel> _wordPanels = new();

        private void Start()
        {
            _wordPanels.Add(AbilityType.ExtraDamageAbility, _extraDamagePanel);
            _wordPanels.Add(AbilityType.InvulnerAbility, _invulnerPanel);
            _wordPanels.Add(AbilityType.DroneAbility, _dronePanel);

            Observer.DeactivationAbilityEvent += OnAbilityDeactivated;
            EnemyLetter.OnDead += FindDeadLetter;

            SetWordsToPanels();
        }

        private void SetWordsToPanels()
        {
            foreach (var panelAbility in _wordPanels.Keys)
            {
                SetWordToPanel(panelAbility);
            }
        }

        private void SetWordToPanel(AbilityType type)
        {
            if (WordGiver.TryGetWordNecessary(GetActiveWords(), out WordType wordType, out string text))
            {
                _wordPanels[type].SetWord(text, wordType);
            }
        }

        private List<string> GetActiveWords()
        {
            var list = new List<string>();
            foreach (var wordPanel in _wordPanels.Values)
            {
                if (wordPanel.IsActive(out string word))
                    list.Add(word);
            }

            return list;
        }

        private bool IsAnotherPanelHasWord(string word)
        {
            foreach (var panel in _wordPanels.Values)
            {
                if (panel.HasWord(word))
                    return true;
            }
            return false;
        }

        private void OnAbilityDeactivated(AbilityType type)
        {
            SetWordToPanel(type);
        }

        private void FindDeadLetter(Letter letter)
        {
            foreach (var panel in _wordPanels.Values)
            {
                panel.CheckDeadLetter(letter);
            }
        }

        private void OnDestroy()
        {
            EnemyLetter.OnDead -= FindDeadLetter;
        }
    }
}