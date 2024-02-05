namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.Core;
    using UnityEngine;

    public class Commander : MonoBehaviour
    {
        private IPlayerEntity playerEntity;
        private ICommand singleCommand;
        private List<ICommand> commands;

        readonly CommandInvoker commandInvoker = new();

        private void Awake()
        {
            if (!TryGetComponent(out IPlayerEntity playerEntity))
            {
                enabled = false;
            }
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
