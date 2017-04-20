
--输出日志--
function log(str)
  Util.Log(str);
end

--错误日志--
function logError(str)
  Util.LogError(str);
end

--警告日志--
function logWarn(str)
  Util.LogWarning(str);
end

--查找对象--
function find(str)
  return GameObject.Find(str);
end

function destroy(obj)
  GameObject.Destroy(obj);
end

function newObject(prefab)
  return GameObject.Instantiate(prefab);
end

--创建面板--
function createPanel(name,func)
  panelMgr:CreatePanel(name,func);
end

function child(transform,str)
  return transform:FindChild(str);
end

function childGobj(transform,str)
  local trsf = child(transform,str);
  if trsf then
    return trsf.gameObject;
  end
end

function getComponent(transform,typeName)
  return transform:GetComponent(typeName);
end

--function subGet(childNode, typeName)
--  return child(childNode):GetComponent(typeName);
--end

function findPanel(str)
  local obj = find(str);
  if obj == nil then
    error(str.." is null");
    return nil;
  end
  return obj:GetComponent("BaseLua");
end
