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
		int _m_iUseCount;
		public int m_iUseCount{
			get{ return _m_iUseCount; }
			set{
				_m_iUseCount = value;

				// ToFileTime 的单位(0.1毫秒)
				this.m_lLastUsedTime = System.DateTime.Now.ToFileTime ();
				this.m_lReleaseTime = this.m_lLastUsedTime + m_lDurationTime * 1000 * 10;
			}
		}

		/// <summary>
		/// 最后一次使用时间
		/// </summary>
		public long m_lLastUsedTime{
			get;
			private set;
		}

		/// <summary>
		/// 可以释放的时间
		/// </summary>
		public long m_lReleaseTime {
			get;
			private set;
		}

		long _m_lDurationTime = 60;
		/// <summary>
		/// 每次更新文件间隔时间(单位秒)
		/// </summary>
		public long m_lDurationTime{
			get{ return _m_lDurationTime;}
			set{
				long temp = value;
				temp = temp <= 3 ? 3 : temp;

				this.m_lReleaseTime += (temp - _m_lDurationTime) * 1000 * 10;
				_m_lDurationTime = temp;
			}
		}

		/// <summary>
		/// 释放回掉事件
		/// </summary>
		public System.Action<string> m_callRelease;

		public ABInfo ()
		{
		}

		public ABInfo (string name, AssetBundle bundle, System.Action<string> callRelease)
		{
			Reset (name, bundle, callRelease);
		}

		/// <summary>
		/// 重置
		/// </summary>
		public void Reset(string name, AssetBundle bundle, System.Action<string> callRelease){
			this.m_abName = name;
			this.m_assetBundle = bundle;
			this.m_callRelease = callRelease;

			this.m_iUseCount = 0;
		}

		/// <summary>
		/// 释放
		/// </summary>
		public void Release(){

			if (m_callRelease != null) {
				m_callRelease (m_abName);
				m_callRelease = null;
			}

			if (m_assetBundle != null) {
				m_assetBundle.Unload (true);
				m_assetBundle = null;
			}

		}

		/// <summary>
		/// 内存资源对象
		/// </summary>
		/// <returns>The asset.</returns>
		/// <param name="assetName">Asset name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetAsset<T>(string assetName) where T : UnityEngine.Object{
			if (m_assetBundle != null) {
				return m_assetBundle.LoadAsset<T> (assetName);
			}
			return null;
		}
	}
}