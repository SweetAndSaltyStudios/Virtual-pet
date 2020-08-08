using System.Collections;
using UnityEngine;

public enum AI_STATE
{
    IDLE,
    ROAM,
    TAG,
    HIDE_AND_SEEK
}

namespace Sweet_And_Salty_Studios
{
    public class AISystem : MonoBehaviour
    {
        private AI_STATE currentAIState;
        private Vector2 direction;
        private Vector2 destination;
        private bool drawDestination;
        private bool isGrounded;
        private Transform target;

        private bool shouldMove;
        private readonly float stopRange = 0.5f;
        private Rigidbody2D rb2D;
        private CircleCollider2D triggerCollider2D = null;

        [SerializeField]
        private LineRenderer lineRenderer;

        private bool hasInitialized;

        #region PROPERTIES

        public StateMachine<AISystem> StateMachine
        {
            get;
            private set;
        }

        public Vector2 GetCurrentPosition
        {
            get
            {
                return transform.position;
            }
        }
       
        #endregion PROPERTIES

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            var colliders = GetComponents<Collider2D>();
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].isTrigger)
                {
                    triggerCollider2D = colliders[i] as CircleCollider2D;
                    break;
                }
            }

            StateMachine = new StateMachine<AISystem>(this);

            hasInitialized = true;
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => hasInitialized);

            lineRenderer.enabled = false;
            StateMachine.ChangeState(IdleState.Instance);
        }

        private void OnDrawGizmos()
        {
            if(triggerCollider2D != null)
            {
                Gizmos.DrawWireSphere(transform.position, triggerCollider2D.radius);
            }

            if(drawDestination)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(target == null ? destination : (Vector2)target.position, stopRange);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch(collision.gameObject.layer)
            {
                case 8:

                    var interactable = collision.gameObject.GetComponent<Item>();

                    if(interactable is Ball)
                    {
                        target = interactable.transform;
                        //ChangeAIState(AI_STATE.TAG);
                    }

                    if(interactable is MagnifyGlass)
                    {
                        
                    }

                    break;

                default:

                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(target != null && collision.gameObject.transform == target)
            {
                target = null;
                //ChangeAIState(AI_STATE.IDLE);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch(collision.gameObject.layer)
            {           
                case 8:

                    var item = collision.gameObject.GetComponent<Item>();

                    if(item is IConsumable)
                    {
                        item.GetComponent<IConsumable>().Consume();
                    }

                    if(item is Ball)
                    {
                        item.OnInteract();
                    }

                break;

                case 9:

                    isGrounded = true;

                break;

                default:

                    break;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // 9 is Ground layer
            if(collision.gameObject.layer == 9)
            {
                isGrounded = false;
            }
        }

        private void FixedUpdate()
        {
            if(shouldMove)
            {
                direction = (destination - GetCurrentPosition).normalized;
                AddForce(new Vector2( direction.x * Time.fixedDeltaTime * 100, 0), ForceMode2D.Force);

              

            }
        }

        private void LateUpdate()
        {
            if(shouldMove)
            {
                drawDestination = true;
                lineRenderer.SetPosition(1, destination);
                lineRenderer.SetPosition(0, rb2D.position);
            }
        }

        private void AddForce(Vector2 direction, ForceMode2D forceType)
        {
            rb2D.AddForce(new Vector2(direction.x, 0), forceType);
        }

        //public void ChangeAIState(AI_STATE newState)
        //{
        //    currentAIState = newState;

        //    switch(currentAIState)
        //    {
        //        case AI_STATE.IDLE:

        //            StopCoroutine(IIdle());
        //            StartCoroutine(IIdle());

        //            break;

        //        case AI_STATE.ROAM:

        //            StopCoroutine(IRoam());
        //            StartCoroutine(IRoam());               

        //            break;

        //        case AI_STATE.TAG:

        //            StopCoroutine(ITag());
        //            StartCoroutine(ITag());

        //            break;


        //        case AI_STATE.HIDE_AND_SEEK:

        //            StopCoroutine(IHideAndSeek());
        //            StartCoroutine(IHideAndSeek());

        //            break;

        //        default:

        //            break;
        //    }

        //    UIManager.Instance.UpdateDebugText(currentAIState.ToString());
        //}

        //private IEnumerator IIdle()
        //{
        //    yield return new WaitForSeconds(Random.Range(2, 4));

        //    ChangeAIState(AI_STATE.ROAM);
        //}

        //private IEnumerator IRoam()
        //{
        //    while(currentAIState == AI_STATE.ROAM)
        //    {
        //        destination = new Vector2(GameManager.Instance.GetRandomPosition().x, rb2D.position.y);
             
        //        shouldMove = true;
        //        lineRenderer.enabled = true;

        //        yield return new WaitUntil(() => Mathf.Abs(rb2D.position.x - destination.x) <= stopRange);

        //        shouldMove = false;
        //        lineRenderer.enabled = false;
        //        drawDestination = false;

        //        yield return new WaitUntil(() => rb2D.velocity.magnitude < 2f);

        //        yield return new WaitForSeconds(Random.Range(2, 4));
        //    }

           
        //}

        //private IEnumerator ITag()
        //{
        //    while(target != null)
        //    {
        //        destination = target.position;

        //        shouldMove = true;
        //        lineRenderer.enabled = true;


        //        yield return null;
        //    }

        //    shouldMove = false;
        //    lineRenderer.enabled = false;
        //    drawDestination = false;
        //}

        //private IEnumerator IHideAndSeek()
        //{
        //    while(target != null)
        //    {


        //        yield return null;
        //    }
        //}

        
    }
}