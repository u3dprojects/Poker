function checknumber(value, base)
  return tonumber(value, base) or 0
end

function checkint(value)
  return math.round(checknumber(value))
end

-- 随机种子，短时间内避免种子重复
function randomSeed()
  local _time = os.time();
  local _seed = tostring(_time):reverse():sub(1, 6);
  math.randomseed(_seed);
end

function table.contains(tab, element)
  if tab == nil then
    return false
  end

  for _, value in pairs(tab) do
    if value == element then
      return true
    end
  end
  return false
end

function table.getCount(tab)
  local count = 0

  for k, v in pairs(tab) do
    count = count + 1
  end

  return count
end

function table.nums(t)
  local count = 0
  for k, v in pairs(t) do
    count = count + 1
  end
  return count
end

function table.keys(hashtable)
  local keys = {}
  for k, v in pairs(hashtable) do
    keys[#keys + 1] = k
  end
  return keys
end

function table.values(hashtable)
  local values = {}
  for k, v in pairs(hashtable) do
    values[#values + 1] = v
  end
  return values
end

function table.merge(dest, src)
  for k, v in pairs(src) do
    dest[k] = v
  end
end

function table.insertTo(dest, src, begin)
  begin = checkint(begin)
  if begin <= 0 then
    begin = #dest + 1
  end

  local len = #src
  for i = 0, len - 1 do
    dest[i + begin] = src[i + 1]
  end
end

function table.indexOf(array, value, begin)
  for i = begin or 1, #array do
    if array[i] == value then return i end
  end
  return false
end

function table.keyOf(hashtable, value)
  for k, v in pairs(hashtable) do
    if v == value then return k end
  end
  return nil
end

function table.removeByValue(array, value, removeall)
  local c, i, max = 0, 1, #array
  while i <= max do
    if array[i] == value then
      table_remove(array, i)
      c = c + 1
      i = i - 1
      max = max - 1
      if not removeall then break end
    end
    i = i + 1
  end
  return c
end

function table.map(t, fn)
  local n = {}
  for k, v in pairs(t) do
    n[k] = fn(v, k)
  end
  return n
end

function table.walk(t, fn)
  for k,v in pairs(t) do
    fn(v, k)
  end
end

function table.filter(t, fn)
  local n = {}
  for k, v in pairs(t) do
    if fn(v, k) then
      n[k] = v
    end
  end
  return n
end

function table.unique(t, bArray)
  local check = {}
  local n = {}
  local idx = 1
  for k, v in pairs(t) do
    if not check[v] then
      if bArray then
        n[idx] = v
        idx = idx + 1
      else
        n[k] = v
      end
      check[v] = true
    end
  end
  return n
end

function table.deepCopy( src, dest )
  local function _deepCopy( src, dest )
    dest = dest or {}
    for k, v in pairs( src ) do
      if type(v) == "table" then
        dest[k] = _deepCopy( v )
      else
        dest[k] = v
      end
    end

    return dest
  end

  return _deepCopy( src )
end

function table.getSafeArrayValue( array, index )
  index = math.min( #array, math.max(index, 1))
  return array[ index ]
end

function table.append(org,...)
  local src = {...};
  local lens = #org;
  local beg = lens + 1;
  local tp = "";
  for key, var in pairs(src) do
    org[beg] = var;
    beg = beg + 1;
  end
end


local function shuffle(starNum, endNum, count)
  local shuffleNum = {};
  local math = math;

  -- 判断是否存在
  local function isExist(num)
    for key, var in ipairs(shuffleNum) do
      if var == num then
        return true
      end
    end
    return false
  end

  -- 生成随机数
  local function generateShuffle()
    if count > endNum then
      count = endNum;
    end

    local number = math.random(starNum, endNum)
    if isExist(number) then
      -- 如果存在,则继续随机
      generateShuffle()
    else
      -- 不存在,加入到随机数表中
      table.insert(shuffleNum, number)
      if #shuffleNum < count then
        generateShuffle()
      end
    end
  end

  randomSeed();
  generateShuffle()

  return shuffleNum;
end

-- 洗牌，混乱
function table.shuffle(org)
  local lens = #org;
  local rtab = shuffle(1,lens,lens);

  for k,v in ipairs(rtab) do
    org[k],org[v] = org[v],org[k];
  end
end

-- 洗牌，混乱 中取固定长度
function table.subShuffle(org,count)
  local lens = #org;
  local rtab = shuffle(1,lens,count);

  local ret = {};
  for k,v in ipairs(rtab) do
    ret[k] = org[v];
  end

  return ret;
end

-- 打印table
function table.printTable( tb , title, notSortKey )
  notSortKey = not (not notSortKey);

  local tabNum = 0
  local function stab( numTab )
    return string.rep("    ", numTab)
  end

  local str = {}

  local function _printTable( t )
    table.insert( str, "{" )
    tabNum = tabNum + 1

    local keys = {}
    for k, v in pairs( t ) do
      table.insert( keys, k )
    end

    if not notSortKey then table.sort(keys) end

    for _, k in pairs( keys ) do
      local v = t[ k ]
      local kk
      if type(k) == "string" then
        kk = "['" .. k .. "']"
      else
        kk = "[" .. tostring(k) .. "]"
      end
      if type(v) == "table" then
        table.insert( str, string.format('\n%s%s = ', stab(tabNum),kk))
        _printTable( v )
      else
        local vv = ""
        if type(v) == "string" then
          vv = string.format("\"%s\"", v)
        elseif type(v) == "number" or type(v) == "boolean" then
          vv = tostring(v)
        else
          vv = "[" .. type(v) .. "]"
        end

        if type(k) == "string" then
          table.insert( str, string.format("\n%s%-18s = %s,", stab(tabNum), kk, string.gsub(vv, "%%", "?") ) )
        else
          table.insert( str, string.format("\n%s%-4s = %s,", stab(tabNum), kk, string.gsub(vv, "%%", "?") ) )
          --print( string.format("%s%s", stab(tabNum), vv) )
        end

      end
    end
    tabNum = tabNum - 1

    if tabNum == 0 then
      table.insert( str, '}')
    else
      table.insert( str, '},')
    end
  end


  local titleInfo = title or "table"
  table.insert( str, string.format("\n----------begin[%s]----------[%s]\n", titleInfo, os.date("%H:%M:%S") )  )
  if not tb or type(tb) ~= "table" then
    print(tb)
  else
    _printTable(tb)
  end

  table.insert( str, string.format("\n----------end[%s]----------\n", titleInfo))
  print( table.concat(str, ""))
end
