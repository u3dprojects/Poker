using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : unity3d 组件 对象接口
	/// 作者 : Canyon
	/// 日期 : 2016-12-26 12:00
	/// 功能 :  
	/// </summary>
	public interface IViewBase : IBase
	{
		/// <summary>
		/// 執行显示
		/// </summary>
		void DoShow ();

		/// <summary>
		/// 執行隐藏
		/// </summary>
		void DoHide ();

		/// <summary>
		/// 執行销毁
		/// </summary>
		void DoDestroy ();
	}
}