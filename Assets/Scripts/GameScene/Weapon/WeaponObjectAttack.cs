using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectAttack : MonoBehaviour
{
    public WeaponObject weapon;

    // external script components
    public WeaponRaycast Raycast;
    public WeaponRaycastSpread RaycastSpread;
    WeaponObjectAttackVisualSoundEffects _visualSoundEffects;

    // fire control
    bool _bIsFiring = false;
    public bool bIsFiring
    {
        get { return _bIsFiring; }
    }
    [SerializeField]
    float _firerateTimer = 0f;
    int _currentRoundsInMagazine;

    // weapon state
    enum WeaponState { Idle, Firing, Reload };
    [SerializeField]
    WeaponState _currentWeaponState = WeaponState.Idle;


    private void Awake()
    {
        Raycast = GetComponent<WeaponRaycast>();

        RaycastSpread = GetComponent<WeaponRaycastSpread>();
        RaycastSpread.weapon = this.weapon;

        _currentRoundsInMagazine = weapon.weaponStats.roundsPerMagazine;

        _visualSoundEffects = GetComponent<WeaponObjectAttackVisualSoundEffects>();
        _visualSoundEffects.weapon = this.weapon;
    }

    public void FixedUpdate()
    {
        ProcessHUDWeaponStats();
    }

    void ProcessHUDWeaponStats()
    {
        weapon.playerAttack.pm.HUD.HUDWeaponStats.MaxAmmo = weapon.weaponStats.roundsPerMagazine;
        weapon.playerAttack.pm.HUD.HUDWeaponStats.AmmoCount = _currentRoundsInMagazine;
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
            _firerateTimer <= 0 &&
            _currentWeaponState == WeaponState.Idle
            )
        {
            return true;
        }

        return false;
    }

    protected void AttackWithRaycast()
    {
        _currentWeaponState = WeaponState.Firing;
        // do raycast
        RaycastHit hit = Raycast.GetRaycastAttack(weapon.cam);

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
        _currentWeaponState = WeaponState.Idle;
        yield return null;
    }

    public virtual void StopAttack1()
    {
        _bIsFiring = false;
    }

    public void Reload()
    {
        // Reload only if magazine is not full and can be reloaded
        if (_currentRoundsInMagazine < weapon.weaponStats.roundsPerMagazine &&
            _currentWeaponState == WeaponState.Idle)
        {
            // Reload using Coroutine
            IEnumerator reloadCoroutine = ReloadCoroutine();
            StartCoroutine(reloadCoroutine);
        }
    }

    IEnumerator ReloadCoroutine()
    {
        StartReload();
        float t = 0;
        while (t <= weapon.weaponStats.reloadTime)
        {
            t += Time.deltaTime;
            // animate HUD ammo bar
            weapon.playerAttack.pm.HUD.HUDWeaponStats.ReloadAmmoBar(t / weapon.weaponStats.reloadTime);
            yield return new WaitForEndOfFrame();
        }
        EndReload();
        yield return null;
    }

    void StartReload()
    {
        _currentWeaponState = WeaponState.Reload;
        // play animation
        _visualSoundEffects.PlayWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.ReloadIn);
    }

    void EndReload()
    {
        // Refill ammo
        _currentRoundsInMagazine = weapon.weaponStats.roundsPerMagazine;
        // play animation and change state back to idle
        _visualSoundEffects.PlayWeaponAnimation(WeaponObjectAnimationManager.weaponAnimation.ReloadOut);
        _currentWeaponState = WeaponState.Idle;
    }
}
