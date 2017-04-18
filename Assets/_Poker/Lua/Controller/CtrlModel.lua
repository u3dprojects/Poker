--- prefab 管理
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc : 
-- Modify :


-- require "Common/define"

PromptCtrl = {};
local this = PromptCtrl;

local panel;
local prompt;
local transform;
local gameObject;

--构建函数--
function PromptCtrl.New()
  logWarn("PromptCtrl.New--->>");
  return this;
end

function PromptCtrl.Awake()
  logWarn("PromptCtrl.Awake--->>");
  panelMgr:CreatePanel('Prompt', this.OnCreate);
end

--启动事件--
function PromptCtrl.OnCreate(obj)
  gameObject = obj;
  transform = obj.transform;

  panel = transform:GetComponent('UIPanel');
  prompt = transform:GetComponent('LuaBehaviour');
  logWarn("Start lua--->>"..gameObject.name);

  this.InitPanel(); --初始化面板--
  prompt:AddClick(PromptPanel.btnOpen, this.OnClick);
end

--初始化面板--
function PromptCtrl.InitPanel()
  panel.depth = 1;  --设置纵深--
  local parent = PromptPanel.gridParent;
  local itemPrefab = prompt:LoadAsset('PromptItem');
  for i = 1, 100 do
    local go = newObject(itemPrefab);
    go.name = tostring(i);
    go.transform.parent = parent;
    go.transform.localScale = Vector3.one;
    go.transform.localPosition = Vector3.zero;
    prompt:AddClick(go, this.OnItemClick);

    local goo = go.transform:FindChild('Label');
    goo:GetComponent('UILabel').text = i;
  end
  local grid = parent:GetComponent('UIGrid');
  grid:Reposition();
  grid.repositionNow = true;
  parent:GetComponent('WrapGrid'):InitGrid();
end

--滚动项单击事件--
function PromptCtrl.OnItemClick(go)
  log(go.name);
end

--单击事件--
function PromptCtrl.OnClick(go)
  Network.DemoMsg();
  logWarn("OnClick---->>>"..go.name);
end

--测试发送二进制--
function PromptCtrl.TestSendBinary()
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Protocal.Message);
    buffer:WriteByte(ProtocalType.BINARY);
    buffer:WriteString("ffff我的ffffQ靈uuu");
    buffer:WriteInt(200);
    networkMgr:SendMessage(buffer);
end

--关闭事件--
function PromptCtrl.Close()
  panelMgr:ClosePanel(CtrlNames.Prompt);
end