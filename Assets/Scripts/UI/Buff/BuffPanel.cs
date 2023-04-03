using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameBoxProject
{
    class BuffPanel : MonoBehaviour
    {
        [SerializeField] private BuffUiIcon _iconPrefab;

        private Dictionary<Buff, BuffUiIcon> _buffIcons = new();

        public void AddBuff(Buff buff)
        {
            if (!_buffIcons.ContainsKey(buff))
                _buffIcons.Add(buff, Instantiate(_iconPrefab, transform));

            _buffIcons[buff].Activate(buff);
        }
    }
}
