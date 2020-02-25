const signalR = require("@microsoft/signalr");

// connects to the signalr events and passes in a callback for the update event specifically
export function connect(updateEvent) {
  let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/plantHub")
    .withAutomaticReconnect()
    .build();

  connection.on("updatePlant", data => {
    updateEvent(data.id);
  });

  connection.start().catch(err => console.log(err));
}
