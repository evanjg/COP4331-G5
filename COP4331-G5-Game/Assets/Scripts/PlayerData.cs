using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerData {
	public static string FILE_NAME = "playerdata.dat";

	[Header("Serialized data")]
	public Pet pet;
	public int experience = 0;
	[NonSerialized]
	public Inventory inventory;

	[NonSerialized]
	public int level = 1;
	[NonSerialized]
	private int levelBefore;
	[NonSerialized]
	public int experienceToNextLevel = 0;
	[NonSerialized]
	public float percentToNextLevel = 0.0f;
	[NonSerialized]
	public bool loaded = false;
	[NonSerialized]
	public bool loadedFromFile = false;
	[NonSerialized]
	public bool loadError = true;

	[Header("Box Opening")]
	[NonSerialized]
	public BoxItem boxType;
	[NonSerialized]
	public int score = 0;

	// Events
	[NonSerialized]
	public UnityEvent onLevelUp = new UnityEvent();
	[NonSerialized]
	public UnityEvent onExperienceChanged = new UnityEvent();
	[NonSerialized]
	public UnityEvent onLoaded = new UnityEvent();
	[NonSerialized]
	public UnityEvent onSaved = new UnityEvent();

	public PlayerData() {
		inventory = new Inventory(16);
	}

	public static int GetExperienceForNextLevel(int level) {
		return level * 150 + 150;
	}

	public static int GetTotalExperienceForLevel(int level) {
		int xp = 0;
		for (int i = 1; i <= level; i++) {
			xp += GetExperienceForNextLevel(i - 1);
		}
		return xp;
	}

	public static int GetLevel(int experience) {
		int level = 0;
		int xpRequired;
		do {
			level++;
			xpRequired = GetExperienceForNextLevel(level);
			experience -= xpRequired;
		} while (experience >= 0);
		return level;
	}

	public void AwardExperience(int amount) {
		experience += amount;
		onExperienceChanged.Invoke();
		UpdateStats();
	}

	public void UpdateStats() {
		levelBefore = level;
		level = GetLevel(experience);
		if (level > levelBefore) onLevelUp.Invoke();
		levelBefore = level;

		int totalXpThisLevel = GetTotalExperienceForLevel(level);
		int totalXpNextLevel = GetTotalExperienceForLevel(level + 1);
		percentToNextLevel = (float) (experience - totalXpThisLevel) / (float) (totalXpNextLevel - totalXpThisLevel);
		experienceToNextLevel = totalXpNextLevel - experience;
	}

	public void CopyDataFrom(PlayerData other) {
		this.experience = other.experience;
		UpdateStats();
		this.inventory = other.inventory;
	}

	public void Save() {
		Debug.Log("Saved to " + Application.persistentDataPath + "/" + FILE_NAME);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + FILE_NAME);
		bf.Serialize(file, this);
		file.Close();
		onSaved.Invoke();
	}

	public void Load() {
		if (File.Exists(Application.persistentDataPath + "/" + FILE_NAME)) {
			Debug.Log("Loading from " + Application.persistentDataPath + "/" + FILE_NAME);
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + FILE_NAME, FileMode.Open);
			PlayerData data = null;
			try {
				data = (PlayerData) bf.Deserialize(file);
			} catch (Exception) {
				loadError = true;
			}
			file.Close();

			if (data != null) CopyDataFrom(data);
			loadedFromFile = true;
		} else {
			Debug.Log("Did not find save file");
		}
		loaded = true;
		onLoaded.Invoke();
	}
}
