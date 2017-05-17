using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 类名 : 管理控制
/// 作者 : Canyon
/// 日期 : 2017-05-15 21:53
/// 功能 : 
/// </summary>
public class MgrCtrls  {

	/// <summary>
	/// 管理代码的名称
	/// </summary>
	public class Names
	{
		public const string ABMgr = "ABMgr";
		public const string WWWMgr = "WWWMgr";
	}

	/// <summary>
	/// 单例
	/// </summary>
	private static MgrCtrls _instance;

	static public MgrCtrls instance{
		get{ 
			if (_instance == null) {
				_instance = new MgrCtrls ();
			}
			return _instance;
		}
	}

	protected MgrCtrls (){
	}

	GameObject m_gobjMgr;
	/// <summary>
	/// 绑定脚本需要的GameObject对象
	/// </summary>
	public GameObject gobjMgr{
		get{ 
			if (m_gobjMgr == null) {
				m_gobjMgr = GameObject.Find ("MgrCtrls");
				if (m_gobjMgr == null) {
					m_gobjMgr = new GameObject("MgrCtrls");
				}
			}
			return m_gobjMgr;
		}
	}

	/// <summary>
	/// 缓存绑定了的管理组件脚本
	/// </summary>
	static Dictionary<string, object> m_mgrs = new Dictionary<string, object>();

	/// <summary>
	/// 添加Unity对象
	/// </summary>
	public T AddMgr<T>(string typeName) where T : Component {
		object result = null;
		m_mgrs.TryGetValue(typeName, out result);
		if (result != null) {
			return (T)result;
		}
		Component c = gobjMgr.AddComponent<T>();
		m_mgrs.Add(typeName, c);
		return default(T);
	}

	/// <summary>
	/// 获取系统管理器
	/// </summary>
	public T GetMgr<T>(string typeName) where T : class {
		if (!m_mgrs.ContainsKey(typeName)) {
			return default(T);
		}
		object manager = null;
		m_mgrs.TryGetValue(typeName, out manager);
		return (T)manager;
	}

	/// <summary>
	/// 删除管理器
	/// </summary>
	public void RemoveMgr(string typeName) {
		if (!m_mgrs.ContainsKey(typeName)) {
			return;
		}
		object manager = null;
		m_mgrs.TryGetValue(typeName, out manager);
		Type type = manager.GetType();
		if (type.IsSubclassOf(typeof(MonoBehaviour))) {
			GameObject.Destroy((Component)manager);
		}
		m_mgrs.Remove(typeName);
	}
}
