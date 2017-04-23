--- 用户 数据(保护操作)
-- Anchor : Canyon
-- Time : 2017-04-23 17:35
-- Desc : 
-- Modify :

local DBUser = {};

DBUser.__index = {__name = "DBUser"};

function DBUser.New()

	local self = {};
  
  setmetatable(self, DBUser);
  
  return self;
end

function DBUser:SetToken(tk)
	self.token = tk;
end

return DBUser;