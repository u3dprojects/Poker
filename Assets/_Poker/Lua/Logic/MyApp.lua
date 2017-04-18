--- 应用入口
--Author : Canyon
--Date   : 2017/04/18

MyApp = {}

--这句是重定义元表的索引，就是说有了这句，这个才是一个类。
MyApp.__index = MyApp

--构造体，构造体的名字是随便起的，习惯性改为New()
function MyApp.New( ... )
  local self = {};    --初始化self，如果没有这句，那么类所建立的对象改变，其他对象都会改变
  setmetatable(self, MyApp);  --将self的元表设定为Class

  local pars = {...};

  self:ctor(unpack(pars));

  return self;    --返回自身
end

function MyApp:ctor()
  gloabMgr.Require("Logic.CtrlManager");

  --注册LuaView--
  gloabMgr.Require(PanelNames,"View");

  --测试第三方库功能--
  local func = function() self:test_coroutine(); end
  coroutine.start(func);

  CtrlManager.Init();
  local ctrl = CtrlManager.GetCtrl(CtrlNames.Prompt);
  if ctrl ~= nil then
    ctrl:Awake();
  end
  logWarn('数据初始化Game.lua InitOK--->>>');
end


--测试协同--
function MyApp:test_coroutine()
  logWarn("1111");
  coroutine.wait(1);
  logWarn("2222");

  local www = WWW("http://doc.ulua.org/readme.txt");
  coroutine.www(www);
  log("myapp = " .. www.text);
end

--测试打印方法--
function MyApp:test()
  logWarn("x:>" .. self.x .. " y:>" .. self.y);
end



--endregion
