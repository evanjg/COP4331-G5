using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="NewPet.asset")]
public class Pet : ScriptableObject {
	[Header("Flavor")]
	[Tooltip("The name of this pet species")]
	public string name;
	[Tooltip("A description of this pet species")]
	public string description;
}
