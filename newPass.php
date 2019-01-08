
<?php
require_once('baza.php');
session_start();

if(isset($_POST['log_in']))
{
    // $pass = filtruj($_POST['pass'],$link);
    // $pass1 = filtruj($_POST['newPass1'],$link);
    // $pass1 = filtruj($_POST['newPass2'],$link);
    $pass = $_POST['pass'];
    $pass1 = $_POST['newPass1'];
    $pass2 = $_POST['newPass2'];

    if( empty($pass) || empty($pass1) || empty($pass2)) {
      header("Location: changePass.php");
      $_SESSION['e_len']="Musisz wypełnić wszystkie pola";
      exit;
    }
    if((strlen($pass)<6) || (strlen($pass)>20) || (strlen($pass1)<6) || (strlen($pass1)>20) || (strlen($pass2)<6) || (strlen($pass2)>20)){
        header("Location: changePass.php");
        $_SESSION['e_len']="Hasło musi zawierać od 6 do 20 znaków";
        exit;
    }
    if($pass1!=$pass2){
        header("Location: changePass.php");
        $_SESSION['e_len']="Powtórzone hasło nie jest zgodne";
        exit;
  
    }

    $pass_hash=password_hash($pass1, PASSWORD_DEFAULT);


    $test=$_SESSION['email'];
    $wynik=mysqli_query($link, "Select password from Person where email='$test';") or die("nie udało się pobrać danych");
    $rows=mysqli_fetch_array($wynik);


    if((password_verify($pass, $rows['password']))){
        mysqli_query($link, "Update Person Set password='$pass_hash' where email='$test';") or die("nie udało się zmienic hasła");
        header("Location: changePass.php");
        $_SESSION['e_change']="Pomyślnie zmieniono hasło!";
        exit;
    }
    else{
        header("Location: changePass.php");
        $_SESSION['e_len']="Podano błędne hasło";
        exit("");
    }
}
else 
{
    header("Location: index.php?error=0");
    exit;
}
?>