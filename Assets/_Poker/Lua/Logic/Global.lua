--- Lua 的 单例 对象
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc : 一个脚本对应一个界面(脚本里面去加载界面)，这里主要控制lua脚本 

myApp = gloabMgr.RequireOne("Logic.MyApp").New();

mgrUI = gloabMgr.RequireOne("Controller.MgrUI").New();

mgrWww = gloabMgr.RequireOne("Net/WWWNetwork");

mgrWwwGame = gloabMgr.RequireOne("Net/WWWGame");

TimeEx = gloabMgr.RequireOne("System/TimeEx");

json = gloabMgr.RequireOne("cjson");

util = gloabMgr.RequireOne("3rd/cjson/util");