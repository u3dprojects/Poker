﻿using UnityEngine;
using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : 組件根脚本
	/// 作者 : Canyon
	/// 日期 : 2017-05-12 22:03
	/// 功能 : 
	/// </summary>
	public class Kernel : MonoBehaviour,IViewBase
	{
		#region Members 成员 和 变量
		private Transform _trsfSelf;
		/// <summary>
		/// 自身的transform對象
		/// </summary>
		/// <value>The trsf self.</value>
		public Transform m_trsfSelf{
			get{
				if (_trsfSelf == null) {
					_trsfSelf = transform;
				}
				return _trsfSelf;
			}
			private set{ 
				_trsfSelf = value;
			}
		}

		private GameObject _gobjSelf;
		/// <summary>
		/// 自身GameObject對象
		/// </summary>
		/// <value>The gobj self.</value>
		public GameObject m_gobjSelf{
			get{
				if (_gobjSelf == null) {
					_gobjSelf = gameObject;
				}
				return _gobjSelf;
			}
			private set{ 
				_gobjSelf = value;
			}
		}

		/// <summary>
		/// 显隐状态
		/// </summary>
		/// <value><c>true</c> if is visibled; otherwise, <c>false</c>.</value>
		public bool m_isVisibled{ get; private set;}

		/// <summary>
		/// 暂停 状态
		/// </summary>
		/// <value><c>true</c> if is pause; otherwise, <c>false</c>.</value>
		public bool m_isPause{ get; set;}

		  // 名字
		private string _nmGobj = "";
		/// <summary>
		/// gameobject的名称
		/// </summary>
		/// <value>The nm gobj.</value>
		public string m_nmGobj{
			get{ 
				if (string.IsNullOrEmpty (_nmGobj)) {
					m_nmGobj = m_gobjSelf.name;
				}
				return _nmGobj;
			}
			set{ 
				if (_nmGobj != value) {
					m_gobjSelf.name = name;
				}
				_nmGobj = value;
			}
		}

		/// <summary>
		/// 是否已经初始化了
		/// </summary>
		private bool isInit = false;

		#endregion

		#region interfaces func 接口方法体

		/// <summary>
		/// 執行初始化
		/// </summary>
		public void DoInit ()
		{
			OnInit ();
		}

		/// <summary>
		/// 執行开始
		/// </summary>
		public void DoBegin ()
		{
			OnBegin ();
		}

		/// <summary>
		/// 執行更新
		/// </summary>
		public bool DoUpdate ()
		{
			return OnUpdate ();
		}

		/// <summary>
		/// 執行结束
		/// </summary>
		public void DoEnd ()
		{
			OnEnd ();
		}

		/// <summary>
		/// 执行清除
		/// </summary>
		public void DoClear ()
		{
			OnClear ();
		}

		/// <summary>
		/// 執行显示
		/// </summary>
		public void DoShow ()
		{
			m_gobjSelf.SetActive (true);

			OnReady ();
		}

		/// <summary>
		/// 執行隐藏
		/// </summary>
		public void DoHide ()
		{
			m_gobjSelf.SetActive (false);
		}

		/// <summary>
		/// 執行销毁
		/// </summary>
		public void DoDestroy ()
		{
			GameObject.Destroy (m_gobjSelf);
		}

		#endregion

		#region MonoBehaviour Func 组件自带函数
//		void Awake(){
//		}

		// Use this for initialization
		void Start () {
			OnReady ();
			OnStart ();
		}

		// Update is called once per frame
		bool Update () {
			return DoUpdate ();
		 }

		void OnEnable(){
			m_isVisibled = true;
			OnReady ();
			OnShow ();
		}

		void OnDisable(){
			m_isVisibled = false;
			OnHide ();
		}

		void OnDestroy(){
			OnDispose ();
		}
		#endregion

		void OnReady(){
			if (isInit) {
				return;
			}
			isInit = true;
			DoInit ();
		}

		#region entity implementation func 子类可以实现的函数

		/// <summary>
		/// 正执行 组件Component的开始
		/// </summary>
		protected virtual void OnStart ()
		{
		}

		/// <summary>
		/// 正执行初始化
		/// </summary>
		protected virtual void OnInit ()
		{
		}

		/// <summary>
		/// 正执行 生命周期的 开始
		/// </summary>
		protected virtual void OnBegin ()
		{
		}

		/// <summary>
		/// 正执行显示
		/// </summary>
		protected virtual void OnShow ()
		{
		}

		/// <summary>
		/// 正执行隐藏
		/// </summary>
		protected virtual void OnHide ()
		{
			StopAllCoroutines();
			CancelInvoke();
		}

		/// <summary>
		/// 正执行每帧率更新
		/// </summary>
		protected virtual bool OnUpdate(){
			return false;
		}

		/// <summary>
		/// 正执行资源清除
		/// </summary>
		protected virtual void OnClear(){
		}

		/// <summary>
		/// 执行结束
		/// </summary>
		protected virtual void OnEnd()
		{
		}

		/// <summary>
		/// 正执行销毁
		/// </summary>
		protected virtual void OnDispose ()
		{
			m_gobjSelf = null;
			m_trsfSelf = null;
		}
		#endregion

		void SetParent(GameObject parent,bool isLocal = true){
			UNGameEx.parentTrsf (m_gobjSelf, parent, isLocal);
		}

		void SetParent(Transform parent,bool isLocal = true){
			UNGameEx.parentTrsf (m_trsfSelf, parent, isLocal);
		}

		public void AddTo(GameObject parent,bool isLocal = true){
			SetParent (parent, isLocal);
		}

		public void AddTo(Transform parent,bool isLocal = true){
			SetParent (parent, isLocal);
		}

		public void AddChild(GameObject child,bool isLocal = true){
			UNGameEx.parentTrsf (child, m_gobjSelf, isLocal);
		}

		public void AddChild(Transform child,bool isLocal = true){
			UNGameEx.parentTrsf (child,  m_trsfSelf, isLocal);
		}

		public Transform Find(string val,Transform trsfParent = null){
			return UNGameEx.Find (m_trsfSelf,val,trsfParent);
		}

		public GameObject FindGobj(string val,Transform trsfParent = null){
			Transform ret = Find (val,trsfParent);
			if (ret != null) {
				return ret.gameObject;
			}
			return null;
		}

		public T FindComponent<T> (GameObject gobj,bool isNullAdd = false) where T : Component
		{
			return UNGameEx.FindComponent<T> (gobj, isNullAdd);
		}

		public T FindComponent<T> (Transform trsf,bool isNullAdd = false) where T : Component
		{
			return UNGameEx.FindComponent<T> (trsf, isNullAdd);
		}

		public T FindComponent<T> (string val,Transform trsfParent,bool isNullAdd) where T : Component{
			Transform trsf = Find (val,trsfParent);
			return FindComponent<T> (trsf,isNullAdd);
		}

		public T FindComponent<T> (string val,bool isNullAdd = false) where T : Component
		{
			Transform trsf = Find (val);
			return FindComponent<T> (trsf,isNullAdd);
		}

		protected virtual void SetPos(Vector3 pos,bool isLocal = true){
			if (isLocal) {
				m_trsfSelf.localPosition = pos;
			} else {
				m_trsfSelf.position = pos;
			}
		}

		protected virtual void SetScale(float scale){
			SetScale(Vector3.one * scale);
		}

		protected virtual void SetScale(Vector3 pos){
			m_trsfSelf.localScale = pos;
		}

		public Vector3 GetScale(bool isLocal = true){
			if (isLocal) {
				return m_trsfSelf.localScale;
			} else {
				return m_trsfSelf.lossyScale;
			}
		}
	}
}