@echo off
cd /d ../../..
docker build -f src/Ray.JdTool.HttpApi.Host/Dockerfile -t zai7lou/jd_tool_api .
pause