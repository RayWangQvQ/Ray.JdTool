@echo off
cd /d ../../..
docker build -f src/Ray.JdTool.Gateway/Dockerfile -t zai7lou/jd_tool_gateway .
pause