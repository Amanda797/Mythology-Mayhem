using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MythologyMayhem
{
    [Tooltip("True if Main Hand Weapon, False if Offhand Weapon/Tool")]
    public bool MainOrOff;

    public MainHand mainHand;
    public OffHand offHand;
}
