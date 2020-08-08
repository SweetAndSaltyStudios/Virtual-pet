using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class RoamState : State<AISystem>
    {
        private static RoamState instance;
        public static RoamState Instance
        {
            get
            {
                return instance == null ? instance : new RoamState();
            }

        }

        private RoamState()
        {
            if(instance != null)
            {
                return;
            }

            instance = this;
        }

        public override void Enter(AISystem owner)
        {
            Debug.LogWarning(owner.name + " ROAM ENTER");
        }

        public override void Execute(AISystem owner)
        {

        }

        public override void Exit(AISystem owner)
        {
            Debug.LogWarning(owner.name + " ROAM EXIT");
        }
    }
}