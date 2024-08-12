using System.Collections;
using UnityEngine;

public class ChangeWeaponState : State
{
    public override void Enter() { }

    public override IEnumerator Exit()
    {
        yield return null;
    }
    public override void Update() { /* Логика для смены оружия */ }
}