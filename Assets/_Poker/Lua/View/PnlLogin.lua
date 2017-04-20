--- 界面 - 登录
-- Anchor : Canyon
-- Time : 2017-04-19 17:10
-- Desc : Tolua 要求 prefabname = lua.modelname 
do
  PnlLogin = {};
  local this = PnlLogin;

  local transform;
  local gameObject;
  
  --启动事件--
  function PnlLogin.Awake(obj)
    gameObject = obj;
    transform = obj.transform;

    this.InitPanel();
  end

  --初始化面板--
  function PnlLogin.InitPanel()
--    this.btnOpen = transform:FindChild("Open").gameObject;
--    this.gridParent = transform:FindChild('ScrollView/Grid');
  end

  --单击事件--
  function PnlLogin.OnDestroy()
    logWarn("OnDestroy---->>>");
  end
  
  return PnlLogin;
end
