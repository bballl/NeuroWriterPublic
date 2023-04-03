using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoxProject
{
    public interface IPoolObject
    {
        event Action<IPoolObject> OnObjectNeededToDeactivate;
        void ResetBeforeBackToPool();
    }
}
