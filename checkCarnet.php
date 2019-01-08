<?php
require_once('baza.php');
session_start();

if(!(($_SESSION['zalogowany'])==2) || !isset($_SESSION['zalogowany'])){
    header("Location: index.php?error=0");
    exit;
}

    //   $id=$_SESSION['id_person'];
      $q=mysqli_query($link, "call pShowCarnet(". $_SESSION['id_person']. ")");
      $tabl=mysqli_fetch_assoc($q);
      
      $_SESSION['name_of_type']=htmlspecialchars($tabl['name_of_type']);
      $_SESSION['duration']=htmlspecialchars($tabl['duration']);
      $_SESSION['begin_date']=htmlspecialchars($tabl['begin_date']);
      $_SESSION['end_date']=htmlspecialchars($tabl['end_date']);
      $_SESSION['price']=htmlspecialchars($tabl['price']);
  

if($_SESSION['name_of_type']){
    header('Location: yourCarnet.php');
}

else{
    header('Location: NoCarnet.php');
}
?>