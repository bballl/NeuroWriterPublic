using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Units.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class GoldHolder : MonoBehaviour
    {
        public Gold Gold { get; private set; }

        private int _startGold = 0;

        private void Awake()
        {
            Construct();
            Timer.OnTimeEnded += Timer_OnTimeEnded;
        }

        private void Timer_OnTimeEnded()
        {
            int result = Gold.Current - _startGold;
            Observer.SaveCoinsEvent(result);
        }

        public void AddGold(int value)
        {
            Gold.AddGold(value);
        }

        private void Construct() // в будущем учесть загрузку данных
        {
            _startGold = new LoadData().GetIntData(GlobalVariables.Coins);
            Gold = new Gold(_startGold);
        }

        private void OnDestroy()
        {
            Timer.OnTimeEnded -= Timer_OnTimeEnded;
        }
    }
}