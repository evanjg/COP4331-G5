using System;

// Struct to represent moving peg at (x, y) by (dx, dy) holes
// Can also be used to represent a "reverse" move
[Serializable]
public struct Move {
	public int x;
	public int y;
	public int dx;
	public int dy;
	public int cost;

	public Move(int x, int y, int dx, int dy) {
		this.x = x;
		this.y = y;
		this.dx = dx;
		this.dy = dy;
		this.cost = 0;
	}
}