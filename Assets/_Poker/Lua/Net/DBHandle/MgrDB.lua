--- 数据管理
-- Anchor : Canyon
-- Time : 2017-04-23 17:35
-- Desc :
-- Modify :

do

  MgrDB = {};

  local that = MgrDB;

  -- 取得用户对象
  function MgrDB.GetUser()
    if that.user == nil then
      that.user = RequireOne("Net.DBHandle.DBUser").New();
    end
    return that.user;
  end
  return MgrDB;
end
