using UnityEngine;
using System.Collections;
using LKBehaviorTree;

public class BehaviorTreeExample : MonoBehaviour 
{
	BehaviorTree tree = new BehaviorTree();

	void Start()
	{
		// Add nodes
		this.tree.PushNode(new TaskLog("TestLog"));

		// Start tree
		this.tree.Run();
	}

	void Update()
	{
		
	}
}
