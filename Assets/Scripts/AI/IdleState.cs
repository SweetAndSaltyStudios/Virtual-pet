using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class IdleState : State<AISystem>
    {
        private static IdleState instance;
        public static IdleState Instance
        {
            get
            {
                return instance ?? new IdleState();
            }

        }

        private IdleState()
        {
            if(instance != null)
            {
                return;
            }

            instance = this;
        }

        public override void Enter(AISystem owner)
        {
            Debug.LogWarning(owner.name + " IDLE ENTER");
        }

        public override void Execute(AISystem owner)
        {
            
        }

        public override void Exit(AISystem owner)
        {
            Debug.LogWarning(owner.name + " IDLE EXIT");
        }
    }
}