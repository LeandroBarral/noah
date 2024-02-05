

namespace LobaApps
{
    using System.Collections;
    using LobaApps.Architecture.Core;
    using UnityEngine;

    public abstract class PlayerCommand : ICommand
    {
        protected readonly IPlayerEntity Entity;

        public abstract IEnumerator Execute();

        protected PlayerCommand(IPlayerEntity entity)
        {
            Entity = entity;
        }
    }

    public class PlayerAttackCommand : PlayerCommand
    {
        public PlayerAttackCommand(IPlayerEntity entity) : base(entity)
        {
        }

        public override IEnumerator Execute()
        {
            Entity.Attack();
            yield return new WaitForSeconds(0);
            Entity.Animations.Idle();
        }
    }

    public class PlayerMoveCommand : PlayerCommand
    {
        public PlayerMoveCommand(IPlayerEntity entity) : base(entity)
        {
        }

        public override IEnumerator Execute()
        {
            Entity.Move();
            yield return new WaitForSeconds(0);
        }
    }

    public class PlayerJumpCommand : PlayerCommand
    {
        public PlayerJumpCommand(IPlayerEntity entity) : base(entity)
        {
        }

        public override IEnumerator Execute()
        {
            Entity.Jump();
            Entity.Animations.Landing();
            Entity.Animations.Idle();
            yield return new WaitForSeconds(0);
        }
    }
}