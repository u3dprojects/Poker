--- 处理器  - 请求，响应
-- Anchor : Canyon
-- Time : 2017-04-23 17:35
-- Desc :
-- Modify :

do

  local Require = Require or gloabMgr.Require;

  local RequireOne = RequireOne or gloabMgr.RequireOne;

  -- 协议（请求，响应）
  local tabReqRes = {
    "Net.protobuf.EchoMsg_pb",
    "Net.protobuf.Req_DeskMsg_pb",
    "Net.protobuf.Req_LoginMsg_pb",
    "Net.protobuf.Res_PlayerMsg_pb",
  }

  local mgrDB = nil;

  Handler = {};

  local that = Handler;

  -- 初始化事件
  function Handler.InitEvents()
    Event.AddListener(Protocal.OnSuccessConnect, that.OnSuccessConnect);
    Event.AddListener(Protocal.Unload, that.Unload);

    Event.AddListener(Protocal.EntryGame, that.CallEntryGame);
    Event.AddListener(Protocal.Heartbeat, that.CallHeartbeat);
  end

  -- 卸掉事件
  function Handler.Unload()
    Event.RemoveListener(Protocal.Unload);
    Event.RemoveListener(Protocal.OnSuccessConnect);
  end

  -- socket 连接成功
  function Handler.OnSuccessConnect()
    Require(tabReqRes);
    mgrDB = RequireOne("Net.DBHandle.MgrDB");
    that.EntryGame();
  end

  function Handler.EntryGame()
    local msg = request_Req_LoginMsg_pb.EnterGame();
    msg.token = mgrDB.GetUser().token;
    local body = msg:SerializeToString();

    local msgPoker = MsgPoker.New();
    msgPoker:SetCmd(Protocal.EntryGame);
    msgPoker:SetStatus("0");
    msgPoker:SetBody(body);

    networkMgr:SendMessage(msgPoker);
  end

  function Handler.CallEntryGame(buffer)
    local msgPoker = MsgPoker.New();
    msgPoker:Init(Protocal.EntryGame,buffer);
    local data = msgPoker:GetLuaData();
    local msg = response_Res_PlayerMsg_pb.Player();
    msg:ParseFromString(data);

    mgrDB.GetUser():Init(msg);

    that.Heartbeat();

    log('EntryGame: cmd >'.. tostring(msgPoker:GetCmd()) ..' msg:>' .. msg.name);
  --    coroutine.wait(0.005)
  --    this.DemoMsg();
  end

  function Handler.Heartbeat()
    local msg = request_Req_LoginMsg_pb.Heartbeat();
    local body = msg:SerializeToString();

    local msgPoker = MsgPoker.New();
    msgPoker:SetCmd(Protocal.Heartbeat);
    msgPoker:SetStatus("0");
    msgPoker:SetBody(body);
    networkMgr:SendMessage(msgPoker);
  end

  function Handler.CallHeartbeat(buffer)
    log('CallHeartbeat:> 1');
    coroutine.wait(1)
    that.Heartbeat();
    log('CallHeartbeat:> 2');
  end

  return Handler;
end
