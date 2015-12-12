using UnityEngine;
using System.Collections;
using LKBehaviorTree;

public class BehaviorTreeExample : MonoBehaviour 
{
	public LKBehaviorTree.Tree tree;

	void Start()
	{
		// Init tree
		this.tree.PushNode(new TaskLog("Hello world"));
		this.tree.PushNode(new TaskLog("Hello world1"));
		this.tree.Init();

		Debug.Log("Init Tree");

		// Start tree
		this.tree.Run();

		Debug.Log("Run Tree");
	}
}
