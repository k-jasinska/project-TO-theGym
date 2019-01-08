<?php
session_start();
require_once('baza.php');

if(!(($_SESSION['zalogowany'])==2) || !isset($_SESSION['zalogowany'])){
    header("Location: index.php?error=0");
    exit();
}

if (isset($_POST['insert'])) {

    $answer=$_POST['check'];

    if(empty($answer) ) {
        header("Location: NoCarnet.php");
        $_SESSION['e_len']="Musisz wybrać karnet";
        exit;
    }
    $wynik=mysqli_query($link, "call t_add_Entrance(". $_SESSION['id_person'] ." ,". $answer.")") or die("nie udało się pobrać danych");
    if (mysqli_error($link)) {
        echo '<div class="info">'.mysqli_error($link).'</div>';
      }

    header("location: checkCarnet.php");
   

} 

?>