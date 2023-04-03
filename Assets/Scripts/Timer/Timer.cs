using System;

namespace GameBoxProject
{
    public class Timer
    {
        public event Action<int> OnNewMinuteStarted;
        public event Action<string> OnTimeChanged;
        public static event Action OnTimeEnded;

        private int _minutesLeft;
        private float _secondsLeft;

        public Timer(int min, float sec)
        {
            _minutesLeft = min;
            _secondsLeft = sec;
        }

        /// <summary>
        /// ����� ��� ��������� ������� � �������
        /// </summary>
        /// <param name="seconds"></param>
        public void DecreaseTime(float seconds)
        {
            if (_secondsLeft <= seconds)                              //����� ������� ������ ������, ��� �������� �� �������
            {
                if (_minutesLeft == 0)
                {
                    OnTimeEnded?.Invoke();
                    return;
                }

                _minutesLeft--;
                OnNewMinuteStarted?.Invoke(_minutesLeft);

                _secondsLeft = 60;
                _secondsLeft -= seconds;
            }
            else                                                //�� ������� ������ 0 ������
            {
                if (_secondsLeft > seconds)                     //�� ������� ������ ������, ��� ��������� �������
                {
                    _secondsLeft -= seconds;
                }
                else                                            //�� ������� ������ ������, ��� ��������� �������
                {
                    float difference = seconds - _secondsLeft;

                    _minutesLeft--;
                    OnNewMinuteStarted?.Invoke(_minutesLeft);

                    _secondsLeft = 60;
                    _secondsLeft -= difference;
                }
            }

            OnTimeChanged?.Invoke(GetTime());
        }

        public string GetTime()
        {
            string minutesText = _minutesLeft.ToString("00");
            string secondsText = _secondsLeft.ToString("00");
            string timeText = string.Format("{0}:{1}", minutesText, secondsText);
            return timeText;
        }
    }
}