--主入口函数。从这里开始lua逻辑
function Main()					
	print("===Main==");
end

--场景切换通知
function OnLevelWasLoaded(level)
	print("===OnLevelWasLoaded==");
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end