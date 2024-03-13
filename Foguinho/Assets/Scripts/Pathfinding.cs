// using System;
// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.Tilemaps;
// public class Pathfinding : MonoBehaviour {

// 	public Transform seeker, target;
//     Tilemap collisionTileMap;
//     Tilemap pathTileMap;
//     //public bool showPath;
//     //Action<T> callback;
//     List<GridNode> gridPath;

// 	void Start() {
// 		collisionTileMap = GameObject.Find("Collisions").GetComponent<Tilemap>();
// 		seeker = transform;
//         //pathTileMap = GameObject.Find("Paths").GetComponent<Tilemap>();

//         //FindPath(seeker.position, target.position);
// 	}

//     // public List<GridNode> GetPath(Action<T> _callback) {
//     //     FindPath(seeker.position, target.position);
//     //     this.callback = _callback;
// 	// }

//     public void FindPath(/*Vector3 startPos, Vector3 targetPos,*/ Action<List<GridNode>> callBack) {
// 		Vector3 startPos = seeker.position;
// 		Vector3 targetPos = target.position;
// 		// Debug.Log("startPos - " + startPos);
// 		// Debug.Log("targetPos - " + targetPos);

// 		GridNode startNode = GetGridNode(startPos);
// 		GridNode targetNode = GetGridNode(targetPos);

// 		// Debug.Log("Starting from: " + startNode.gridX + ", " + startNode.gridY);
// 		// Debug.Log("Objective: " + targetNode.gridX + ", " + targetNode.gridY);

// 		List<GridNode> openSet = new List<GridNode>();
// 		List<GridNode> closedSet = new List<GridNode>();
// 		openSet.Add(startNode);

// 		//int gg = 0;

// 		while (openSet.Count > 0) {
// 			//gg++;
// 			GridNode node = openSet[0];

// 			//Debug.Log(gg + "° - " + node.gridX + ", " + node.gridY);

// 			for (int i = 1; i < openSet.Count; i ++) {
// 				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
// 					if (openSet[i].hCost < node.hCost)
// 						node = openSet[i];
// 				}
// 			}

// 			//Debug.Log("HMMM");

// 			openSet.Remove(node);
// 			closedSet.Add(node);
// 			//Debug.Log(closedSet);
// 			// int ifr = 0;
// 			// foreach(GridNode gy in closedSet)
// 			// {
// 			// 	Debug.Log(gg + "° - " + closedSet[ifr].gridX + ", " + closedSet[ifr].gridY + " está no closed");
// 			// 	ifr++;
// 			// }
// 			// ifr = 0;
			
// 			//só preciso alterar o check da linha de baixo
// 			if (node.gridX == targetNode.gridX && node.gridY == targetNode.gridY) {
// 				// Debug.Log("CRx,y = " + node.gridX + ", " + node.gridY);
// 				// Debug.Log("CRPx,y = " + node.parent.gridX + ", " + node.parent.gridY);
                
// 				callBack(RetracePath(startNode,node));

// 				return;
// 			}

// 			foreach (GridNode neighbour in GetNeighbours(node)) {

// 				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
// 					//Debug.Log(gg + "° - " + node.gridX + ", " + node.gridY + "foi col");
// 					continue;
// 				}

// 				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
// 				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
// 					neighbour.gCost = newCostToNeighbour;
// 					neighbour.hCost = GetDistance(neighbour, targetNode);
// 					neighbour.parent = node;

// 					//Debug.Log(neighbour.gCost);
// 					//Debug.Log(neighbour.hCost);

// 					if (!openSet.Contains(neighbour)){
// 						openSet.Add(neighbour);
// 					}
// 				}
// 			}
// 		}
// 	}

//     GridNode GetGridNode(Vector3 worldPosition)
//     {
//         Grid grid = GameObject.Find("Grid").GetComponent<Grid>();
//         Vector3Int nodePosition = grid.WorldToCell(worldPosition);

// 		GridNode node = new GridNode(!collisionTileMap.HasTile(nodePosition), grid.CellToWorld(nodePosition), nodePosition.x, nodePosition.y);

// 		return node;
//     }

//     GridNode GetGridNode(Vector3Int nodePos)
//     {
//         Grid grid = GameObject.Find("Grid").GetComponent<Grid>();
//         Vector3 worldPosition = grid.CellToWorld(nodePos);

// 		GridNode node = new GridNode(!collisionTileMap.HasTile(nodePos), worldPosition, nodePos.x, nodePos.y);
// 		return node;
//     }

// 	List<GridNode> RetracePath(GridNode startNode, GridNode endNode) {
// 		List<GridNode> path = new List<GridNode>();
// 		GridNode currentNode = endNode;

		
// 		int ki = 0;

// 		//while (currentNode.gridX != startNode.gridX && currentNode.gridY != startNode.gridY) {
// 		while (currentNode != startNode) {
// 			path.Add(currentNode);
// 			// Debug.Log("CRx,y = " + currentNode.gridX + ", " + currentNode.gridY);
// 			// Debug.Log(ki);
// 			// Debug.Log("CRPx,y = " + currentNode.parent.gridX + ", " + currentNode.parent.gridY);
// 			currentNode = currentNode.parent;
// 			ki++;
// 		}
// 		path.Reverse();

// 		return path;
        
// 		//SE QUISER VER O CAMINHO GERADO É SÓ DESCOMENTAR ESSE CÓDGIO ABAIXO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// 		// foreach(GridNode node in path)
// 		// {
// 		// 	Debug.Log("current node coordenates are x: " + node.gridX + ", y: " + node.gridY);
// 		// }
// 	}

//     // void DrawPath(List<GridNode> path)
//     // {
//     //     foreach(GridNode node in path)
//     //     {
//     //         pathTileMap.SetColor(new Vector3Int(node.gridX, node.gridY, 0), Color.red);
//     //     }
        
//     // }

// 	public List<GridNode> GetNeighbours(GridNode node) {
// 		List<GridNode> neighbours = new List<GridNode>();

// 		for (int x = -1; x <= 1; x++) {
// 			for (int y = -1; y <= 1; y++) {
// 				if (x == 0 && y == 0){
// 					continue;
// 				}

// 				int checkX = node.gridX + x;
// 				int checkY = node.gridY + y;

// 				//if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
// 				neighbours.Add(GetGridNode(new Vector3Int(checkX, checkY, 0)));
// 				//}
// 			}
// 		}

// 		return neighbours;
// 	}

// 	int GetDistance(GridNode nodeA, GridNode nodeB) {
// 		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
// 		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

// 		if (dstX > dstY)
// 			return 14*dstY + 10* (dstX-dstY);
// 		return 14*dstX + 10 * (dstY-dstX);
// 	}
// }

