const WebSocket = require("ws");

const clients = [];
const player1 = null;
const player2 = null;
const wss = new WebSocket.Server({ port: 8080 }, ()=>{
    console.log("Server Started");
});

wss.on("connection", function connection(ws) {
    ws.on("open", (data) => {
        console.log("New connection")
    });
});

ws.on("Message", (data)=>{
    console.log("Data received: %$", data);
    
    ws.send("The server response: "+ data);
});

wss.on("listening", ()=>{
    console.log (" listening on 8080");
});