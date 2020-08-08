using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class KnobLauncher : MonoBehaviour
    {
        private struct LaunchData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public LaunchData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
        }

        [Header("Variables")]
        [Space]
        public Rigidbody2D knob;
        public Transform Target;
        public float h = 25;
        public float gravity = -18;
        public bool DebugPath;
        private LaunchData launchData;
        private Vector2 previousDrawPoint;
        private int resolution;

        private void Start()
        {
            knob.gravityScale = 0;
        }

        private void Update()
        {
            if(Input.GetButton("Jump"))
            {
                Launch();
            }

            if(DebugPath)
            {
                DrawPath();
            }
        }

        private void OnDrawGizmos()
        {
            if(DebugPath)
            {
                DrawPath();
            }
        }

        private void Launch()
        {
            Physics2D.gravity = Vector2.up * gravity;
            knob.gravityScale = 1;
            knob.velocity = CalculateLaunchData().initialVelocity;
        }

        private void DrawPath()
        {
            launchData = CalculateLaunchData();
            previousDrawPoint = knob.position;
            resolution = 30;

            for(int i = 0; i <= resolution; i++)
            {
                var simulationTime = i / (float)resolution * launchData.timeToTarget;
                var displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
                var drawPoint = knob.position + new Vector2( displacement.x, displacement.y);
                Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);

                previousDrawPoint = drawPoint;
            }
        }

        private LaunchData CalculateLaunchData()
        {
            var displacementY = transform.position.y - knob.position.y;
            var displacementXZ = new Vector3(
                InputManager.Instance.MouseWorldPosition.x - knob.position.x,
                0,
                 InputManager.Instance.MouseWorldPosition.y - knob.position.y);

            var time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

            var velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
            var velocityXZ = displacementXZ / time;

            return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        }
    }
}
