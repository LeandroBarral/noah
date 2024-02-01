
namespace LobaApps.Architecture.State
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class FiniteStateMachine<EState> : IStateMachine<EState>
        where EState : Enum
    {
        public abstract IState<EState> Factory(EState stateKey);

        public IDictionary<EState, IState<EState>> Cache { get; }
        public EState CurrentKey { get; private set; }
        public IState<EState> Current { get; private set; }
        public ISet<IStateTransition<EState>> Transitions { get; }

        public EState InitialStateKey;

        public FiniteStateMachine(EState initialStateKey)
        {
            Cache = new Dictionary<EState, IState<EState>>();
            Transitions = new HashSet<IStateTransition<EState>>();

            InitialStateKey = initialStateKey;
        }

        public virtual void Start()
        {
            // Debug.Log($">>> [Enter] {GetType().Name}");
            if (TryGetCachedOrBuild(InitialStateKey, out IState<EState> state))
            {
                CurrentKey = InitialStateKey;
                Current = state;
                Current.Start();
            }
        }

        public virtual void Update()
        {
            if (TryGetTransition(out IStateTransition<EState> transition))
            {
                StartTransition(transition);
            }

            Current.Update();
        }

        public void StartTransition(IStateTransition<EState> transition)
        {
            if (CurrentKey.Equals(transition.Target))
                return;

            if (TryGetCachedOrBuild(transition.Target, out IState<EState> state))
            {
                Current.Exit();
                CurrentKey = transition.Target;
                Current = state;
                Current.Start();
            }
        }

        private bool TryGetTransition(out IStateTransition<EState> transitionMatch)
        {
            foreach (IStateTransition<EState> transition in Transitions)
                if (transition.Condition.Evaluate())
                {
                    transitionMatch = transition;
                    return true;
                }

            foreach (IStateTransition<EState> transition in Current.Transitions)
                if (transition.Condition.Evaluate())
                {
                    transitionMatch = transition;
                    return true;
                }

            transitionMatch = null;
            return false;
        }

        public virtual void Exit()
        {
            Current.Exit();
            // Debug.Log($"<<< [Exit] {GetType().Name}");
        }

        private bool TryGetCachedOrBuild(EState initialStateKey, out IState<EState> state)
        {
            if (Cache.TryGetValue(initialStateKey, out state))
                return true;

            try
            {
                state = Factory(initialStateKey);
                Cache.Add(initialStateKey, state);

                return true;
            }
            catch (Exception)
            {
                Debug.LogError($"State {initialStateKey} not found in cache and cannot be built");
                return false;
            }
        }
    }
}