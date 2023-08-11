using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run, 
        Shoot, 
        Loose
    }

    public PlayerState playerState;
    public SkeletonAnimation skeletonAnimation;

    public bool canShoot = false;

    private void Start()
    {
        playerState = PlayerState.Run;
    }

    private void Update()
    {
        AnimationState();

        if (GameManager.instance.loose)
        {
            playerState = PlayerState.Loose;
            skeletonAnimation.loop = false;
        }
        else
        {
            skeletonAnimation.loop = true;
        }
    }

    public void AnimationState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                skeletonAnimation.AnimationName = "idle";
                break;
            case PlayerState.Run:
                skeletonAnimation.AnimationName = "run";
                break;
            case PlayerState.Shoot:
                skeletonAnimation.AnimationName = "shoot";
                break;
            case PlayerState.Loose:
                skeletonAnimation.AnimationName = "loose";
                break;
        }
    }
}