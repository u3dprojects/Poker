
require "Logic/Include"

--管理器--
Game = {};
local this = Game;

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    networkMgr:OnInit();
    
    Require("Logic.Global");
    myApp:run();
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
