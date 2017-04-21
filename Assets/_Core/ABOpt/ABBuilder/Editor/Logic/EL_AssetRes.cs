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

	BuildTarget m_buildTarget = BuildTarget.StandaloneWindows;

	public void DrawView(SerializedObject obj,SerializedProperty field){
		this.m_Object = obj;
		this.m_Property = field;

		if (m_Object == null) {
			return;
		}

		m_Object.Update ();

		EG_GUIHelper.FEG_BeginVArea ();

		EG_GUIHelper.FEG_HeadTitMid ("Build Resources",Color.magenta);
		EG_GUIHelper.FG_Space(10);

		m_buildTarget = (BuildTarget)EditorGUILayout.EnumPopup ("平台 : ",m_buildTarget);
		EG_GUIHelper.FG_Space(10);

		//开始检查是否有修改
		EditorGUI.BeginChangeCheck();

		//显示属性
		//第二个参数必须为true，否则无法显示子节点即List内容
		EditorGUILayout.PropertyField(m_Property,new GUIContent("资源所在文件夹 : "),true);

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
		BuildOnew (null);
	}

	void BuildOnew(UnityEngine.Object one){
		if (one == null) {
			EditorUtility.DisplayDialog ("Tips", "请选择来源文件夹!!!", "Okey");
			return;
		}

		System.Type typeFolder = typeof(UnityEditor.DefaultAsset);

		System.Type typeOrg = one.GetType ();

		if (typeOrg != typeFolder) {
			EditorUtility.DisplayDialog ("Tips", "来源文件不是文件夹,请选择来源文件夹!!!", "Okey");
			return;
		}

		string pathOrg = AssetDatabase.GetAssetPath (one);

		BuildProtobufFile (pathOrg);
	}

	static void BuildProtobufFile(string dir) {

		EL_Path.Init(dir);

		string protoc = "d:/protobuf-2.4.1/src/protoc.exe";
		string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

		foreach (string f in EL_Path.files) {
			string name = Path.GetFileName(f);
			string ext = Path.GetExtension(f);
			if (!ext.Equals(".proto")) continue;

			ProcessStartInfo info = new ProcessStartInfo();
			info.FileName = protoc;
			info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
			info.WindowStyle = ProcessWindowStyle.Hidden;
			info.UseShellExecute = true;
			info.WorkingDirectory = dir;
			info.ErrorDialog = true;

			UnityEngine.Debug.Log(info.FileName + " " + info.Arguments);

			Process pro = Process.Start(info);
			pro.WaitForExit();
		}
		AssetDatabase.Refresh();
	}
}
