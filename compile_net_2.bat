@echo off
REM
REM -- RunUO compile script for .NET 2.0 --
REM

if exist RunUO.exe (
	echo Deleting binary...
	del RunUO.exe 1>NUL 2>NUL
	
	if exist RunUO.exe (
		echo Failed!
		echo.
		echo Is RunUO.exe in use?
		echo.
		goto end
	) else (
		echo Success.
		echo.
	)
)

echo Recompiling...
C:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe /unsafe /out:RunUO.exe /recurse:Server\*.cs /win32icon:Server\runuo.ico /optimize /main:Server.Core

:end
pause
