using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    static class TextEncoder
    {
        public static Dictionary<string, bool> CryptText(this string text)
        {
            Dictionary<string, bool> resText = new Dictionary<string, bool>();
            int startIndex = 0;
            int counter = 0;
            text = text.ToLower();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals('_'))
                {
                    counter++;

                    string txt = text.Substring(startIndex, i - startIndex);

                    Debug.Log($"Create substring. From {startIndex} with length {i-startIndex}. Text={txt}. Counter={counter}");
                    startIndex = i + 1;

                    if (txt.Length == 0 || txt.Equals(" "))
                        continue;

                    resText.Add(txt, counter%2 == 0);
                }
            }
            string lastTxt = text.Substring(startIndex, text.Length - startIndex);
            if (lastTxt.Length > 0 && !lastTxt.Equals(" "))
                resText.Add(lastTxt, false);

            return resText;
        }
    }
}
