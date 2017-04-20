--- 游戏数据:与服务器交互的数据请求
-- Anchor : Canyon
-- Time : 2016-03-19 17:30
-- Desc :

local cjson = require("cjson");
local WWWForm = WWWForm or UnityEngine.WWWForm;
local WWWNetwork = require("Net.WWWNetwork");

local math = math;
local url = "http://119.29.24.161:8080/ChessLogin/login";

local M = {};

-- 取得唯一标识
function M:getUid()
--  local tabUser = mgrRecord:getData( User_Info );
--  if type(tabUser) ~= "table" then
--    tabUser = {};
--  end
--  local uid = tabUser.uid;
--  if uid == nil or uid == "" then
--    -- getUUID
--    uid = Tools.getNewUID(true);
--    tabUser.uid = uid;
--    mgrRecord:setData(User_Info, tabUser);
--  end
--  return uid;
end

function M:login(lgId, call4Succes,call4Fail,pars)
  local _form = WWWForm.New();
  -- 唯一码
  _form:AddField("code",lgId);
  
  -- 渠道标识
  _form:AddField("channel","NAN");
  
  -- 昵称
  _form:AddField("name","NAN");
  
  -- 设备唯一编号
  _form:AddField("deviceId","NAN");
  
  -- 设备名称
  _form:AddField("deviceName","NAN");
  
  -- 设备token
  _form:AddField("deviceToken","NAN");
  
  -- 系统名称
  _form:AddField("osName","NAN");
  
  -- 系统版本
  _form:AddField("osVersion","NAN");
  
  -- sdk名称
  _form:AddField("sdkName","NAN");
  
  -- sdk版本号
  _form:AddField("sdkVersion","NAN");
  
  -- 移动设备国家码
  _form:AddField("mcc","NAN");
  
  WWWNetwork:startWwwPost(url,_form,call4Succes,call4Fail,pars);
end

return M
