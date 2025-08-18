using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerUnitBuilder : IUnitBuilder
{
    private IUnitsFactory _unitFactory;
    private IWeaponFactory _weaponFactory;

    public PlayerUnitBuilder(IUnitsFactory unitFactory, IWeaponFactory weaponFactory)
    {
        _unitFactory = unitFactory;
        _weaponFactory = weaponFactory;
    }

    public T Build<T>(IFactoryArguments unitSpawnArgs) where T : Object
    {
        if (unitSpawnArgs is not UnitSpawnArguments spawnArgs)
            throw new ArgumentException("Invalid arguments provided for UnitBuilder.");

        PlayerUnit unit = _unitFactory.CreateObject<PlayerUnit, UnitSpawnArguments>(spawnArgs);

        if (unit == null)
            throw new ArgumentNullException($"PlayerUnitBuilder. unit is null!");

        T unitT = (T)(object)unit;

        if (unitT == null)
            throw new ArgumentNullException($"PlayerUnitBuilder. unitT is null!");

        Weapon weapon = GetWeapon(unit);

        return unitT;
    }

    private Weapon GetWeapon(PlayerUnit unit)
    {
        WeaponSpawnArguments weaponSpawnArguments = new WeaponSpawnArguments(
            unit.gameObject,
            unit.UnitConfig.UnitWeapon.WeaponConfig,
            unit.UnitConfig.UnitWeapon.SpawnPosition,
            unit.UnitConfig.UnitWeapon.SpawnRotation);

        Weapon weapon = _weaponFactory.CreateObject<Weapon, WeaponSpawnArguments>(weaponSpawnArguments);

        if (weapon == null)
        {
            Debug.LogError($"PlayerUnitBuilder. weapon is null!");
            return null;
        }

        return weapon;
    }
}
