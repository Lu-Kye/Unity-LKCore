using UnityEngine;
using System.Collections;
using UnityEditor;

public static class ConfigEditorMenu
{
	[MenuItem("LKCore/Config/GenAll")]
	public static void GenAll()
	{
		ResourceEditorGenConfig.GenResourceConfig();
		ConfigEditor.GenConfigManagerAuto();
	}

	[MenuItem("LKCore/Config/GenConfigManagerAuto")]
	public static void GenConfigManagerAuto()
	{
		ConfigEditor.GenConfigManagerAuto();
	}
}
