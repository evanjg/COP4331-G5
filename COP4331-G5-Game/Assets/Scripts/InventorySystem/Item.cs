using UnityEngine;

[CreateAssetMenu(fileName="NewItem.asset")]
public class Item : ScriptableObject {
	public string id;
	public string nameEnglish;
	public Sprite icon;
	public int sortingIndex = 1;
	public int stackLimit = 1;
}
