<?php
class Gato {
    public $db = "game.db";

    public $p1, // username
           $p2, // username2
           $actual,
           $round,
           $score1,
           $score2,
           $board; // array

    public function init() {
        $this->board = array(0, 0, 0, 0, 0, 0, 0, 0, 0);
        $this->p1 = "id1";
        $this->p2 = "id2";
        $this->actual = 0;
        $this->round = 0;
        $this->score1 = 0;
        $this->score2 = 0;
        $this->saveDb();
    }

    public function saveDb() {
        $file = fopen($this->db, "w") or die("error");
        $strData = json_encode($this->toJson());
        fwrite($file, $strData);
        fclose($file);
    }

    public function toJson() {
        $data = array(
            "p1" => $this->p1,
            "p2" => $this->p2,
            "actual" => $this->actual,
            "round" => $this->round,
            "score1" => $this->score1,
            "score2" => $this->score2,
            "board" => $this->board
        );
        return $data;
    }

    public function loadDb() {
        $file = fopen($this->db, "r") or die("error");
        $strData = fread($file, filesize($this->db));
        $data = json_decode($strData);
        $this->p1 = $data->p1;
        $this->p2 = $data->p2;
        $this->actual = $data->actual;
        $this->round = $data->round;
        $this->score1 = $data->score1;
        $this->score2 = $data->score2;
        $this->board = $data->board;
    }

    public function toString() {
        echo "" .
            "p1:" . $this->p1 . "<br/>" .
            "p2:" . $this->p2 . "<br/>" .
            "actual:" . $this->actual . "<br/>" .
            "round:" . $this->round . "<br/>" .
            "score1:" . $this->score1 . "<br/>" .
            "score2:" . $this->score2 . "<br/>" .
            "board:";
        var_dump($this->board);
    }

    public function setPlayerId($player, $id) {
        if ($player == 1)
            $this->p1 = $id;
        elseif ($player == 2)
            $this->p2 = $id;
        else
            return false;
        return true;
    }

    public function getPlayer($id) {
        if ($id == $this->p1)
            return 1;
        elseif ($id == $this->p2)
            return 2;
        else
            return 0;
    }

    public function getScore($player) {
        switch ($player) {
            case 1:
                return $this->score1;
                break;
            case 2:
                return $this->score2;
                break;
            default:
                return "-1";
                break;
        }
    }

    public function getStatus($id) {
        $player = $this->getPlayer($id);
        $data = array(
            "actual" => $this->actual,
            "round" => $this->round,
            "score" . $player => $this->getScore($player),
            "board" => $this->board,
        );
        return json_encode($data);
    }

    public function turn($id, $pos) {
        $player = $this->getPlayer($id);
        if ($player == 0) {
            return "error: invalid player";
        }
        if ($player != ($this->actual + 1)) {
            return "error: not your turn";
        }
        if ($this->board[$pos] == 0) {
            $this->board[$pos] = $player;
            $this->actual = ($this->actual + 1) % 2; // Cambiar turno
            return "OK";
        } else {
            return "error: position taken";
        }
    }

    public function isWin() {
        // validaciones
        if ($this->board[0] == $this->actual && $this->board[1] == $this->actual && $this->board[2] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[3] == $this->actual && $this->board[4] == $this->actual && $this->board[5] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[6] == $this->actual && $this->board[7] == $this->actual && $this->board[8] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[0] == $this->actual && $this->board[4] == $this->actual && $this->board[8] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[2] == $this->actual && $this->board[4] == $this->actual && $this->board[6] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[0] == $this->actual && $this->board[3] == $this->actual && $this->board[6] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[1] == $this->actual && $this->board[4] == $this->actual && $this->board[7] == $this->actual) {
            $this->gameOver();
            return;
        }

        if ($this->board[2] == $this->actual && $this->board[5] == $this->actual && $this->board[8] == $this->actual) {
            $this->gameOver();
            return;
        }

        // Cambia de jugador
        $this->changeSide();

        // verifica si el juego es empate
        if ($this->turns > 9) {
            $this->gameOverText("¡Empate!");
        }
    }

    private function gameOver() {
        $this->gameOverText = $this->playerSide . " WIN!";
        echo $this->gameOverText . PHP_EOL;
    }

    private function changeSide() {
        $this->playerSide = ($this->playerSide == 'X') ? 'O' : 'X';
    }
}

$gato = new Gato();

if (!empty($_GET["action"])) {
    $action = $_GET["action"];
} else {
    $action = 0;
}

switch ($action) {
    case 0: // empty
        echo "empty";
        break;
    case 1:
        $gato->init();
        $gato->loadDb();
        $gato->toString();
        break;
    case 2:
        if (!empty($_GET["id"])) {
            $id = $_GET["id"];
        } else {
            $id = 0;
        }
        $gato->loadDb();
        echo $gato->getStatus($id);
        break;
    case 3: // turn
        if (!empty($_GET["id"])) {
            $id = $_GET["id"];
        } else {
            $id = 0;
        }
        if (!empty($_GET["pos"])) {
            $pos = $_GET["pos"] - 1;
        } else {
            $pos = -1;
        }
        $gato->loadDb();
        $result = $gato->turn($id, $pos);
        $gato->saveDb();
        echo json_encode(array("result" => $result, "actual" => $gato->actual));
        break;
    default:
        echo "No Control";
        break;
}
?>