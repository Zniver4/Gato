const express = require('express');
const app = express();
const fs = require('fs');

const port = 8080;

class Gato {
    constructor() {
        this.db = "game.db";
        this.gameOverText = "";
        this.p1 = null;
        this.p2 = null;
        this.actual = null;
        this.round = null;
        this.score1 = null;
        this.score2 = null;
        this.board = [];
    }

    init() {
        this.board = [0, 0, 0, 0, 0, 0, 0, 0, 0];
        this.p1 = "id1";
        this.p2 = "id2";
        this.actual = Math.floor(Math.random() * 2) + 1;
        this.round = 1;
        this.score1 = 0;
        this.score2 = 0;
        this.saveDb();
    }

    saveDb() {
        const data = JSON.stringify(this.toJson());
        fs.writeFileSync(this.db, data);
    }

    toJson() {
        return {
            p1: this.p1,
            p2: this.p2,
            actual: this.actual,
            round: this.round,
            score1: this.score1,
            score2: this.score2,
            board: this.board
        };
    }

    loadDb() {
        const data = fs.readFileSync(this.db);
        const parsedData = JSON.parse(data);
        this.p1 = parsedData.p1;
        this.p2 = parsedData.p2;
        this.actual = parsedData.actual;
        this.round = parsedData.round;
        this.score1 = parsedData.score1;
        this.score2 = parsedData.score2;
        this.board = parsedData.board;
    }

    toString() {
        return `
            p1: ${this.p1}<br/>
            p2: ${this.p2}<br/>
            actual: ${this.actual}<br/>
            round: ${this.round}<br/>
            score1: ${this.score1}<br/>
            score2: ${this.score2}<br/>
            board: ${JSON.stringify(this.board)}
        `;
    }

    setPlayerId(player, id) {
        if (player == 1) this.p1 = id;
        else if (player == 2) this.p2 = id;
        else return false;
        return true;
    }

    getPlayer(id) {
        if (id == this.p1) return 1;
        else if (id == this.p2) return 2;
        else return 0;
    }

    getScore(player) {
        switch (player) {
            case 1:
                return this.score1;
            case 2:
                return this.score2;
            default:
                return "-1";
        }
    }

    getStatus(id) {
        const player = this.getPlayer(id);
        return JSON.stringify({
            actual: this.actual,
            round: this.round,
            [`score${player}`]: this.getScore(player),
            board: this.board
        });
    }

    turn(id, pos) {
        if (this.board[pos] == 0) {
            const player = this.getPlayer(id);
            if (player != this.actual) {
                return "No es tu turno";
            }

            this.board[pos] = player;
            this.isWin();
            return "OK";
        } else {
            return "error";
        }
    }

    isWin() {
        const winPatterns = [
            [0, 1, 2], [3, 4, 5], [6, 7, 8],
            [0, 3, 6], [1, 4, 7], [2, 5, 8],
            [0, 4, 8], [2, 4, 6]
        ];

        for (const pattern of winPatterns) {
            if (this.board[pattern[0]] == this.actual && this.board[pattern[1]] == this.actual && this.board[pattern[2]] == this.actual) {
                this.gameOver();
                return;
            }
        }

        if (!this.board.includes(0)) {
            this.gameOverText = "¡Empate!";
            console.log(this.gameOverText);
            process.exit();
        } else {
            this.changeSides();
        }
    }

    changeSides() {
        this.actual = (this.actual == 1) ? 2 : 1;
    }

    gameOver() {
        this.gameOverText = `¡El jugador ${this.actual} gano!`;
        console.log(this.gameOverText);
    }
}

const gato = new Gato();

app.get("/", (req, res) => {
    res.send("Hello world");
});

app.get("/action/db", (req,res) => {
    const db = fs.readFileSync(gato.db, "utf8");
    res.send(db);
}); 

app.get("/action/init", (req, res) => {
    gato.init();
    gato.loadDb();
    res.send(gato.toString());
});

app.get("/action/data", (req, res) => {
    try {
        const data = fs.readFileSync(gato.db, "utf8");
        res.setHeader("Content-Type", "application/json");
        res.send(data);
    } catch (error) {
        res.status(500).send({ error: "Error al leer la base de datos." });
    }
});


app.get("/status/:id", (req, res) => {
    const id = req.params.id;
    gato.loadDb();
    res.send(gato.getStatus(id));
});

app.get("/turn/:id/:pos", (req, res) => {
    const id = req.params.id;
    const pos = req.params.pos - 1;
    gato.loadDb();
    res.send(gato.turn(id, pos));
    gato.saveDb();
});

app.listen(port, () => {
    console.log(`Server running on port ${port}`);
    console.log("Server init: ${port}")
    console.log("Server")
});

