using System;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class WeaponFactory : IWeaponFactory
{
    private DiContainer _container;

    public WeaponFactory(DiContainer container)
    {
        _container = container;
    }

    public T CreateObject<T, TArgs>(TArgs args)
        where T : Object
        where TArgs : IFactoryArguments
    {
        if (args is not WeaponSpawnArguments spawnArgs)
            throw new ArgumentException("Invalid arguments provided for WeaponFactory.");

        T weaponPrefabT = (T)(object)spawnArgs.Config.Prefab;

        Weapon weapon = GameObject.Instantiate(weaponPrefabT) as Weapon;

        if (weapon == null)
            throw new ArgumentNullException("Weapon is Null!");

        _container.Inject(weapon);

        weapon.transform.SetParent(spawnArgs.Parent.transform);

        weapon.transform.localPosition = spawnArgs.SpawnPosition;
        weapon.transform.localRotation = spawnArgs.SpawnRotation;

        return (T)(object)weapon;
    }
}
