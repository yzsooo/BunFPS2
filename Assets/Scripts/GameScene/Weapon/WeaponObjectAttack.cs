using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectAttack : MonoBehaviour
{
    public WeaponObject weapon;

    // external script components
    WeaponRaycast _raycast;
    WeaponObjectAttackVisualSoundEffects _visualSoundEffects;

    // fire control
    bool _bIsFiring = false;
    [SerializeField]
    float _firerateTimer = 0f;
    int _currentRoundsInMagazine;

    private void Awake()
    {
        _raycast = GetComponent<WeaponRaycast>();

        _currentRoundsInMagazine = weapon.weaponStats.roundsPerMagazine;

        _visualSoundEffects = GetComponent<WeaponObjectAttackVisualSoundEffects>();
        _visualSoundEffects.weapon = this.weapon;
    }

    public bool StartAttack1()
    {
        _bIsFiring = true;
        if (!WeaponCanFire())
        {
            return false;
        }
        AttackWithRaycast();
        return true;
        
    }

    protected virtual bool WeaponCanFire()
    {
        if (_currentRoundsInMagazine > 0 &&
            _firerateTimer <= 0
            )
        {
            return true;
        }

        return false;
    }

    protected void AttackWithRaycast()
    {
        Debug.Log("Bang");
        // do raycast
        RaycastHit hit = _raycast.GetRaycastAttack(weapon.cam);

        Debug.Log(hit.collider);

        // validate collision
        Entity entityOnHit = ReturnEntityOnCollider(hit.collider.GetComponent<EntityColliderInfo>());

        // deal damage to enemy on collision
        if (entityOnHit != null)
        {
            entityOnHit.HP.TakeDamage(weapon.weaponStats.damage);
            Debug.Log("Damaged");
        }
        // expend ammo
        _currentRoundsInMagazine--;

        // set firerate timer
        _firerateTimer = 60 / weapon.weaponStats.roundsPerMinute;
        IEnumerator firerateTimer = FirerateTimerCoroutine();
        StartCoroutine(firerateTimer);

        // play visual and sound effects
        _visualSoundEffects.PlayWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.Fire);
        _visualSoundEffects.CreateBulletHoleDecal(hit);
    }

    private Entity ReturnEntityOnCollider(EntityColliderInfo c)
    {
        if (c == null ||
            c.parentEntity == null)
        {
            return null;
        }
        return c.parentEntity;
    }

    IEnumerator FirerateTimerCoroutine()
    {
        // wait for _firerateTimer seconds and _firerateTimer as 0
        // this used to work by subtracting deltatime from _firerateTimer until it reaches 0
        yield return new WaitForSeconds(_firerateTimer);
        _firerateTimer = 0;

        yield return null;
    }

    public virtual void StopAttack1()
    {
        _bIsFiring = false;
    }
}
