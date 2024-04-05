using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : RangedWeaponAttack
{
    [SerializeField] Vector3 spreadRotation;
    [SerializeField] Transform pump;
    [SerializeField] Vector3 pumpRestPosition;
    [SerializeField] Vector3 pumpedPosition;
    [SerializeField] float pumpStartDelay = .5f;
    [SerializeField] float pumpSpeed;

    int pelletCount = 10;
    float pumpDelay = .75f;
    float timer;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void BeginAttack()
    {
        if (timer <= 0)
        {
            SpawnProjectile();
        }
    }

    public override void DuringAttack()
    {

    }

    public override void EndAttack()
    {

    }

    void SpawnProjectile()
    {
        if (weaponAmmo.TryUseAmmo())
        {
            for (int i = 0; i < pelletCount; i++)
            {
                Vector3 eulerAngles = projectileSpawnPoint.eulerAngles;
                eulerAngles += new Vector3(Random.Range(-spreadRotation.x, spreadRotation.x), Random.Range(-spreadRotation.y, spreadRotation.y), Random.Range(-spreadRotation.z, spreadRotation.z));
                Quaternion rotation = Quaternion.Euler(eulerAngles);
                GameObject projectile = PoolManager.Instance.SpawnObjectWithLifetime(projectileID, projectileSpawnPoint.position, rotation, 10f);
                float damage = Random.Range(damageMin, damageMax);
                projectile.GetComponent<Projectile>().SetDamage(damage, Source);
            }
            ApplyRecoil();
            SoundManager.Instance.PlaySoundAtPosition(shootSound, projectileSpawnPoint.position);
            timer = pumpDelay;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
