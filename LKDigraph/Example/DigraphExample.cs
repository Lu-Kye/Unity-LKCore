using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DigraphExample : MonoBehaviour 
{
	void Main() 
	{
		// Graph
		var graph = new Digraph();

		// Init nodes && adjmatrix
		for (int i = 0; i < 10; i++) 
		{
			var data = new DigraphExampleData();
			data.Id = i;

			graph.AddNode(data);
		}

		this.AddEdge(graph, 0, 1, 1f);
		this.AddEdge(graph, 0, 2, 2f);
		this.AddEdge(graph, 0, 3, 3f);
		this.AddEdge(graph, 1, 2, 2f);
		this.AddEdge(graph, 3, 4, 4f);
		this.AddEdge(graph, 4, 5, 4f);
		this.AddEdge(graph, 5, 6, 4f);
		this.AddEdge(graph, 6, 7, 4f);

		List<DigraphExampleData> path;
		var dist = graph.Dijkstra(graph.GetNodeData(0), graph.GetNodeData(5), out path);

		var s = "path: ";
		path.ForEach(e => s += (e as DigraphExampleData).Id + "->");
		Debug.Log(s);

		Debug.Log("dist: " + dist);
	}

	void AddEdge(Digraph graph, int start, int end, float weight)
	{
		graph.AddEdge(graph.GetNodeData(start), graph.GetNodeData(end), weight);
	}
}
