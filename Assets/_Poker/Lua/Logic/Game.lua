
require "Logic/Include"

local json = require "cjson"
local util = require "3rd/cjson/util"

--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    AppConst.SocketPort = 9999;
    AppConst.SocketAddress = "127.0.0.1";
    networkMgr:SendConnect();

    gloabMgr.Require("Logic.Global");
    myApp:run();
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
