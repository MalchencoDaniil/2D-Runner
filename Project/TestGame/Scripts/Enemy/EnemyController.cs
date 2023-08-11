using UnityEngine;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Angry,
        Idle, 
        Run,
        Win
    }

    [Header("References")]
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] internal AudioSource enemySplash;
    [SerializeField] internal Color splashColor;

    [Header("Target Detection")]
    [SerializeField] private float lookDistance = 10f;
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask targetLayer;

    [Header("MovementStats")]
    [SerializeField] private float movementSpeed = 4;

    private void Awake()
    {
        int n = Random.Range(0, 2);

        if (n == 0)
            enemyState = EnemyState.Idle;
        else
            enemyState = EnemyState.Angry;
    }

    private void Update()
    {
        AnimationState();
        EnemyMove();
    }

    private void EnemyMove()
    {
        Debug.DrawRay(head.position, Vector2.left * lookDistance, Color.red);
        RaycastHit2D raycastHit = Physics2D.Raycast(head.position, Vector2.left, lookDistance, targetLayer);

        if (raycastHit && !GameManager.instance.loose)
        {
            enemyState = EnemyState.Run;

            transform.position += -transform.right * movementSpeed * Time.deltaTime;
        }
    }

    public void AnimationState()
    {
        switch (enemyState)
        {
            case EnemyState.Run:
                skeletonAnimation.AnimationName = "run";
                break;
            case EnemyState.Idle:
                skeletonAnimation.AnimationName = "idle";
                break;
            case EnemyState.Win:
                skeletonAnimation.AnimationName = "win";
                break;
            case EnemyState.Angry:
                skeletonAnimation.AnimationName = "angry";
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            GameManager.instance.Loss();

            enemyState = EnemyState.Win;
        }
    }
}