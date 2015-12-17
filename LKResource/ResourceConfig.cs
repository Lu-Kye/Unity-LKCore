using System.Collections.Generic;

public class ResourceConfig
{
		public static ResourceInfo PREFAB_ROLETEST = new ResourceInfo("Prefabs/TrafficAI/RoleTest", "PREFAB_ROLETEST", "ROLETEST", "RoleTest", "", 0);
		public static ResourceInfo PREFAB_TRAFFICAIEDIT = new ResourceInfo("Prefabs/TrafficAI/TrafficAIEdit", "PREFAB_TRAFFICAIEDIT", "TRAFFICAIEDIT", "TrafficAIEdit", "", 0);


		public static ResourceInfo SHADER_TRAFFICPOINT = new ResourceInfo("Shaders/TrafficPoint", "SHADER_TRAFFICPOINT", "TRAFFICPOINT", "TrafficPoint", "", 0);
		public static ResourceInfo SHADER_TRAFFICROAD = new ResourceInfo("Shaders/TrafficRoad", "SHADER_TRAFFICROAD", "TRAFFICROAD", "TrafficRoad", "", 0);

		public static ResourceInfo JSON_VO_TRAFFIC_EDGE = new ResourceInfo("Jsons/vo_traffic_edge", "JSON_VO_TRAFFIC_EDGE", "VO_TRAFFIC_EDGE", "vo_traffic_edge", "", 0);
		public static ResourceInfo JSON_VO_TRAFFIC_NODE = new ResourceInfo("Jsons/vo_traffic_node", "JSON_VO_TRAFFIC_NODE", "VO_TRAFFIC_NODE", "vo_traffic_node", "", 0);
		public static ResourceInfo JSON_VO_TRAFFIC_ROLE = new ResourceInfo("Jsons/vo_traffic_role", "JSON_VO_TRAFFIC_ROLE", "VO_TRAFFIC_ROLE", "vo_traffic_role", "", 0);
		public static ResourceInfo JSON_VO_TRAFFIC_ROLE_SPAWN = new ResourceInfo("Jsons/vo_traffic_role_spawn", "JSON_VO_TRAFFIC_ROLE_SPAWN", "VO_TRAFFIC_ROLE_SPAWN", "vo_traffic_role_spawn", "", 0);


	private static Dictionary<string, ResourceInfo> dictResourceInfo = new Dictionary<string, ResourceInfo>();
	static ResourceConfig()
	{
			dictResourceInfo["Prefabs/TrafficAI/RoleTest"] = PREFAB_ROLETEST;
			dictResourceInfo["Prefabs/TrafficAI/TrafficAIEdit"] = PREFAB_TRAFFICAIEDIT;


			dictResourceInfo["Shaders/TrafficPoint"] = SHADER_TRAFFICPOINT;
			dictResourceInfo["Shaders/TrafficRoad"] = SHADER_TRAFFICROAD;

			dictResourceInfo["Jsons/vo_traffic_edge"] = JSON_VO_TRAFFIC_EDGE;
			dictResourceInfo["Jsons/vo_traffic_node"] = JSON_VO_TRAFFIC_NODE;
			dictResourceInfo["Jsons/vo_traffic_role"] = JSON_VO_TRAFFIC_ROLE;
			dictResourceInfo["Jsons/vo_traffic_role_spawn"] = JSON_VO_TRAFFIC_ROLE_SPAWN;


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
