using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 类名 : unity 5 以上的AB资源管理 window 窗体
/// 作者 : Canyon
/// 日期 : 2017-07-06 10:30
/// 功能 : 
/// </summary>
public class ABMgrWindowEditor : EditorWindow {

	static bool isOpenWindowView = false;

	static protected ABMgrWindowEditor vwWindow = null;

	// 窗体宽高
	static public float width = 600;
	static public float height = 635;

	[MenuItem("Tools/MgrABInfosWindows")]
	static void AddWindow()
	{
		if (isOpenWindowView || vwWindow != null)
			return;

		try
		{
			isOpenWindowView = true;
			float x = 460;
			float y = 200;
			Rect rect = new Rect(x, y, width, height);

			// 大小不能拉伸
			// vwWindow = GetWindowWithRect<EDW_Skill>(rect, true, "SkillEditor");

			// 窗口，只能单独当成一个窗口,大小可以拉伸
			//vwWindow = GetWindow<EDW_Skill>(true,"SkillEditor");

			// 这个合并到其他窗口中去,大小可以拉伸
			vwWindow = GetWindow<ABMgrWindowEditor>("ABMgrWindows");

			vwWindow.position = rect;
		}
		catch (System.Exception)
		{
			OnClearSWindow();
			throw;
		}
	}

	static void OnClearSWindow()
	{
		isOpenWindowView = false;
		vwWindow = null;
	}

	void OnEnable(){
		
	}

	void OnDestroy()
	{
		OnClearSWindow();
	}

	// 在给定检视面板每秒10帧更新
	void OnInspectorUpdate()
	{
		Repaint();
	}

	#region  == Member Attribute ===

	// 逻辑管理
	EL_MgrAssetNames m_mgrABAsset = new EL_MgrAssetNames();
	#endregion

	void OnGUI(){
		EG_GUIHelper.FEG_BeginV ();
		{
			EG_GUIHelper.FEG_BeginH ();
			{
				GUIStyle style = EditorStyles.label;
				style.alignment = TextAnchor.MiddleCenter;
				string txtDecs = "类名 : AB资源管理工具\n"
					+ "作者 : Canyon\n"
					+ "日期 : 2017-07-06 10:30\n"
					+ "描述 : 主要处理绑定了abName的资源进行修改，删除(新增暂时为处理)\n";
				GUILayout.Label(txtDecs, style);
				style.alignment = TextAnchor.MiddleLeft;
			}
			EG_GUIHelper.FEG_EndH ();

			EG_GUIHelper.FG_Space(10);

			m_mgrABAsset.DrawView (this.position.width,this.position.height);
		}
		EG_GUIHelper.FEG_EndV ();
	}
}
