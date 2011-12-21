@echo off

mkdir ../Output
mkdir Output

REM ====================== Gendarme ===============================
IF EXIST ../Output/nPgTools.dll (
	Gendarme\gendarme.exe --html Output/Gendarme.html ../Output/nPgTools.dll
	echo.
	echo.
	echo === Gendarme builded sucessfully... ===
	echo.
) ELSE (
	echo.
	echo.
	echo nPgTools.dll not found in ../Output, please build the solution before...
	echo.
)

REM ====================== Style Cop ==============================
StyleCop\StyleCopCmd.exe -pf ../Projects/NpgTools/nPgTools_vs2005.csproj -of Output/StyleCop.xml -tf StyleCop\StyleCopReport.xsl

REM ====================== Doxygen ==============================
IF EXIST "C:\Program Files\doxygen\bin\doxygen.exe" (
	"C:\Program Files\doxygen\bin\doxygen.exe" Doxyfile.doxygen
) ELSE (
	echo Doxygen will not be builded, "C:\Program Files\doxygen\bin\doxygen.exe" hasn't been found
)

pause