namespace LobaApps.Architecture.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using LobaApps.Architecture.State;

    public class TestFiniteStateMachine : FiniteStateMachine<TestFiniteStateMachine.States>
    {
        public TestFiniteStateMachine() : base(States.StateA) { }

        public override IState<States> Factory(States stateKey) => stateKey switch
        {
            States.StateA => new StateA(),
            States.StateB => new StateB(),
            _ => throw new System.NotImplementedException(),
        };

        public enum States { StateA, StateB, }

        public class StateA : IState<States>
        {
            public ISet<IStateTransition<States>> Transitions { get; } = new HashSet<IStateTransition<States>>();
            public void Exit() { }
            public void Start() { }
            public void Update() { }
        }

        public class StateB : IState<States>
        {
            public ISet<IStateTransition<States>> Transitions { get; } = new HashSet<IStateTransition<States>>{
                new Transition<States>(States.StateA, new FuncPredicate(() => true))
            };
            public void Exit() { }
            public void Start() { }
            public void Update() { }
        }
    }

    public class StateMachineTest
    {
        [Test]
        public void FiniteStateMachine_WithTransition_ShouldChangeStatesCorrectly()
        {
            var stateMachine = new TestFiniteStateMachine();
            Assert.IsNotNull(stateMachine);
            stateMachine.Start();
            Assert.AreEqual(TestFiniteStateMachine.States.StateA, stateMachine.CurrentKey);
            stateMachine
                .Transitions
                .Add(new Transition<TestFiniteStateMachine.States>(
                        TestFiniteStateMachine.States.StateB,
                        new FuncPredicate(() => true)
                    )
                );
            stateMachine.Update();
            Assert.AreEqual(TestFiniteStateMachine.States.StateB, stateMachine.CurrentKey);
            stateMachine.Update();
            Assert.AreEqual(TestFiniteStateMachine.States.StateA, stateMachine.CurrentKey);
        }
    }
}
