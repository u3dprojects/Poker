--- Lua 的 单例 对象
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc : 一个脚本对应一个界面(脚本里面去加载界面)，这里主要控制lua脚本

Event = gloabMgr.RequireOne("events");

myApp = gloabMgr.RequireOne("Logic.MyApp").New();

mgrUI = gloabMgr.RequireOne("Controller.MgrUI").New();

mgrWww = gloabMgr.RequireOne("Net/WWWNetwork");

mgrWwwGame = gloabMgr.RequireOne("Net/WWWGame");

TimeEx = gloabMgr.RequireOne("System/TimeEx");

RndEx = gloabMgr.RequireOne("System/RndEx");

json = gloabMgr.RequireOne("cjson");

util = gloabMgr.RequireOne("3rd/cjson/util");

-- 数据处理
--gloabMgr.RequireOne("Net.DBHandle.Handler");
--Handler.InitEvents();
