using UnityEngine;
using System.Collections;
using UnityEditor;

public static class ConfigEditorMenu
{
	[MenuItem("LKCore/Config/GenConfigManagerAuto")]
	public static void GenConfigManagerAuto()
	{
		ConfigEditor.GenConfigManagerAuto();
	}
}
