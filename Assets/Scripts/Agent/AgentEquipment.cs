using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentEquipment : MonoBehaviour
{
    [SerializeField] Transform weaponHoldTarget;
    [SerializeField] float weaponOffsetChangeSpeed = 1f;
    [SerializeField] DamageSource damageSource;
    [SerializeField] Transform arms;
    [SerializeField] Vector3 armsEquippedRotation;

    public event Action OnWeaponChange;

    public bool HasWeaponEquipped => CurrentWeaponGO != null;
    public bool HasTwoWeapons => PrimaryWeapon != null && SecondaryWeapon != null;
    public WeaponAttack CurrentWeaponAttack => CurrentWeapon?.attack;
    public WeaponAmmunition CurrentWeaponAmmunition => CurrentWeapon?.ammo;
    GameObject CurrentWeaponGO => CurrentWeapon?.gameObject;
    public Holdable CurrentHoldable => CurrentWeapon?.holdable;

    public Weapon CurrentWeapon { get; private set; }
    public Weapon PrimaryWeapon { get; private set; }
    public Weapon SecondaryWeapon { get; private set; }

    public class Weapon
    {
        public GameObject gameObject;
        public WeaponAnimation animation;
        public WeaponAttack attack;
        public WeaponAmmunition ammo;
        public Holdable holdable;

        public Weapon(GameObject gameObject)
        {
            this.gameObject = gameObject;
            attack = gameObject.GetComponent<WeaponAttack>();
            animation = gameObject.GetComponent<WeaponAnimation>();
            ammo = gameObject.GetComponent<WeaponAmmunition>();
            holdable = gameObject.GetComponent<Holdable>();
        }
    }

    private void Start()
    {
        WeaponAttack[] weaponAttacks = GetComponentsInChildren<WeaponAttack>();
        if (weaponAttacks.Length > 0 && weaponAttacks[0] != null)
        {
            PickupWeapon(weaponAttacks[0].gameObject);
            PrimaryWeapon = new Weapon(weaponAttacks[0].gameObject);
        }
        if (weaponAttacks.Length > 1 && weaponAttacks[1] != null)
        {
            SecondaryWeapon = new Weapon(weaponAttacks[1].gameObject);
        }
        if (PrimaryWeapon != null)
        {
            Equip(PrimaryWeapon);
        }
        if (SecondaryWeapon != null)
        {
            UnEquip(SecondaryWeapon);
        }
    }

    public void Equip(Weapon weapon)
    {
        if (weapon == null)
        {
            arms.rotation = Quaternion.identity;
            return;
        }
        CurrentWeapon = weapon;
        CurrentWeapon.gameObject.SetActive(true);
        weapon.gameObject.transform.SetParent(weaponHoldTarget);
        weapon.gameObject.transform.localPosition = weapon.attack.HoldOffset;
        CurrentWeapon.attack.Source = damageSource;
        arms.localEulerAngles = armsEquippedRotation;
        OnWeaponChange?.Invoke();
    }

    public void UnEquip(Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(false);
            weapon.gameObject.transform.localEulerAngles = Vector3.zero;
        }
    }

    public void PickupWeapon(GameObject weaponGO)
    {
        weaponGO.transform.SetParent(weaponHoldTarget);
        weaponGO.transform.position = weaponHoldTarget.position;
        weaponGO.transform.rotation = weaponHoldTarget.rotation;
        Rigidbody weaponRB = weaponGO.GetComponent<Rigidbody>();
        weaponRB.isKinematic = true;
        Weapon newWeapon = new Weapon(weaponGO);
        weaponGO.GetComponent<BoxCollider>().enabled = false;
        if (PrimaryWeapon == null)
        {
            PrimaryWeapon = newWeapon;
        }
        else if (SecondaryWeapon == null)
        {
            SecondaryWeapon = newWeapon;
        }
        if (CurrentWeapon != null)
        {
            UnEquip(CurrentWeapon);
        }
        Equip(newWeapon);
    }

    public void DropWeapon()
    {
        if (CurrentWeapon != null)
        {
            if (CurrentWeapon == PrimaryWeapon)
            {
                PrimaryWeapon = null;
            }
            else
            {
                SecondaryWeapon = null;
            }
            CurrentWeaponGO.transform.SetParent(null, true);
            CurrentWeaponGO.transform.Translate(transform.forward * .5f);
            Rigidbody weaponRB = CurrentWeaponGO.GetComponent<Rigidbody>();
            weaponRB.isKinematic = false;
            weaponRB.useGravity = true;
            CurrentWeaponGO.GetComponent<BoxCollider>().enabled = true;
            CurrentWeapon = null;
        }
    }

    public bool TrySwitchWeapon()
    {
        if (SecondaryWeapon == null)
        {
            return false;
        }
        if (CurrentWeapon == PrimaryWeapon)
        {
            UnEquip(PrimaryWeapon);
            Equip(SecondaryWeapon);
        }
        else
        {
            UnEquip(SecondaryWeapon);
            Equip(PrimaryWeapon);
        }
        return true;
    }

    public void RefillPercentAmmoAllWeapons(float percent)
    {
        if (PrimaryWeapon != null)
        {
            PrimaryWeapon.ammo.AddAmmo((int)(PrimaryWeapon.ammo.MaxCarriedAmmo * percent));
        }
        if (SecondaryWeapon != null)
        {
            SecondaryWeapon.ammo.AddAmmo((int)(SecondaryWeapon.ammo.MaxCarriedAmmo * percent));
        }
    }
}
