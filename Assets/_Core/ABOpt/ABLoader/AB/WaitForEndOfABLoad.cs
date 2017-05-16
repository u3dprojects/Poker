using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 自定义资源加载资源协同程序
/// 作者 : Canyon
/// 日期 : 2017-05-16 17:53
/// 功能 : 
/// </summary>
public class WaitForEndOfABLoad : CustomYieldInstruction {
	#region implemented abstract members of CustomYieldInstruction
	public override bool keepWaiting {
		get {
			return false;
		}
	}
	#endregion
}
