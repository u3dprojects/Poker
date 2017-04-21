﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Core.Kernel{
	
	/// <summary>
	/// 类名 : 路径工具
	/// 作者 : Canyon
	/// 日期 : 2017-04-21 15:40
	/// 功能 : 
	/// </summary>
	public static class EL_Path {

		/// <summary>
		/// 文件夹地址
		/// </summary>
		static public List<string> paths = new List<string>();

		/// <summary>
		/// 文件地址
		/// </summary>
		static public List<string> files = new List<string>();

		/// <summary>
		/// 取得该路径下面的-所有文件，以及该下面的所以文件夹
		/// </summary>
		/// <param name="path">Path.</param>
		static public void Recursive(string path) {
			string[] names = Directory.GetFiles(path);
			string[] dirs = Directory.GetDirectories(path);
			foreach (string filename in names) {
				string ext = Path.GetExtension(filename);
				if (ext.Equals(".meta")) continue;
				files.Add(filename.Replace('\\', '/'));
			}
			foreach (string dir in dirs) {
				paths.Add(dir.Replace('\\', '/'));
				Recursive(dir);
			}
		}

		static public void Init(string path){
			Clear ();
			Recursive (path);
		}

		static public void Clear()
		{
			paths.Clear();
			files.Clear(); 
		}
	}
}