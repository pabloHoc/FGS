using System;
using System.Collections.Generic;
using FSG.Entities;

namespace FSG.AI
{
	public class Node
	{
		public string Id { get; init; }

		public string Name { get; init; } // Used to debug

		public int X { get; init; }

		public int Y { get; init; }

		public int TotalCost { get; set; }

		public int CostFromStart { get; set; }

		public int EstimatedCostToEnd { get; set; }

		public List<string> ConnectedTo { get; init; }

		public Node Parent { get; set; }
	}

	public class RegionToNodeConverter
	{
		private readonly List<Region> _regions;

		public RegionToNodeConverter(List<Region> regions)
		{
			_regions = regions;
		}

		public List<Node> GetNodes()
		{
			var nodes = new List<Node>();

			foreach (var region in _regions)
			{
				nodes.Add(new Node
				{
					Id = region.Id,
					Name =region.Name,
					X = region.X,
					Y = region.Y,
					CostFromStart = 0,
					TotalCost = 0,
					EstimatedCostToEnd = 0,
					ConnectedTo = region.ConnectedTo.ConvertAll<string>(region => region.Id)
				});
			}

			return nodes;
		}
	}

    public class Pathfinder
	{
		private readonly List<Node> _nodes;

		public Pathfinder(List<Node> nodes)
		{
			_nodes = nodes;
		}

		public List<Node> FindPath(Node start, Node end)
		{
			var openList = new List<Node>();
			var closesList = new List<Node>();
			var path = new List<Node>();

			openList.Add(start);

			while(openList.Count > 0)
			{
				var lowestIndex = 0;

				for (int i = 0; i < openList.Count; i++)
				{
					if (openList[i].TotalCost < openList[lowestIndex].TotalCost)
					{
						lowestIndex = i;
					}
				}

				var current = openList[lowestIndex];

				if (current.Id == end.Id)
				{
					var temp = current;
					path.Add(temp);

					while(temp.Parent != null)
					{
						path.Add(temp.Parent);
						temp = temp.Parent;
					}

					path.Reverse();

					return path;
				}

				openList.RemoveAt(lowestIndex);
				closesList.Add(current);

				var neighbors = _nodes.FindAll(node => current.ConnectedTo.Contains(node.Id));

				for (int i = 0; i < neighbors.Count; i++)
				{
					var neighbor = neighbors[i];

					if (!closesList.Contains(neighbor))
					{
						var possibleCost = current.CostFromStart + 1;

						if (!openList.Contains(neighbor))
						{
							openList.Add(neighbor);
						} else if (possibleCost >= neighbor.CostFromStart)
						{
							continue;
						}

						neighbor.CostFromStart = possibleCost;
						neighbor.EstimatedCostToEnd = 1; // TODO: heuristic(neighbor, end)
						neighbor.TotalCost = neighbor.CostFromStart + neighbor.EstimatedCostToEnd;
						neighbor.Parent = current;
					}
				}
			}

			return new List<Node>();
		}
	}
}

