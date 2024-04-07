using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdderCannonAttack : RangedWeaponAttack
{
    [SerializeField] float maxInaccuracyAngle = 5f;
    [SerializeField] Transform[] spawnPoints;


    public override void BeginAttack()
    {
        SpawnProjectile();
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
            foreach (var projectileSpawnPoint in spawnPoints)
            {
                Vector2 angleChange = Random.insideUnitCircle * maxInaccuracyAngle;
                Quaternion rotation = Quaternion.Euler(projectileSpawnPoint.eulerAngles + new Vector3(angleChange.x, angleChange.y, 0));
                GameObject projectile = PoolManager.Instance.SpawnObjectWithLifetime(projectileID, projectileSpawnPoint.position, rotation, 10f);
                float damage = Random.Range(damageMin, damageMax);
                projectile.GetComponent<Projectile>().SetDamage(damage, Source);
                ApplyRecoil();
                SoundManager.Instance.PlaySoundAtPosition(shootSound, projectileSpawnPoint.position);
                PoolManager.Instance.SpawnObjectWithLifetime(muzzleFlash, projectileSpawnPoint.position, projectileSpawnPoint.rotation, 3f);
            }
        }
    }


}
