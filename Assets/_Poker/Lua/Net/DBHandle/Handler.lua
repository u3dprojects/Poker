--- 交互层
-- Anchor : Canyon
-- Time : 2017-04-23 17:35
-- Desc :
-- Modify :

do

  local Require = gloabMgr.Require;

  local RequireOne = gloabMgr.RequireOne;

  -- 协议（请求，响应）
  local tabReqRes = {
    "Net.protobuf.EchoMsg_pb",
    "Net.protobuf.Req_DeskMsg_pb",
    "Net.protobuf.Req_LoginMsg_pb",
    "Net.protobuf.Res_PlayerMsg_pb",
  }

  Handler = {};

  local that = Handler;

  -- 初始化事件
  function Handler.InitEvents()
    Event.AddListener(Protocal.OnSuccessConnect, that.OnSuccessConnect);
    Event.AddListener(Protocal.Unload, that.Unload);

    Event.AddListener(Protocal.EntryGame, that.EntryGameCall);
  end

  -- 卸掉事件
  function Handler.Unload()
    Event.RemoveListener(Protocal.Unload);
    Event.RemoveListener(Protocal.OnSuccessConnect);
  end

  -- socket 连接成功
  function Handler.OnSuccessConnect()
    Require(tabReqRes);
    that.EntryGame();
  end

  function Handler.EntryGame()
    local msg = request_Req_LoginMsg_pb.EnterGame();
    msg.token = that.GetUser().token;
    print(msg.token)
    local body = msg:SerializeToString();

    local msgPoker = MsgPoker.New();
    msgPoker:SetCmd(Protocal.EntryGame);
    msgPoker:SetStatus("0");
    msgPoker:SetBody(body);

    networkMgr:SendMessage(msgPoker);
  end

  function Handler.EntryGameCall(buffer)
    local msgPoker = MsgPoker.New();
    msgPoker:Init(Protocal.EntryGame,buffer);
    local data = msgPoker:GetLuaData();
    local msg = response_Res_PlayerMsg_pb.Player();
    msg:ParseFromString(data);

    log('EntryGame: protocal:>'.. tostring(msgPoker:GetCmd()) ..' msg:>' .. msg.name .. "," .. type(msg));
--    coroutine.wait(0.005)
--    this.DemoMsg();
  end

  -- 取得用户对象
  function Handler.GetUser()
    if that.user == nil then
      that.user = RequireOne("Net.DBHandle.DBUser").New();
    end
    return that.user;
  end
  return Handler;
end
