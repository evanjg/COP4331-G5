using System;
using UnityEngine;

[Serializable]
public class DropTableEntry {
	public int weight = 1;
	public int countMin = 1;
	public int countMax = 1;
	public DropType entryType = DropType.Item;
	public Item item;
	public DropTable dropTable;
}

public enum DropType {
	Item, Table
} 