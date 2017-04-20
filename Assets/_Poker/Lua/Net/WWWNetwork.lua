--- 异步请求 WWW
-- Anchor : Canyon
-- Time : 2016-03-19 15:30
-- Desc :

local WWW = WWW or UnityEngine.WWW;
-- local cjson = require("cjson");
-- local WWWForm = WWWForm or UnityEngine.WWWForm;

local M = {};

function M:wwwCoroutine(url,call4Succes,call4Fail,pars)
  return function()
    local www = WWW.New(url);
    coroutine.www( www );
    if www.error then
      if call4Fail then
        call4Fail(www,pars);
      end;
    else
      if call4Succes then
        call4Succes(www,pars);
      end;
    end;
    www:Dispose();
    www = nil;
  end;
end

function M:wwwCoroutinePost(url,form,call4Succes,call4Fail,pars)
  return function()
    local www = WWW.New(url,form);
    coroutine.www( www );
    if www.error then
      if call4Fail then
        call4Fail(www,pars);
      end;
    else
      if call4Succes then
        call4Succes(www,pars);
      end;
    end;
    www:Dispose();
    www = nil;
  end
end

function M:startWww(url,call4Succes,call4Fail,pars)
	local _func = self:wwwCoroutine(url,call4Succes,call4Fail,pars);
	coroutine.start(_func);
end

function M:startWwwPost(url,form,call4Succes,call4Fail,pars)
  local _func = self:wwwCoroutinePost(url,form,call4Succes,call4Fail,pars);
  coroutine.start(_func);
end

return M
