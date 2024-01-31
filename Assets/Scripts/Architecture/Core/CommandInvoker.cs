

namespace LobaApps.Architecture.Core
{
    using System.Collections;

    using System.Collections.Generic;

    public class CommandInvoker
    {
        public IEnumerator ExecuteCommand(List<ICommand> commands)
        {
            foreach (var command in commands)
            {
                yield return command.Execute();
            }
        }
    }
}
