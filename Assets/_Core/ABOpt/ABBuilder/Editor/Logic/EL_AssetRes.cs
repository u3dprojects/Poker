using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Core.Kernel;

/// <summary>
/// 类名 : 编译 资源
/// 作者 : Canyon
/// 日期 : 2017-04 -21 16:16
/// 功能 : 
/// </summary>
public class EL_AssetRes{

	//序列化对象
	SerializedObject m_Object;

	//序列化属性
	SerializedProperty m_Property;

	List<UnityEngine.Object> m_list = null;

	// 平台
	BuildTarget m_buildTarget = BuildTarget.StandaloneWindows;

	// 相对目录(要打包的资源必须在该目录下)
	string m_rootRelative = "Builders";

	public void DrawView(SerializedObject obj,SerializedProperty field,List<UnityEngine.Object> list){
		this.m_Object = obj;
		this.m_Property = field;
		this.m_list = list;

		if (m_Object == null) {
			return;
		}

		m_Object.Update ();

		EG_GUIHelper.FEG_BeginVArea ();

		EG_GUIHelper.FEG_HeadTitMid ("Build Resources",Color.magenta);
		EG_GUIHelper.FG_Space(10);

		m_buildTarget = (BuildTarget)EditorGUILayout.EnumPopup ("平台 : ",m_buildTarget);
		EG_GUIHelper.FG_Space(10);

		EG_GUIHelper.FEG_BeginH ();
		{
			m_rootRelative = EditorGUILayout.TextField ("相对目录 : ", m_rootRelative);

			GUIStyle style = new GUIStyle ();
			style.normal.textColor = Color.yellow;
			EditorGUILayout.LabelField ("(资源文件夹节点里必须有个[相对目录]，不然不会Build！！！)", style);
		}
		EG_GUIHelper.FEG_EndH ();
		EG_GUIHelper.FG_Space(10);

		//开始检查是否有修改
		EditorGUI.BeginChangeCheck();

		//显示属性
		//第二个参数必须为true，否则无法显示子节点即List内容
		EditorGUILayout.PropertyField(m_Property,new GUIContent("资源文件夹 : "),true);

		//结束检查是否有修改
		if (EditorGUI.EndChangeCheck())
		{
			//提交修改
			m_Object.ApplyModifiedProperties();
		}

		EG_GUIHelper.FG_Space(10);

		if (GUILayout.Button("DoBuild"))
		{
			DoMake();
		}
		EG_GUIHelper.FEG_EndV ();

		EG_GUIHelper.FG_Space(10);
	}

	void DoMake(){
		int lens = m_Property.arraySize;
		if (lens <= 0) {
			EditorUtility.DisplayDialog ("Tips", "请选择来源文件夹!!!", "Okey");
			return;
		}

		for (int i = 0; i < lens; i++) {
			BuildOnew (m_list[i]);
		}
	}

	void BuildOnew(UnityEngine.Object one){
		if (one == null)
			return;
		
		System.Type typeFolder = typeof(UnityEditor.DefaultAsset);

		System.Type typeOrg = one.GetType ();

		if (typeOrg != typeFolder) {
			EditorUtility.DisplayDialog ("Tips", "来源文件不是文件夹!!!", "Okey");
			return;
		}
		string pathOrg = AssetDatabase.GetAssetPath (one);

		BuildProtobufFile (pathOrg);
	}

	static void BuildProtobufFile(string dir) {

		EL_Path.Init(dir);

		foreach (string f in EL_Path.files) {
			string name = Path.GetFileName(f);
			string ext = Path.GetExtension(f);
			if (!ext.Equals(".proto")) continue;
		}
		AssetDatabase.Refresh();
	}
}
