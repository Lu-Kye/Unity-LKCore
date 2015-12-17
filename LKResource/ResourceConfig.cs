using System.Collections.Generic;
using Soul;

public class ResourceConfig
{
		public static ResourceInfo PREFAB_ROLETEST = new ResourceInfo("Prefabs/RoleTest", "PREFAB_ROLETEST", "ROLETEST", "RoleTest", "", 0);
		public static ResourceInfo PREFAB_TRAFFICAIEDIT = new ResourceInfo("Prefabs/TrafficAIEdit", "PREFAB_TRAFFICAIEDIT", "TRAFFICAIEDIT", "TrafficAIEdit", "", 0);

		public static ResourceInfo MAT_ARROW = new ResourceInfo("Material/Traffic/Arrow", "MAT_ARROW", "ARROW", "Arrow", "", 0);
		public static ResourceInfo MAT_POINTFOREDGE = new ResourceInfo("Material/Traffic/PointForEdge", "MAT_POINTFOREDGE", "POINTFOREDGE", "PointForEdge", "", 0);
		public static ResourceInfo MAT_POINTFORNODE = new ResourceInfo("Material/Traffic/PointForNode", "MAT_POINTFORNODE", "POINTFORNODE", "PointForNode", "", 0);
		public static ResourceInfo MAT_ROAD = new ResourceInfo("Material/Traffic/Road", "MAT_ROAD", "ROAD", "Road", "", 0);

		public static ResourceInfo SHADER_TRAFFICPOINT = new ResourceInfo("Shader/TrafficPoint", "SHADER_TRAFFICPOINT", "TRAFFICPOINT", "TrafficPoint", "", 0);
		public static ResourceInfo SHADER_TRAFFICROAD = new ResourceInfo("Shader/TrafficRoad", "SHADER_TRAFFICROAD", "TRAFFICROAD", "TrafficRoad", "", 0);

		public static ResourceInfo JSON_SG_TRAFFIC_EDGE = new ResourceInfo("Json/sg_traffic_edge", "JSON_SG_TRAFFIC_EDGE", "SG_TRAFFIC_EDGE", "sg_traffic_edge", "", 0);
		public static ResourceInfo JSON_SG_TRAFFIC_NODE = new ResourceInfo("Json/sg_traffic_node", "JSON_SG_TRAFFIC_NODE", "SG_TRAFFIC_NODE", "sg_traffic_node", "", 0);
		public static ResourceInfo JSON_SG_TRAFFIC_ROLE = new ResourceInfo("Json/sg_traffic_role", "JSON_SG_TRAFFIC_ROLE", "SG_TRAFFIC_ROLE", "sg_traffic_role", "", 0);
		public static ResourceInfo JSON_SG_TRAFFIC_ROLE_SPAWN = new ResourceInfo("Json/sg_traffic_role_spawn", "JSON_SG_TRAFFIC_ROLE_SPAWN", "SG_TRAFFIC_ROLE_SPAWN", "sg_traffic_role_spawn", "", 0);


	private static Dictionary<string, ResourceInfo> dictResourceInfo = new Dictionary<string, ResourceInfo>();
	static ResourceConfig()
	{
			dictResourceInfo["Prefabs/RoleTest"] = PREFAB_ROLETEST;
			dictResourceInfo["Prefabs/TrafficAIEdit"] = PREFAB_TRAFFICAIEDIT;

			dictResourceInfo["Material/Traffic/Arrow"] = MAT_ARROW;
			dictResourceInfo["Material/Traffic/PointForEdge"] = MAT_POINTFOREDGE;
			dictResourceInfo["Material/Traffic/PointForNode"] = MAT_POINTFORNODE;
			dictResourceInfo["Material/Traffic/Road"] = MAT_ROAD;

			dictResourceInfo["Shader/TrafficPoint"] = SHADER_TRAFFICPOINT;
			dictResourceInfo["Shader/TrafficRoad"] = SHADER_TRAFFICROAD;

			dictResourceInfo["Json/sg_traffic_edge"] = JSON_SG_TRAFFIC_EDGE;
			dictResourceInfo["Json/sg_traffic_node"] = JSON_SG_TRAFFIC_NODE;
			dictResourceInfo["Json/sg_traffic_role"] = JSON_SG_TRAFFIC_ROLE;
			dictResourceInfo["Json/sg_traffic_role_spawn"] = JSON_SG_TRAFFIC_ROLE_SPAWN;


	}

	/// <summary>
	/// Gets the ResourceInfo by resource path
	/// </summary>
	/// <returns>The res info.</returns>
	public static ResourceInfo GetResourceInfo(string path)
	{
			if (dictResourceInfo.ContainsKey(path)) return dictResourceInfo[path];
		return null;
	}
}
