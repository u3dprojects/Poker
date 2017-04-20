--- UI 的 Lua 脚本 管理
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc : 一个脚本对应一个界面(脚本里面去加载界面)，这里主要控制lua脚本
-- Modify :
do
  MgrUI = {};

  MgrUI.__index = MgrUI

  local Fixed_Width = 1334;
  local Fixed_Height = 750;

  -- LT - luaTable view视图中的UI资源脚本
  LTUiName = {
    PnlLogin = "PnlLogin",
  }

  --构建函数--
  function MgrUI.New()
    local self = {};
    setmetatable(self, MgrUI);
    self:ctor();
    return self;
  end

  function MgrUI:ctor()
    -- UI lua脚本位置
    self.vwLuaRoot = "View";

    self.guiRoot = GameObject.Find("GameGUI");
    self.guiScale = self.guiRoot.transform.localScale.x

    -- 纵横比
    self.aspectScreen = (Screen.height / Screen.width);
    self.aspectUI = (Fixed_Height / Fixed_Width);

    -- 屏幕 - UI 比例;
    self.resolutionScale = self.aspectScreen / self.aspectUI;

    -- 模型在UI中实际缩放大小
    self.modelScaleInUI = self.resolutionScale / self.guiScale;

    gloabMgr.Require(LTUiName,self.vwLuaRoot);
  end

  -- 打开界面 (传递视图的lua脚本名)
  function MgrUI:OpenView(assetname)
    panelMgr:CreatePanel(assetname, function(gobj) self:OnCreate(gobj) end);
  end

  function MgrUI:OnCreate(gobj)
  print(gobj.name);
  end
  
  return MgrUI;
end
