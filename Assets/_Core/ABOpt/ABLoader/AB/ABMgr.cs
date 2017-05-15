using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : AssetBundle资源 管理
	/// 作者 : Canyon
	/// 日期 : 2017-05-12 21:53
	/// 功能 : 
	/// </summary>
	public class ABMgr : MonoBehaviour
	{

		private static ABMgr _instance;

		static public ABMgr instance{
			get{ 
				if (_instance == null) {
					_instance = MgrCtrls.instance.AddMgr<ABMgr> (MgrCtrls.Names.ABMgr);
					DontDestroyOnLoad (MgrCtrls.instance.gobjMgr);
				}
				return _instance;
			}
		}

		/// <summary>
		/// 是否可以释放资源(特殊模式下先不忙释放资源比如在战斗中)
		/// </summary>
		public bool m_isCanRelease{ get; set;}

		/// <summary>
		/// 首次延迟执行
		/// </summary>
		public float m_fDelay = 10.0f;

		/// <summary>
		/// 间隔多长时间执行一次
		/// </summary>
		public float m_fDuration = 5.0f;

		Dictionary<string,ABInfo> m_dicInfos = new Dictionary<string, ABInfo> ();

		List<ABInfo> m_list = new List<ABInfo>();
	
		void OnEnable(){
			InvokeRepeating ("_ReleaseFunc", m_fDelay, m_fDuration);
		}

		void OnDisable(){
			StopAllCoroutines();
			CancelInvoke();
		}

		/// <summary>
		/// 初始化资源数据
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="bundle">Bundle.</param>
		/// <param name="call">Call.</param>
		public void InitABInfo(string name,AssetBundle bundle,System.Action<string>  call){
			ABInfo info = null;
			m_dicInfos.TryGetValue (name, out info);
			if (info == null) {
				info = new ABInfo (name, bundle, call);
				m_dicInfos.Add (name, info);
			} else {
				info.Reset(name, bundle, call);
			}
		}

		/// <summary>
		/// Calculates the count. 计算使用次数
		/// </summary>
		/// <param name="name">Name. 资源名</param>
		/// <param name="isUse">true ：增加计数，false：减少计数</param>
		public void CalcCount(string name,bool isUse = true){
			ABInfo info = null;
			m_dicInfos.TryGetValue (name, out info);
			if (info != null) {
				info.m_iUseCount += isUse ? 1 : -1;
			}
		}

		/// <summary>
		/// InvokeRepeating 调用的函数
		/// </summary>
		void _ReleaseFunc(){
			ReleaseBundle ();
		}

		/// <summary>
		/// 释放
		/// </summary>
		/// <param name="isForced">true 时间未到也释放</param>
		public void ReleaseBundle(bool isForced  = false){
			if (!m_isCanRelease)
				return;
			try {
				long nowTime = System.DateTime.Now.ToFileTime();
	
				bool isRelease = false;
				m_list.Clear();
				m_list.AddRange(m_dicInfos.Values);

				ABInfo tmp = null;
				for (int i = 0; i < m_list.Count; i++) {
					tmp = m_list[i];
					isRelease = isForced || (tmp.m_iUseCount <= 0 && tmp.m_lReleaseTime  <= nowTime);
					if(isRelease){
						m_dicInfos.Remove(tmp.m_abName);
						tmp.Release();
					}
				}
			} catch (System.Exception ex) {
				Debug.LogError (ex);
			}
		}

	}
}