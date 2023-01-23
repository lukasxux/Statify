rd /S /Q .vs 2> nul
rd /S /Q StatifyProject.Application/.vs 2> nul
rd /S /Q StatifyProject.Application/bin 2> nul
rd /S /Q StatifyProject.Application/obj 2> nul
rd /S /Q StatifyProject.Webapi/.vs 2> nul
rd /S /Q StatifyProject.Webapi/bin 2> nul
rd /S /Q StatifyProject.Webapi/obj 2> nul

:start
dotnet watch run -c Debug --project StatifyProject.Webapi
goto start