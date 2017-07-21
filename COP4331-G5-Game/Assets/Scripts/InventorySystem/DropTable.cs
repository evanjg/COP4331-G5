using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewDropTable.asset")]
public class DropTable : ScriptableObject {
	public List<DropTableEntry> entries;

	public int GetTotalWeight() {
		int weight = 0;
		foreach (DropTableEntry entry in entries) {
			weight += entry.weight;
		}
		return weight;
	}

	public ItemCount GetRandomItemCount() {
		int totalWeight = GetTotalWeight();
		int n = Random.Range(0, totalWeight);

		for (int i = 0; i < entries.Count; i++) {
			if (n <= entries[i].weight) {
				if (entries[i].entryType == DropType.Item) {
					ItemCount itemCount = new ItemCount();
					itemCount.count = Random.Range(entries[i].countMin, entries[i].countMax);
					itemCount.item = entries[i].item;
					return itemCount;
				} else if (entries[i].entryType == DropType.Table) {
					return entries[i].dropTable.GetRandomItemCount();
				}
			} else {
				n -= entries[i].weight;
			}
		}

		return null;
	}
}
