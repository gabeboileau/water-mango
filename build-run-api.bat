cd water-mango-api
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\msbuild.exe" water-mango.sln /t:Clean,Build /p:Configuration=Release
cd water-mango-api\bin\Release\netcoreapp3.1
start water-mango.api.exe

cd ../../../../../water-mango-gui
npm install
echo before
start "npm start"
echo after
pause