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
    modname = wrap .. "." .. modname;
  end
  
  return require(modname);
end


function M.ClearReuire(pars)
  local tp = type(pars);
  
  if tp == "table" then
    for _, var in pairs(pars) do
      that.ClearReuireOne(var);
    end
  elseif tp == "string" then
    that.ClearReuireOne(pars);
  end
end

function M.ClearReuireOne(modname)
	package.preload[modname] = nil;
  package.loaded[modname] = nil;
end

return M;
end