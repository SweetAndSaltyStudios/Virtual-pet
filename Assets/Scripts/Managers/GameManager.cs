using System.Collections;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class GameManager : Singelton<GameManager>
    {
        #region VARIABLES

        public bool DebugBorders;

        private Transform bordersParent = null;

        private Vector2 topLeft;
        private Vector2 topRight;
        private Vector2 bottomLeft;
        private Vector2 bottomRight;

        private Camera mainCamera;

        private EdgeCollider2D borderEdgeCollider2D;
        private bool isBordersCreated;

        private ParticleSystem rainEffect;

        #endregion VARIABLES

        #region PROPERTIES

        public PetEngine Pet
        {
            get;
            private set;
        }

        private float GroundWidth;

        public Vector2 GetRandomPosition()
        {
            return new Vector2(Random.Range(bottomLeft.x, bottomRight.x), Random.Range(bottomLeft.y, bottomRight.y));
        }

        public bool IsGameRunning
        {
            get;
            set;
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            rainEffect = GetComponentInChildren<ParticleSystem>();
            mainCamera = Camera.main;
        }

        private void Start()
        {
            CreateGameBorderColliders();

            rainEffect.gameObject.SetActive(false);

            GroundWidth = Vector2.Distance(bottomRight, bottomLeft);

            StartCoroutine(IRunGame());
        }

        private void Update()
        {
            if(Input.GetButtonDown("Fire1"))
                ObjectPoolManager.Instance.Spawn(ResourceManager.Instance.EffectPrefab.gameObject);
        }

        private void OnDrawGizmos()
        {
            if(isBordersCreated == false || DebugBorders == false)
            {
                return;
            }

            Gizmos.color = Color.red;

            //Top
            Gizmos.DrawLine(topLeft, topRight);

            //Right
            Gizmos.DrawLine(bottomRight, topRight);

            //Bottom
            Gizmos.DrawLine(bottomLeft, bottomRight);

            //Left
            Gizmos.DrawLine(bottomLeft, topLeft);

        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void CreateGameBorderColliders()
        {
            if(bordersParent == null)
            {
                bordersParent = transform.GetChild(0);
            }

            if(!mainCamera.orthographic)
            {
                Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders");
                return;
            }

            bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight, mainCamera.nearClipPlane));
            topRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, mainCamera.nearClipPlane));
            bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, mainCamera.nearClipPlane));

            // add or use existing EdgeCollider2D
            if(bordersParent.GetComponent<EdgeCollider2D>() == null)
            {
                borderEdgeCollider2D = bordersParent.gameObject.AddComponent<EdgeCollider2D>();
            }
            else
            {
                borderEdgeCollider2D = bordersParent.GetComponent<EdgeCollider2D>();
            }
            
            borderEdgeCollider2D.points = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };

            isBordersCreated = true;
        }

        private Bounds OrthographicBounds(Camera camera)
        {
            var screenAspect = (float)Screen.width / Screen.height;
            var cameraHeight = camera.orthographicSize * 2;

            return new Bounds(camera.transform.position, new Vector2(cameraHeight * screenAspect, cameraHeight));
        }

        private IEnumerator IRunGame()
        {
            yield return new WaitUntil(() => IsGameRunning);

            Pet = ObjectPoolManager.Instance.Spawn(
                ResourceManager.Instance.PetPrefab
                ).GetComponent<PetEngine>();

            rainEffect.gameObject.SetActive(true);
        }

        #endregion CUSTOM_FUNCTIONS

    }
}
