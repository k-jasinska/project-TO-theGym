
<?php
require_once('baza.php');
     $email=$_POST['email'];
    if(empty($email))
    {
       echo "E-mail jest wymagany";
    }
    $wynik=mysqli_query($link, "Select * from Person where email='$email' ;") or die("nie udało się pobrać danych");
    $rows=mysqli_fetch_array($wynik);

    if($email==$rows['email']){
         echo "E-mail jest juz zajety";
        // http_response_code(500);
    }
?>
