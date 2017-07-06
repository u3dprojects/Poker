using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Core.Kernel;

/// <summary>
/// 类名 : 处理unity5.x的资源绑定
/// 作者 : Canyon
/// 日期 : 2017-04-21 15:40
/// 功能 : 1.先处理，绑定了AB的资源(修改删除)
/// </summary>
public class EL_MgrAssetNames  {

	List<ED_AssetData> m_lDatas = new List<ED_AssetData>();

	// 计算高度
	Vector2 scrollPos;
	float topDescH = 130;
	float botBtnH = 30;
	float minScrollH = 260;
	float curScrollH = 260;
	float minWidth = 260;
	float curWidth = 0;

	// 显示folder
	List<bool> m_lFodeout = new List<bool>();

	public void DrawView(float winWidth,float winHeight){
		EG_GUIHelper.FEG_BeginVArea ();

		EG_GUIHelper.FEG_HeadTitMid ("Mgr-AB资源管理",Color.blue);

		EG_GUIHelper.FG_Space(10);

		RecokenWH (winWidth, winHeight);

		if (m_lDatas.Count > 0) {
			EG_GUIHelper.FEG_BeginScroll (ref scrollPos, 0, curWidth, curScrollH);
		}

		ShowListView ();

		if (m_lDatas.Count > 0) {
			EG_GUIHelper.FEG_EndScroll ();
		}

		EG_GUIHelper.FEG_BeginH();
		if (GUILayout.Button("Init"))
		{
			InitOnes();
		}

		if (GUILayout.Button("ReInit"))
		{
			AssetDatabase.RemoveUnusedAssetBundleNames ();
			InitOnes();
		}

		if (GUILayout.Button("RemoveUnused"))
		{
			AssetDatabase.RemoveUnusedAssetBundleNames ();
		}

		if (GUILayout.Button("Refresh"))
		{
			AssetDatabase.Refresh();
		}
		EG_GUIHelper.FEG_EndH ();

		EG_GUIHelper.FEG_EndV ();

		EG_GUIHelper.FG_Space(10);
	}

	void RecokenWH(float winWidth,float winHeight)
	{
		// 100 - 是主界面顶部高度
		curScrollH = winHeight - topDescH - botBtnH;
		curScrollH = Mathf.Max(curScrollH, minScrollH);

		curWidth = winWidth;
		curWidth = Mathf.Max(curWidth, minWidth);
	}

	void InitOnes(){
		m_lDatas.Clear();
		m_lFodeout.Clear ();

		string[] names = AssetDatabase.GetAllAssetBundleNames ();
		if (names == null || names.Length <= 0)
			return;
		
		int lens = names.Length;
		string abName = "";
		for (int i = 0; i < lens; i++) {
			abName = names [i];
			m_lDatas.Add (new ED_AssetData (abName));
		}
	}

	void ShowListView(){
		int lens = m_lDatas.Count;
		if (lens > 0) {
			for (int i = 0; i < lens; i++) {
				if (m_lFodeout.Count <= i) {
					m_lFodeout.Add (false);
				}
				ShowOneDataFolder (m_lDatas[i],i);
				// ShowOneData(m_lDatas[i]);
			}
		} else {
			m_lFodeout.Clear();
		}
	}

