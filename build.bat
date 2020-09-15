@echo off
setlocal EnableDelayedExpansion

WHERE npm >nul 2>&1
set NPM_AVAILABLE=%ERRORLEVEL%

IF "%NPM_AVAILABLE%" NEQ "0" (
  GOTO :error
)

:build
call npm ci
call npm run build
IF "%ERRORLEVEL%" NEQ "0" (
  ECHO The npm build failed with return code %ERRORLEVEL%.
  exit /B %ERRORLEVEL%
)

GOTO end

:error
ECHO Please run %~n0%~x0 again after installing npm.

:end


