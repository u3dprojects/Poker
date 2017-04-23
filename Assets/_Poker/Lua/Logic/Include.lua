--- 需要先加载的部分
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc : 一个脚本对应一个界面(脚本里面去加载界面)，这里主要控制lua脚本
-- Modify :
do
  gloabMgr = require("Common/Import");

  -- 多重函数
  -- 初始化一个函数,调用的时候传一个table过去，会检测该table对象里面有没有该函数
  local function callFunc( funcName )
    return function ( ... )
      local arg = {...}
      return function ( self )
        if self[funcName] then
          self[funcName]( self, unpack( arg ) )
        else
          print( "In table[%s], can't find function by name %s",type(self), funcName )
        end
      end
    end
  end

  gloabMgr.ConstsKV("call",callFunc);

  local locTab = {};

  locTab = {
    "System/LuaClass",
    "System/Math",
    "System/TableEx",
    "System/TimeEx",
    "System/RndEx",
    "System/StringEx",
  }

  gloabMgr.Require(locTab);

  locTab = {};

  table.insertTo(locTab,{
    "cjson",
    "3rd/cjson/util",
  });

  table.insertTo(locTab,{
    "Common/define",
    "Common/functions",
  });

  table.insertTo(locTab,{
    "Net/protocal",
  });

  table.append(locTab,"Net/Network","Net/WWWNetwork","Net/WWWGame");

  gloabMgr.Require(locTab);
  
  -- 协议（请求，响应）
  local tabReqRes = {
    "Net.protobuf.EchoMsg_pb",
    "Net.protobuf.Req_DeskMsg_pb",
    "Net.protobuf.Req_LoginMsg_pb",
    "Net.protobuf.Res_PlayerMsg_pb",
  }
  -- gloabMgr.Require(tabReqRes);
  
end;




