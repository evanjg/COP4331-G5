using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IItemActivator {

	// Singleton design pattern
	public static Player instance;

	// 
	public PlayerData playerData = new PlayerData();

	//public InventoryPanel inventoryPanel;

	public ItemCount[] startingItems;

	private Vector3 savedPosition;
	public GameObject avatar;

	public static float SAVE_TIME = 10.0f;
	public float saveTimer = 10.0f;

	// Singleton design pattern
	void Awake() {
		if (instance) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}

		if (!playerData.loaded) playerData.Load();

		playerData.onExperienceChanged.AddListener(playerData.UpdateStats);
		playerData.onLoaded.AddListener(() => {
			Debug.Log("Loaded");
		});

	}

	// Use this for initialization
	void Start () {
		playerData.inventory = new Inventory(16);
		playerData.inventory.GiveItems(startingItems);
		//inventoryPanel.SetInventory(inventory);

		avatar = GameObject.FindWithTag("Player");
		Debug.Assert(avatar != null, "Avatar null");
		if (avatar != null && savedPosition != null) {
			Debug.Log("Loaded avatar pos");
			avatar.transform.localPosition = savedPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (avatar != null) savedPosition = avatar.transform.localPosition;

		saveTimer -= Time.deltaTime;
		if (saveTimer < 0.0) {
			saveTimer += SAVE_TIME;
			playerData.Save();
		}
	}

	public void ActivateItem(ActivatableItem item) {

	}
}
