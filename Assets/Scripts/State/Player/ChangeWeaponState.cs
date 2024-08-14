using System.Collections;

public class ChangeWeaponState : State
{
    public override void Enter() { }

    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {
        yield return base.Exit(isForce, duration);
    }
    public override void Update() { /* Логика для смены оружия */ }
}