
namespace LobaApps.Architecture.Core
{
    using System.Collections;


    public interface ICommand
    {
        IEnumerator Execute();
    }
}