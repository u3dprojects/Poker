using UnityEngine;
using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : ut-unity 资源下载管理
	/// 作者 : Canyon
	/// 日期 : 2017-05-17 15:03
	/// 功能 : 
	/// </summary>
	public class WWWMgr : BaseMono
	{

		private static WWWMgr _instance;

		static public WWWMgr instance{
			get{ 
				if (_instance == null) {
					_instance = MgrCtrls.instance.AddMgr<WWWMgr> (MgrCtrls.Names.WWWMgr);
				}
				return _instance;
			}
		}

	}
}