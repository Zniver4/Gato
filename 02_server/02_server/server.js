console.log("Saquen las drogaaAAAAAAAAS")
const express = require ("express");
const app = express();
const port = 8080;
var count=0;

app.get("/", (req, res)=> {
    res.send("Hello word")
});

app.get("/count", (req, res)=> {
    count++;
    res.send("Contador: "+count)
});

app.get("/action/init", (req, res)=> {
    res.send("hwreghoiwe")
});

app.get("/status/:player", (req, res) =>{
    res.send("Return status of player" + req.params["player"]
        + "<br>contador: " +count)
})

app.get("/turn/:player/:pos", (req, res)=> {
    let player = "";
    switch (req.params["player"]) {
        case "1":
            player = "player01";
            break;
        case "2":
            player = "player02";
            break;

        default:
            player = "error";
            break;
    };
    res.send("El player "+player+" ha tirado en: " + req.paramas["pos"]);
});

app.listen(port, ()=> {
    console.log("Server init: ${port}")
    console.log("Server")
});

