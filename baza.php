<?php
try{
  $link = mysqli_connect("localhost", "root", "", "silownia") or die(mysqli_connect_error());
  //  $link = mysqli_connect("149.156.136.151", "kjasinska", "abc123", "kjasinska","3306") or die(mysqli_connect_error());
}
  catch(Exception $e){
    echo "Błęd serwera!";
  }
?>

