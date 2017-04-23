--- Socket 通知脚本

local Event = require("events");

Network = {};
local this = Network;

this.isConnect = false;

function Network.Start()
  logWarn("Network.Start!!");
  Event.AddListener(Protocal.Connect, this.OnConnect);
  Event.AddListener(Protocal.Exception, this.OnException);
  Event.AddListener(Protocal.Disconnect, this.OnDisconnect);
--  Event.AddListener(Protocal.Message, this.OnMessage);
--  Event.AddListener(Protocal.Msg, this.DemoResponse);
end

function Network.Connect(address,post)
  this.isConnect = false;
  AppConst.SocketAddress = address or "127.0.0.1";
  AppConst.SocketPort = post or 9999;
  networkMgr:SendConnect();
end

--Socket消息--
function Network.OnSocket(key, data)
  Event.Brocast(tostring(key), data);
end

--当连接建立时--
function Network.OnConnect()
  this.isConnect = true;
  Event.Brocast(Protocal.OnSuccessConnect);
end

--异常断线--
function Network.OnException()
  this.isConnect = false;
  networkMgr:SendConnect();
  logError("OnException------->>>>");
end

--连接中断，或者被踢掉--
function Network.OnDisconnect()
  this.isConnect = false;
  logError("OnDisconnect------->>>>");
end

--卸载网络监听--
function Network.Unload()
  Event.RemoveListener(Protocal.Connect);
  Event.RemoveListener(Protocal.Exception);
  Event.RemoveListener(Protocal.Disconnect);
  Event.Brocast(Protocal.Unload);
  --  Event.RemoveListener(Protocal.Message);
  logWarn('Unload Network...');
end

--登录返回--
function Network.OnMessage(buffer)
  if TestProtoType == ProtocalType.BINARY then
    this.TestLoginBinary(buffer);
  end
  if TestProtoType == ProtocalType.PB_LUA then
    this.TestLoginPblua(buffer);
  end
  if TestProtoType == ProtocalType.PBC then
    this.TestLoginPbc(buffer);
  end
  if TestProtoType == ProtocalType.SPROTO then
    this.TestLoginSproto(buffer);
  end
  ----------------------------------------------------
  local ctrl = CtrlManager.GetCtrl(CtrlNames.Message);
  if ctrl ~= nil then
    ctrl:Awake();
  end
  logWarn('OnMessage-------->>>');
end

--二进制登录--
function Network.TestLoginBinary(buffer)
  local protocal = buffer:ReadByte();
  local str = buffer:ReadString();
  log('TestLoginBinary: protocal:>'..protocal..' str:>'..str);
end

function Network.DemoResponse(buffer)
  local msgPoker = MsgPoker.New();
  msgPoker:Init(Protocal.Msg,buffer);
  local data = msgPoker:GetLuaData();
  local msg = DemoMsg_pb.DemoResponse();
  msg:ParseFromString(data);

  log('DemoResponse: protocal:>'.. tostring(msgPoker:GetCmd()) ..' msg:>' .. msg.name .. "," .. type(msg));
  coroutine.wait(0.005)
  this.DemoMsg();
end

function Network.DemoMsg()
  local login = DemoMsg_pb.DemoRequest();
  login.IP = "192.168.1.111";
  login.accountId = 'Test0001';
  login.serverId = 20101;
  local msg = login:SerializeToString();
  ----------------------------------------------------------------
  local buffer = MsgPoker.New();
  buffer:SetCmd(Protocal.Msg);
  buffer:SetStatus(0);
  -- buffer:WriteByte(ProtocalType.PB_LUA);
  buffer:SetBody(msg);
  networkMgr:SendMessage(buffer);
  log("=== Network.DemoMsg end ===");
end
