using UnityEngine;
using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : AssetBundle资源对象
	/// 作者 : Canyon
	/// 日期 : 2017-05-12 20:59
	/// 功能 : 
	/// </summary>
	public class ABInfo
	{
		/// <summary>
		/// AssetBundle资源名
		/// </summary>
		public string m_abName;

		/// <summary>
		/// asset bundle. 资源
		/// </summary>
		public AssetBundle m_assetBundle;

		/// <summary>
		/// 引用计数
		/// </summary>
		public int m_iUseCount;

		/// <summary>
		/// 最后一次使用时间
		/// </summary>
		public long m_lLastUsedTime;

		/// <summary>
		/// 释放回掉事件
		/// </summary>
		public System.Action<string> m_callRealse;

		public ABInfo ()
		{
		}

		public ABInfo (string name, AssetBundle bundle, System.Action<string> callRealse)
		{
			this.m_abName = name;
			this.m_assetBundle = bundle;
			this.m_callRealse = callRealse;

			this.m_iUseCount = 0;
			this.m_lLastUsedTime = System.DateTime.Now.ToFileTime ();
		}
	}
}