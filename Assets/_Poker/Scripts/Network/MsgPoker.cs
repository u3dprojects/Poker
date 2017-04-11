using UnityEngine;
using System.Collections;
using System.IO;
using LuaInterface;

namespace Poker {
	public class MsgPoker{
		ushort cmd;
		ushort status;
		byte[] body;

		public void SetCmd(ushort v) {
			this.cmd = v;
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
