-- 协议 常量
Protocal = {
  Unload   = 'Unload';  -- 通知销毁
  OnSuccessConnect = '100';  --连接服务器成功回调
	Connect		= '101';	--连接服务器
	Exception   = '102';	--异常掉线
	Disconnect  = '103';	--正常断线   
	Message		= '104';	--接收消息
}

--协议类型--
ProtocalType = {
  BINARY = 0,
  PB_LUA = 1,
  PBC = 2,
  SPROTO = 3,
}
