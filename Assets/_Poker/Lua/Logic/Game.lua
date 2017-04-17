
local json = require "cjson"
local util = require "3rd/cjson/util"

require "Common/define"
require "Common/functions"

require "Logic/LuaClass"
require "Logic/CtrlManager"

require "Controller/PromptCtrl"

--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

function Game.InitViewPanels()
	for i = 1, #PanelNames do
		require ("View/"..tostring(PanelNames[i]))
	end
end

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    AppConst.SocketPort = 9999;
    AppConst.SocketAddress = "127.0.0.1";
    networkMgr:SendConnect();

    --注册LuaView--
    this.InitViewPanels();

    --测试第三方库功能--
    coroutine.start(this.test_coroutine);

    CtrlManager.Init();
    local ctrl = CtrlManager.GetCtrl(CtrlNames.Prompt);
    if ctrl ~= nil then
        ctrl:Awake();
    end
    logWarn('数据初始化Game.lua InitOK--->>>');
end

--测试协同--
function Game.test_coroutine()    
    logWarn("1111");
    coroutine.wait(1);	
    logWarn("2222");
	
    local www = WWW("http://doc.ulua.org/readme.txt");
    coroutine.www(www);
    logWarn(www.text);    	
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
