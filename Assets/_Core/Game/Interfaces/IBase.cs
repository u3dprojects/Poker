using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : 对象接口
	/// 作者 : Canyon
	/// 日期 : 2016-12-26 12:00
	/// 功能 :  所有的对象(数据对象，组件对象的基础接口) - 对象一个周期
	/// </summary>
	public interface IBase
	{
		/// <summary>
		/// 執行初始化
		/// </summary>
		void DoInit ();

		/// <summary>
		/// 執行开始
		/// </summary>
		void DoBegin ();

		/// <summary>
		/// 執行更新
		/// </summary>
		bool DoUpdate ();

		/// <summary>
		/// 執行结束
		/// </summary>
		void DoEnd ();

		/// <summary>
		/// 执行清除
		/// </summary>
		void DoClear ();
	}
}