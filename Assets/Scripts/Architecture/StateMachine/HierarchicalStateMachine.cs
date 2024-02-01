
namespace LobaApps.Architecture.State
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class HierarchicalStateMachine<EStateMachine> : IHierarchicalStateMachine<EStateMachine>
        where EStateMachine : Enum
    {
        public abstract IStateMachine Factory(EStateMachine type);

        public IDictionary<EStateMachine, IStateMachine> Cache { get; }
        public EStateMachine CurrentKey { get; private set; }
        public IStateMachine Current { get; private set; }
        public ISet<IStateTransition<EStateMachine>> Transitions { get; }

        private readonly EStateMachine InitialStateMachineKey;

        public HierarchicalStateMachine(EStateMachine initialStateMachine)
        {
            Cache = new Dictionary<EStateMachine, IStateMachine>();
            Transitions = new HashSet<IStateTransition<EStateMachine>>();

            InitialStateMachineKey = initialStateMachine;
        }

        public virtual void Start()
        {
            // Debug.Log($">>>> [Enter] {GetType().Name}");
            if (TryGetCachedOrBuild(InitialStateMachineKey, out IStateMachine machine))
            {
                CurrentKey = InitialStateMachineKey;
                Current = machine;
                Current.Start();
            }
        }

        public virtual void Update()
        {
            if (TryGetTransition(out IStateTransition<EStateMachine> transition))
            {
                StartTransition(transition);
            }

            Current.Update();
        }

        public virtual void Exit()
        {
            Current.Exit();
            // Debug.Log($"<<<< [Exit] {GetType().Name}");
        }

        public void StartTransition(IStateTransition<EStateMachine> transition)
        {
            if (CurrentKey.Equals(transition.Target))
                return;

            if (TryGetCachedOrBuild(transition.Target, out IStateMachine machine))
            {
                Current.Exit();
                CurrentKey = transition.Target;
                Current = machine;
                Current.Start();
            }
        }

        private bool TryGetCachedOrBuild(EStateMachine initialStateMachineKey, out IStateMachine machine)
        {
            if (Cache.TryGetValue(initialStateMachineKey, out machine))
                return true;

            try
            {
                machine = Factory(initialStateMachineKey);
                Cache.Add(initialStateMachineKey, machine);

                return true;
            }
            catch (Exception)
            {
                Debug.LogError($"State {initialStateMachineKey} not found in cache and cannot be built");
                return false;
            }
        }

        private bool TryGetTransition(out IStateTransition<EStateMachine> transitionMatch)
        {
            foreach (IStateTransition<EStateMachine> transition in Transitions)
                if (transition.Condition.Evaluate())
                {
                    transitionMatch = transition;
                    return true;
                }

            transitionMatch = null;
            return false;
        }
    }
}