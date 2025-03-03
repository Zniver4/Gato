<?php
function initJson ()
{
    $myfile = fopen("db.txt", "w") or die("Unable to open file!");

    $json = array("Player01" => "id01",
                 "Player02" => "id02", 
                 "CurrentTurn" => -1,
                 "Round" => -1,
                 "Score01" => -1,
                 "Score02" => -1,
                 "Board" => array(0,0,0,0,0,0,0,0,0)
                );

    echo json_encode($json);

    fwrite($myfile, json_encode($json));

    fclose($myfile);
}

function testJson ()
{
    
}

testJson();
?>