--- Lua 的 单例 对象
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc : 一个脚本对应一个界面(脚本里面去加载界面)，这里主要控制lua脚本

Event = RequireOne("events");

myApp = RequireOne("Logic.MyApp").New();

mgrUI = RequireOne("Controller.MgrUI").New();

mgrWww = RequireOne("Net/WWWNetwork");

mgrWwwGame = RequireOne("Net/WWWGame");

TimeEx = RequireOne("System/TimeEx");

RndEx = RequireOne("System/RndEx");

json = RequireOne("cjson");

util = RequireOne("3rd/cjson/util");

-- 数据处理
RequireOne("Net.DBHandle.MgrDB");

-- 请求，响应
if Handler then
  Handler.InitEvents();
end
RequireOne("Net.Handler");
Handler.InitEvents();


