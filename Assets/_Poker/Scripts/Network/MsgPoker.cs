using UnityEngine;
using System.Collections;
using System.IO;
using LuaInterface;
using LuaFramework;

namespace Poker {
	public class MsgPoker{
		ushort cmd;
		ushort status;
		byte[] body;

		LuaByteBuffer luaData;

		public ushort GetCmd(){
			return this.cmd;
		}

		public void SetCmd(ushort v) {
			this.cmd = v;
		}

		public ushort GetStatus(){
			return this.status;
		}

		public void SetStatus(ushort v) {
			this.status = v;
		}

		public void SetBody(byte[] body) {
			this.body = body;
		}

		public void SetBody(LuaByteBuffer strBuffer) {
			body = strBuffer.buffer;
		}

		public LuaByteBuffer GetLuaData(){
			return this.luaData;
		}

		public void Init(ushort cmd,ByteBuffer buffer){
			this.cmd = cmd;
			this.status = buffer.ReadShort ();
			this.luaData = buffer.ReadBufferLua();
		}

		[NoToLua]
		public byte[] ToArray(){
			byte[] ret = new byte[0];
			MemoryStream ms = null;
			using (ms = new MemoryStream()) {
				ms.Position = 0;
				uint msglen = (uint)body.Length + 8;
				BinaryWriter writer = new BinaryWriter(ms);
				writer.Write((int)msglen);
				writer.Write((short)cmd);
				writer.Write ((short)status);
				writer.Write (body);
				writer.Flush();
				ret = ms.ToArray();
			}
			return ret;
		}
	}
}
