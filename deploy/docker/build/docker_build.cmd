@echo off
cd /d ../../..
::docker build -f src/Ray.JdTool.HttpApi.Host/Dockerfile -t zai7lou/jd_tool_api .
docker build -f src/Ray.JdTool.Blazor/Dockerfile -t zai7lou/jd_tool_web .
pause