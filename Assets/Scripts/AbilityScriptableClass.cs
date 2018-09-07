using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability", order = 1)]
public class AbilityScriptableClass: ScriptableObject {
    public Sprite icon;
    public Actions actionName;
}
