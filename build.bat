@echo off
setlocal EnableDelayedExpansion
WHERE yarn >nul 2>&1
set YARN_AVAILABLE=%ERRORLEVEL%

WHERE npm >nul 2>&1
set NPM_AVAILABLE=%ERRORLEVEL%

IF "%YARN_AVAILABLE%" NEQ "0" (
  ECHO The yarn command is not available.
  IF "%NPM_AVAILABLE%" EQU "0" (
    SET /P "Input=Would you like to use npm to install yarn? (y/n):"
    IF "!Input!"=="y" (
      call npm install yarn -g
      goto build
    ) else (
      GOTO error
    )
  ) else (
    GOTO :error
  )
)

:build
  call yarn
  call yarn run build
  GOTO end

:error
  ECHO Please run %~n0%~x0 again after installing yarn.

:end
  

