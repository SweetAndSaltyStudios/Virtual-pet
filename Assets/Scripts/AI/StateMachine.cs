using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public abstract class State<T>
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        public abstract void Enter(T owner);
        public abstract void Execute(T owner);
        public abstract void Exit(T owner);
    }

    public class StateMachine<T>
    {
        public T Owner
        {
            get;
            private set;
        }
        public State<T> CurrentState
        {
            get;
            private set;
        }
        public State<T> PreviousState
        {
            get;
            private set;
        }

        public StateMachine(T owner)
        {
            Owner = owner;
            CurrentState = PreviousState = null;
        }

        public void ChangeState(State<T> newState)
        {
            if(CurrentState != null)
            {
                CurrentState.Exit(Owner);
            }

            PreviousState = newState;
            CurrentState = newState;

            CurrentState.Enter(Owner);

            UIManager.Instance.UpdateDebugText(CurrentState.Name);            
        }

        public void ChangeToPreviousState()
        {
            ChangeState(PreviousState);
        }
    }
}