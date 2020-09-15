@echo off
setlocal EnableDelayedExpansion
set FIND=C:\Windows\System32\find.exe
set SUFFIX=%1
WHERE npm >nul 2>&1
set NPM_AVAILABLE=%ERRORLEVEL%

REM Check that the version of standard-version supports dry-run
call npx standard-version --help | %FIND% "dry-run" 1> nul 2> nul
IF "%ERRORLEVEL%" NEQ "0" (
  GOTO :standardVersionError
)

FOR /F "tokens=4,* USEBACKQ" %%G IN (`npx standard-version --dry-run ^| %FIND% "tagging"`) DO SET PRE_RELEASE_VERSION=%%G
SET PRE_RELEASE_VERSION=%PRE_RELEASE_VERSION:v=%

if "%1"=="" (
  set SUFFIX=alpha
)


:build
ECHO Building Prelease Version: %PRE_RELEASE_VERSION%-%SUFFIX%
call npm install
npm run build --version-prefix=%PRE_RELEASE_VERSION% --version-suffix=%SUFFIX%
GOTO end

:error
ECHO Please run %~n0%~x0 again after installing npm.

:standardVersionError
ECHO Please install the latest version of standard-version (see https://github.com/conventional-changelog/standard-version)
ECHO.
GOTO end

:end


