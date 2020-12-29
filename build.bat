@echo off
setlocal EnableDelayedExpansion
set FIND=C:\Windows\System32\find.exe
set SUFFIX=%1
WHERE npm >nul 2>&1
set NPM_AVAILABLE=%ERRORLEVEL%

:build
call npm ci

IF "%SUFFIX%" == "" (
  GOTO :build
) ELSE (
  GOTO :buildPreRelease
)

:build
  call npm run build
GOTO end

:buildPreRelease
  REM Check that the version of standard-version supports dry-run
  call standard-version --help | %FIND% "dry-run" 1> nul 2> nul
  IF "%ERRORLEVEL%" NEQ "0" (
    GOTO :standardVersionError
  )

  FOR /F "tokens=4,* USEBACKQ" %%G IN (`standard-version --dry-run ^| %FIND% "tagging"`) DO SET PRE_RELEASE_VERSION=%%G
  SET PRE_RELEASE_VERSION=%PRE_RELEASE_VERSION:v=%

  IF "!PRE_RELEASE_VERSION!" == "" (
    ECHO Using Version 0.0.1
    SET PRE_RELEASE_VERSION="0.0.1"
  )

  ECHO Building Prelease Version: %PRE_RELEASE_VERSION%-%SUFFIX%
  call npm run build --version-prefix=%PRE_RELEASE_VERSION% --version-suffix=%SUFFIX%
GOTO end

:error
ECHO Please run %~n0%~x0 again after installing npm.

:standardVersionError
ECHO Please install the latest version of standard-version (see https://github.com/conventional-changelog/standard-version)
ECHO.
GOTO end

:end
