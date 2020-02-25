# Water Mango

- Water Mango is an application that allows the user to water five office plants. (Please do not water them after midnight. Trust me.)

- There are two parts to this application

  - `water-mango-api`

    - The `water-mango-api` is an ASP.NET Core 3.1 Web API.
    - The API by default runs on port `5000`
    - Some of the libaries used:

      - `SignalR` (Used for bi-directional communication with `water-mango-gui`)
      - `AutoMapper` (Used to map the endpoint DTOs to DataModels)

      - `water-mango.contracts`
        - This contracts library contains the DTOs and DataModels necessary for any application to make requests to the API.
        - You would share this library with any other .NET application/front-end that would need to communicate with the API. This ensures that they're using the same contracts and reduces duplicate code.

  - `water-mango-gui`
    - The `water-mango-gui` application is a React JS front end application. It was generated with `create-react-app`.
    - The GUI by default runs on port `3000`
    - Some of the packages used:
      - `@material-ui/core` (Used for our react component templates)
      - `@microsoft/signalr` (Used for bi-directional communication with `water-mango-api`)
      - `axios` (Used to make `http` request for our `water-mango-api` endpoints)

## Requirements

- .NET SDK that supports `ASP.NET Core 3.1`
- `Node.js`
- `Visual Studio 2019`
  - NOTE: The `build-run-api.bat` file relies on the path `C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\msbuild.exe` to exist.

## How to start the application

1. You can start by running the `build-run-api.bat`, provided you have the path above ^.
   - This will build and run the API in a console
2. Navigate to the `water-mango-gui` folder and in the console run `npm install` followed by `npm start`.
3. Profit

## Things to note:

- `ReactiveX` would be a welcomed implementation in the API.
- There is no persistence integrated with this project. The plant's data is created when the API is started and it's kept only in memory.
- The front end receives update events from the backend for individual plants, but we're still updating the entire list. (This could be inefficient with large amounts of data)
- The GUI is _very_ programmer art.. many appologize.
- Stopping the watering process is withing a `1 second` interval. So stopping the process isin't exactly in realtime. There is a slight delay depending how you time it. This works fine in most cases.
- The `ReactJS` front-end is built using React's new functional programming using hooks. It's a little odd but really helps to keep the code clean.
