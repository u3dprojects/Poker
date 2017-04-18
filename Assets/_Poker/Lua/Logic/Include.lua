--- 需要先加载的部分
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
  
  local tab3rd = {
    "cjson",
    "3rd/cjson/util",
  }
  
  gloabMgr.Require(tab3rd);
  
  local tabCommon = {
    "Common/define",
    "Common/functions",
    "Common/protocal"
  };
  
  gloabMgr.Require(tabCommon);
end;

