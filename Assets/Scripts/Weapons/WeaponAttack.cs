using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponAttack : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnRecoil;

    public Vector3 HoldOffset => holdOffset;

    [SerializeField] protected Vector3 holdOffset;
    [SerializeField] protected float damageMin = 10f;
    [SerializeField] protected float damageMax = 20f;
    public CameraShake.Properties camShakeProperties;

    public DamageSource Source { get; set; }

    public abstract void BeginAttack();
    public abstract void DuringAttack();
    public abstract void EndAttack();
}
