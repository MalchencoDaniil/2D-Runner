using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerShoot : MonoBehaviour
{
    private Player playerData;

    private float timeBulletShots;

    [SerializeField, Range(0, 3f)] private float reloadTime = 2f; // animation shoot duration!
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private LayerMask enemyLayer;

    private void Awake()
    {
        playerData = GetComponent<Player>();
    }

    private void Update()
    {
        if (playerData.canShoot)
            return;

        if (Input.GetMouseButtonDown(0) && !GameManager.instance.loose && !GameManager.instance.won)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, enemyLayer);

            Shoot(hit);
        }
    }

    private void Shoot(RaycastHit2D hit)
    {
        if (hit)
        {
            EnemyController enemyController = hit.collider.GetComponent<EnemyController>();
            AudioSource enemySplash = enemyController.enemySplash;

            StartCoroutine(ExplosionSpawn(hit, enemySplash, enemyController));

            Destroy(enemyController.gameObject, 0.54f);
        }

        playerData.canShoot = true;

        playerData.playerState = Player.PlayerState.Shoot;
        Invoke(nameof(ShootEffects), reloadTime / 4f);

        Invoke(nameof(Reload), reloadTime);
    }

    private IEnumerator ExplosionSpawn(RaycastHit2D hit, AudioSource enemySplash, EnemyController enemyController)
    {
        yield return new WaitForSeconds(reloadTime / 4f);

        explosion.startColor = enemyController.splashColor;

        Instantiate(explosion, hit.point, Quaternion.identity);
        enemySplash.Play();
    }

    private void ShootEffects()
    {
        muzzleEffect.gameObject.SetActive(true);
        shootSound.Play();
    }

    private void Reload()
    {
        playerData.canShoot = false;
    }
}