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
            _firerateTimer <= 0.0f
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

        // validate collision
        Entity entityOnHit = ReturnEntityOnCollider(hit.collider.GetComponent<EntityColliderInfo>());

        // deal damage to enemy on collision
        if (entityOnHit != null)
        {
            entityOnHit.HP.TakeDamage(weapon.weaponStats.damage);
        }
        // expend ammo
        _currentRoundsInMagazine--;

        // set firerate timer
        _firerateTimer = 60 / weapon.weaponStats.roundsPerMinute;
        IEnumerator firerateTimer = FirerateTimerCoroutine();
        StartCoroutine(firerateTimer);

        // play visual and sound effects
        _visualSoundEffects.PlayWeaponVisualSoundEffects(hit);
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
        // set _firerateTimer as this weapon's firerate and run down the timer until it reaches 0
        // when _firerateTimer is 0, the weapon can be fired again
        while (_firerateTimer > 0.0f)
        {
            _firerateTimer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _firerateTimer = 0;

        yield return null;
    }

    public virtual void StopAttack1()
    {
        _bIsFiring = false;
    }
}
