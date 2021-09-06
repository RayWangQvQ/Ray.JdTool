@echo off
cd /d ../../..
docker build -f src/Ray.JdTool.Blazor/Dockerfile -t zai7lou/jd_tool_web .
pause