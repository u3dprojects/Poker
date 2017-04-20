--- 加载工具
-- Anchor : Canyon
-- Time : 2017-04-18 11:25
-- Desc :
-- Modify :

do

  -- 全局变量
  local MG = _G;

  local M = {};

  local that = M;

  local package = package;

  local string = string;

  -- 记录加载了的Lua脚本
  local recordRequire = {};

  -- 全局
  function M.Consts(tab)
    local tp = type(tab);

    if tp ~= "table" then
      return;
    end

    for key, var in pairs(tab) do
      that.ConstsKV(key,var);
    end
  end

  -- 全局
  function M.ConstsKV(k,v)
    local tp = type(k);

    if tp ~= "string" then
      return;
    end
    MG[k] = v;
  end

  -- 移除全局
  function M.RemoveOne(k)
    MG[k] = nil;
  end

  function M.Require(pars,wrap)
    local tp = type(pars);

    if tp == "table" then
      for _, var in pairs(pars) do
        that.RequireOne(var,wrap);
      end
    elseif tp == "string" then
      that.RequireOne(pars,wrap);
    end
  end

  function M.RequireOne(modname,wrap)

    string.gsub(modname,"/",".");

    if type(wrap) == "string" then
      string.gsub(wrap,"/",".");
      modname = wrap .. "." .. modname;
    end

    if recordRequire[modname] then
      return recordRequire[modname];
    end

    local function funcMain()
      return require(modname);
    end

    local funcError = function(msg)
      local msgTip = string.format("'%s' not found:", modname);
      if string.find(msg, msgTip) then
        print(msgTip);
      else
        print("load lua error, in " .. modname);
      end
    end

    local status, data = xpcall(funcMain,funcError);

    if status then
      local tp = type(data);
      if (tp == "table" or tp == "userdata") then
        recordRequire[modname] = data;
        return data;
      end
    else
      error(data);
    end
  end

  function M.ClearRequire(pars)
    local tp = type(pars);

    if tp == "table" then
      for _, var in pairs(pars) do
        that.ClearRequireOne(var);
      end
    elseif tp == "string" then
      that.ClearRequireOne(pars);
    end
  end

  function M.ClearRequireOne(modname)
    package.preload[modname] = nil;
    package.loaded[modname] = nil;
  end

  function M.ClearRequire()
    that.ClearRequire(recordRequire);
  end

  return M;
end
