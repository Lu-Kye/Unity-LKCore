using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Digraph 
{
	public const float Zero = 0f;
	public const float Infinity = -1f;
	public const DigraphNode NULL = null;

	#region node
	List<DigraphNode> nodeList = new List<DigraphNode>();

	Dictionary<IDigraphData, DigraphNode> nodeDict = new Dictionary<IDigraphData, DigraphNode>();

	/// <summary>
	/// Add a node.
	/// </summary>
	public void AddNode(IDigraphData data)
	{
		if (this.nodeDict.ContainsKey(data))
			return;

		var node = new DigraphNode(data);

		// Add
		this.nodeList.Add(node);
		this.nodeDict.Add(data, node);

		// Init edges
		this.edges[node] = new Dictionary<DigraphNode, float>();
	}

	public IDigraphData GetNodeData(int index)
	{
		return this.nodeList[index].Data;
	}

	DigraphNode GetNode(IDigraphData data)
	{
		if (!this.nodeDict.ContainsKey(data))
			return null;
		return this.nodeDict[data];
	}
	#endregion

	#region edge
	Dictionary<DigraphNode, Dictionary<DigraphNode, float>> edges 
		= new Dictionary<DigraphNode, Dictionary<DigraphNode, float>>();
	#endregion

	#region edge
	/// <summary>
	/// Add an edge.
	/// </summary>
	public void AddEdge(IDigraphData startData, IDigraphData endData, float weight)
	{
		var start = this.GetNode(startData);
		var end = this.GetNode(endData);
		if (start == null || end == null)
		{
			Debug.LogError("Digraph::AddEdge error start or end node not found");
			return;
		}

		this.edges[start][end] = weight;
	}

	float GetEdgeWeight(DigraphNode start, DigraphNode end)
	{
		if (!this.edges[start].ContainsKey(end))
			return Infinity;
		return this.edges[start][end];
	}
	#endregion

	public float Dijkstra<TData>(IDigraphData startData, IDigraphData endData, out List<TData> path)
		where TData : IDigraphData 
	{
		path = new List<TData>();

		// Prepare
		var start = this.GetNode(startData);
		var end = this.GetNode(endData);
		if (start == end)
			return Zero;

		// Start dijkstra
		// T
		var T = new List<DigraphNode>();
		this.GetT(ref T, start, end);
		if (!T.Contains(end))
			return Infinity;

		// Init weights && prev
		var weights = new Dictionary<DigraphNode, float>();
		// prev[vY] means: v0 -> v?... -> "prev[vY]" -> vY
		var prev = new Dictionary<DigraphNode, DigraphNode>();	
		for (int i = 0, max = T.Count; i < max; i++)
		{
			if (T[i] == start)
			{
				weights[T[i]] = Zero;
				prev[T[i]] = NULL;
			}
			else
			{
				weights[T[i]] = this.GetEdgeWeight(start, T[i]);
				prev[T[i]] = weights[T[i]] != Infinity ? start : NULL;
			}
		}

		// Init S
		var S = new List<DigraphNode>();
		var v0 = T[0];
		T.RemoveAt(0);
		S.Add(v0);

		// Iter
		while (T.Count > 0)
		{
			T.OrderBy(e => weights[e]);

			// W & Add to S
			var w = T[0];
			T.RemoveAt(0);
			S.Add(w);

			// Update after insert w
			for (int i = 0, max = T.Count; i < max; i++)
			{
				var v = T[i];
				var vWeight = weights[v];

				var edgeWeight = this.GetEdgeWeight(w, v);
				if (edgeWeight == Infinity)
					continue;

				var newWeight = weights[w] + edgeWeight;

				// Update
				if (vWeight == Infinity || vWeight > newWeight)
				{
					weights[v] = weights[w] + edgeWeight;
					prev[v] = w;
				}
			}
		}

		// Set path
		var cur = end;
		path.Insert(0, (TData)cur.Data);
		while (prev[cur] != NULL)
		{
			cur = prev[cur];
			path.Insert(0, (TData)cur.Data);
		}

		return weights[end];
	}

	void GetT(ref List<DigraphNode> T, DigraphNode start, DigraphNode end)
	{
		T.Add(start);
		if (start == end)
			return;

		foreach (var edge in this.edges[start])
		{
			this.GetT(ref T, edge.Key, end);
		}
	}
}
