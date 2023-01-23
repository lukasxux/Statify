@echo off
REM Löscht alle temporären Visual Studio Dateien
rd /S /Q .vs 2> nul
rd /S /Q .vscode 2> nul

FOR %%d IN (StatifyProject.Application StatifyProject.Webapi) DO (
    rd /S /Q "%%d/bin" 2> nul 
    rd /S /Q "%%d/obj" 2> nul
)

FOR %%d IN (StatifyProject.Client) DO (
  rd /S /Q "%%d/node_modules" 2> nul
)

