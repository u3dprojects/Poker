--- 交互层
-- Anchor : Canyon
-- Time : 2017-04-23 17:35
-- Desc :
-- Modify :

do

  local Require = gloabMgr.RequireOne;

  Handler = {};

  local that = Handler;

  -- 初始化事件
  function Handler.InitEvents()
    Event.AddListener(Protocal.OnSuccessConnect, that.OnSuccessConnect);
  end

  -- 卸掉事件
  function Handler.Unload()
    Event.RemoveListener(Protocal.OnSuccessConnect);
  end

  function Handler.OnSuccessConnect()
  end

  -- 取得用户对象
  function Handler.GetUser()
    if that.user == nil then
      that.user = Require("Net.DBHandle.DBUser").New();
    end
    return that.user;
  end
  return Handler;
end
