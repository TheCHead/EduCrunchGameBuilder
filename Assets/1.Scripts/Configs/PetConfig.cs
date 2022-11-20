using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet", menuName = "ScriptableObjects/CreatePetConfigFile", order = 3)]
public class PetConfig : ScriptableObject
{
    public Pet PetPrefab;
}
