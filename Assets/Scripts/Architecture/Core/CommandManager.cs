

namespace LobaApps.Architecture.Core
{
    using System.Collections.Generic;
    using UnityEngine;

    public class CommandManager : MonoBehaviour
    {
        private IPlayerEntity playerEntity;
        private ICommand singleCommand;
        private List<ICommand> commands;

        readonly CommandInvoker commandInvoker = new();

        private void Awake()
        {
            playerEntity = GetComponent<IPlayerEntity>();
        }

        private void Start()
        {
            singleCommand = new PlayerMoveCommand(playerEntity);
            commands = new List<ICommand>
            {
                new PlayerAttackCommand(playerEntity),
                new PlayerJumpCommand(playerEntity)
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                singleCommand = new PlayerAttackCommand(playerEntity);
                ExecuteCommand(new List<ICommand> { singleCommand });
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                singleCommand = new PlayerMoveCommand(playerEntity);
                ExecuteCommand(commands);
            }
        }

        private void ExecuteCommand(List<ICommand> commands)
        {
            StartCoroutine(commandInvoker.ExecuteCommand(commands));
        }
    }
}
