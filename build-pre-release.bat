@echo off
set FIND=C:\Windows\System32\find.exe
set SUFFIX=%1

FOR /F "tokens=4,* USEBACKQ" %%G IN (`standard-version --dry-run ^| %FIND% "tagging"`) DO SET PRE_RELEASE_VERSION=%%G

if "%1"=="" (
  set SUFFIX=alpha
)

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

REM Check that the version of standard-version supports dry-run
call standard-version --help | %FIND% "dry-run" 1> nul 2> nul
IF "%ERRORLEVEL%" NEQ "0" (
  GOTO :standardVersionError
)

:build
  ECHO Building Prelease Version: %PRE_RELEASE_VERSION%-%SUFFIX%
  call yarn
  REM call yarn run setVersionSuffix %SUFFIX%
  REM call yarn run build:pre-release
  set "PreReleaseVersion=%PRE_RELEASE_VERSION%" & set "VersionSuffix=%SUFFIX%" & yarn run build:pre-release
  GOTO end

:error
  ECHO Please run %~n0%~x0 again after installing yarn.

:standardVersionError
  ECHO Please install the latest version of standard-version (see https://github.com/conventional-changelog/standard-version)
  ECHO.
  GOTO end

:end
  

