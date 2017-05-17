using UnityEngine;
using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : 根脚本
	/// 作者 : Canyon
	/// 日期 : 2017-05-12 22:03
	/// 功能 : 
	/// </summary>
	public class BaseMono : MonoBehaviour
	{
		private MgrCtrls m_ctrl;
		public MgrCtrls myCtrl{
			get{
				if (m_ctrl == null) {
					m_ctrl = MgrCtrls.instance;
				}
				return m_ctrl;
			}
		}

	}
}
