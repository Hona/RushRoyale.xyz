@echo off
set api_dir=/src/RushRoyale.WebAPI
set ui_dir=/src/RushRoyale.WebUI

wt -d %CD%%api_dir% dotnet watch; split-pane -p "Windows PowerShell" -d %CD%%ui_dir% dotnet watch