	void ShowOneDataFolder(ED_AssetData data,int index){
		EG_GUIHelper.FEG_BeginVAsArea ();

		EG_GUIHelper.FEG_Head(data.assetBundleName);

		EG_GUIHelper.FEG_BeginH();
		{
			 m_lFodeout[index] = EditorGUILayout.Foldout(m_lFodeout[index], data.assetBundleName);

			if (GUILayout.Button("+", EditorStyles.miniButton, GUILayout.Width(50)))
			{
				// 新增一个
			}

			 GUI.color = Color.red;
			if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.Width(50)))
			{
				data.Clear ();
				m_lDatas.Remove (data);
				m_lFodeout.RemoveAt(index);
			}
			GUI.color = Color.white;
		}
		EG_GUIHelper.FEG_EndH();

		EG_GUIHelper.FG_Space(5);

		if (m_lFodeout.Count > index && m_lFodeout[index])
		{
			ShowOneData (data);
		}

		EG_GUIHelper.FEG_EndV ();
		EG_GUIHelper.FG_Space(10);
	}

	void ShowOneData(ED_AssetData data){
		EG_GUIHelper.FEG_BeginVArea ();

		// EG_GUIHelper.FEG_Head(data.assetBundleName,true,null);

		for (int i = 0; i < data.m_lInfos.Count; i++) {
			ShowOneInfo (data,data.m_lInfos [i]);
		}
		EG_GUIHelper.FEG_EndV ();
		EG_GUIHelper.FG_Space(10);
	}

	void ShowOneInfo(ED_AssetData org,ED_AssetInfo info){
		EG_GUIHelper.FEG_BeginVAsArea ();

		EG_GUIHelper.FEG_BeginH();
		EditorGUILayout.LabelField ("资源路径:",info.assetPath);
		// info.abName = EditorGUILayout.TextField (info.abName);
		EG_GUIHelper.FEG_EndH();
		EG_GUIHelper.FG_Space(5);

		EG_GUIHelper.FEG_BeginH();
		EditorGUILayout.LabelField ("AB资源名:");
		info.abName = EditorGUILayout.TextField (info.abName);
		EG_GUIHelper.FEG_EndH();
		EG_GUIHelper.FG_Space(5);

		EG_GUIHelper.FEG_BeginH();
		EditorGUILayout.LabelField ("AB资源后缀名(可有可无):");
		info.abSuffix = EditorGUILayout.TextField (info.abSuffix);
		EG_GUIHelper.FEG_EndH();
		EG_GUIHelper.FG_Space(5);

		info.SetAssetBundleInfo ();

		if (GUILayout.Button("DeleteAssetBundleName"))
		{
			info.abName = "";
		}

		EG_GUIHelper.FEG_EndV ();
		EG_GUIHelper.FG_Space(7);
	}
}

/// <summary>
/// 类名 : 资源绑定管理
/// 作者 : Canyon
/// 日期 : 2017-07-06 09:40
/// 功能 : 
/// </summary>
public class ED_AssetData{
	/// <summary>
	/// 资源名
	/// </summary>
	public string assetBundleName = "";

	/// <summary>
	/// 列表
	/// </summary>
	public List<ED_AssetInfo> m_lInfos = new List<ED_AssetInfo>();

	public ED_AssetData(string abName){
		this.assetBundleName = abName;
		string[] tmpPaths = AssetDatabase.GetAssetPathsFromAssetBundle (abName);
		for (int i = 0; i < tmpPaths.Length; i++) {
			m_lInfos.Add(new ED_AssetInfo(tmpPaths[i]));
		}
	}

	public void Clear(){
		for (int i = 0; i < m_lInfos.Count; i++) {
			m_lInfos [i].Clear ();
		}
	}
}

/// <summary>
/// 类名 : 资源绑定信息
/// 作者 : Canyon
/// 日期 : 2017-07-06 09:40
/// 功能 : 
/// </summary>
public class ED_AssetInfo{
	/// <summary>
	/// 路径
	/// </summary>
	public string assetPath;

	/// <summary>
	/// AB资源名
	/// </summary>
	public string abName;

	/// <summary>
	/// AB资源后缀名
	/// </summary>
	public string abSuffix;

	/// <summary>
	/// 资源
	/// </summary>
	public UnityEngine.Object objAsset;

	/// <summary>
	/// 绑定信息
	/// </summary>
	public AssetImporter assetImporter;

	public ED_AssetInfo(string path){
		this.assetPath = path;
		this.objAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object> (path);
		this.assetImporter = AssetImporter.GetAtPath (path);

		this.abName = this.assetImporter.assetBundleName;
		this.abSuffix = this.assetImporter.assetBundleVariant;
	}

	/// <summary>
	/// 设置绑定信息
	/// </summary>
	public void SetAssetBundleInfo(){
		abName = abName.Trim ();
		bool isABName = !string.IsNullOrEmpty (abName);

		AssetImporter assett = this.assetImporter;
		if (isABName) {
			// 资源名
			assett.assetBundleName = abName.ToLower ();
		} else {
			assett.assetBundleName = null;
		}

		if (!isABName) {
			return;
		}

		abSuffix = abSuffix.Trim ();

		bool isABSuffix = !string.IsNullOrEmpty (abSuffix);
		if (isABSuffix) {
			// 资源后缀名
			assett.assetBundleVariant = abSuffix.ToLower ();
		} else {
			if (!string.IsNullOrEmpty (assett.assetBundleVariant)) {
				assett.assetBundleVariant = null;
			}
		}
	}

	public void Clear(){
		this.abName = "";
		this.abSuffix = "";
		SetAssetBundleInfo ();
	}
}