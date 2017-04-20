using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Resources = Core.Kernel.Resources;

namespace LuaFramework {
    public class ResourceManager : Manager {
		private AssetBundle shared = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public void initialize(Action func) {
//            if (AppConst.ExampleMode) {
//                //------------------------------------Shared--------------------------------------
//                string uri = Util.DataPath + "shared" + AppConst.ExtName;
//                Debug.LogWarning("LoadFile::>> " + uri);
//
//                shared = AssetBundle.LoadFromFile(uri);
//#if UNITY_5
//                shared.LoadAsset("Dialog", typeof(GameObject));
//#else
//                shared.Load("Dialog", typeof(GameObject));
//#endif
//            }

			Resources.m_edtAssetPath = "_Poker/Builds";

            if (func != null) func();    //资源初始化完成，回调游戏管理器，执行后续操作 
        }

        /// <summary>
        /// 载入素材
        /// </summary>
        public AssetBundle LoadBundle(string name) {
            string uri = Util.DataPath + name.ToLower() + AppConst.ExtName;
            AssetBundle bundle = AssetBundle.LoadFromFile(uri); //关联数据的素材绑定
            return bundle;
        }

		public AssetBundle LoadBundle(string name,string suffix) {
			string uri = Util.DataPath + name.ToLower() + suffix;
			AssetBundle bundle = AssetBundle.LoadFromFile(uri); //关联数据的素材绑定
			return bundle;
		}

		public UnityEngine.Object Load(string name,string suffix) {
			bool isLoadAssset = true;
			if (Application.isEditor) {
				isLoadAssset = AppConst.EdtiorLoadAsset;
			}

			if (isLoadAssset) {
				suffix = AppConst.ExtName;
				return LoadBundle (name,suffix);
			} else {
				return Resources.Load4Develop (GetPathFab(name), suffix);
			}
		}

		public UnityEngine.Object LoadPrefab(string name) {
			return Load (name, Resources.m_extLowerFab);
		}


		public string GetPathFab(string name){
			return Resources.m_appAssetPath + name + Resources.DSChar + "Prefabs" + Resources.DSChar + name;
		}
        /// <summary>
        /// 销毁资源
        /// </summary>
        void OnDestroy() {
            if (shared != null) shared.Unload(true);
            Debug.Log("~ResourceManager was destroy!");
        }
    }
}