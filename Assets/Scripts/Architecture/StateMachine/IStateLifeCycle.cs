
using System.Collections;

namespace LobaApps.Architecture.State
{
    public interface IStateLifeCycle
    {
        void Start();
        void Update();
        void Exit();
    }
}