<?php
session_start();
require_once('baza.php');

// filter_var($s, FILTER_SANITIZE_STRING)
// htmlspecialchars($s, ENT_QUOTES)
function filtruj($zmienna,$link)
{
    if (get_magic_quotes_gpc()) {
        $zmienna = stripslashes($zmienna);
    }
$zmienna=htmlspecialchars(trim($zmienna));
    return mysqli_real_escape_string($link,$zmienna);
}

if (isset($_POST['rejestruj'])) {
    

    $name = filtruj($_POST['name'],$link);
    $surname = filtruj($_POST['surname'],$link);
    $email = filtruj($_POST['email'],$link);
    $password = filtruj($_POST['password'],$link);
    $phone = filtruj($_POST['phone'],$link);
    $street = filtruj($_POST['street'],$link);
    $code = filtruj($_POST['code'],$link);
    $city = filtruj($_POST['city'],$link);

    // zapameitaj dane
    $_SESSION['fr_name']=$name;
    $_SESSION['fr_surname']=$surname;
    $_SESSION['fr_email']=$email;
    $_SESSION['fr_password']=$password;
    $_SESSION['fr_phone']=$phone;
    $_SESSION['fr_street']=$street;
    $_SESSION['fr_code']=$code;
    $_SESSION['fr_city']=$city;
    if(isset($_POST['check1'])) $_SESSION['fr_check1']=true;
    if(isset($_POST['check2'])) $_SESSION['fr_check2']=true;


if(empty($name) || empty($surname) || empty($email)|| empty($password)|| empty($phone)|| empty($street)|| empty($code) || empty($city) ) {
    header("Location: rejestracja.php");
    $_SESSION['e_len']="Musisz wypełnić wszystkie pola";
    exit;
}

if(!isset($_POST['check2'])){
    header("Location: rejestracja.php");
    $_SESSION['e_len']="Musisz zaakceptoważ regulamin";
    exit;
}
if(!isset($_POST['check1'])){
    header("Location: rejestracja.php");
    $_SESSION['e_len']="Musisz wyrazić zgodę na przetwarzanie danych";
    exit;
}
if((strlen($name)<3) || (strlen($name)>20)){
    header("Location: rejestracja.php");
    $_SESSION['e_name']="Imię musi mieć od 3 do 20 znaków";
    exit;
}
if((strlen($surname)<3) || (strlen($surname)>80)){
    header("Location: rejestracja.php");
    $_SESSION['e_surname']="Nazwisko musi mieć od 3 do 80 znaków";
    exit;
}
if(strlen($email)>25){
    header("Location: rejestracja.php");
    $_SESSION['e_email']="Email nie może mieć więcej niż 25 znaków";
    exit;
}
if((strlen($password)<6) || (strlen($password)>20)){
    header("Location: rejestracja.php");
    $_SESSION['e_email']="Hasło musi posiadać od 6 do 20 znaków";
    exit;
}

$pass_hash=password_hash($password, PASSWORD_DEFAULT);


if((strlen($phone)<9) || (strlen($phone)>9)){
    header("Location: rejestracja.php");
    $_SESSION['e_phone']="Nr telefonu musi mieć 9 znaków";
    exit;
}

   
if (mysqli_num_rows(mysqli_query($link,"SELECT email FROM Person WHERE email = '" . $email . "';")) == 0) 
{
    if(isset($_SESSION['fr_name'])) unset($_SESSION['fr_name']);
    if(isset($_SESSION['fr_email'])) unset($_SESSION['fr_email']);
    if(isset($_SESSION['fr_password'])) unset($_SESSION['fr_password']);
    if(isset($_SESSION['fr_phone'])) unset($_SESSION['fr_phone']);
    if(isset($_SESSION['fr_street'])) unset($_SESSION['fr_street']);
    if(isset($_SESSION['fr_code'])) unset($_SESSION['fr_code']);
    if(isset($_SESSION['fr_city'])) unset($_SESSION['fr_city']);
    if(isset($_SESSION['fr_check1'])) unset($_SESSION['fr_check1']);
    if(isset($_SESSION['fr_check2'])) unset($_SESSION['fr_check2']);

    $sql1="INSERT INTO adres (street, code, city) VALUES (?, ?, ?);";
    $stmt1=mysqli_stmt_init($link);
    if(!mysqli_stmt_prepare($stmt1, $sql1)){
        echo "sql error1";
    } else {
        mysqli_stmt_bind_param($stmt1, "sss", $street, $code, $city);
        mysqli_stmt_execute($stmt1);
        $insertID=mysqli_insert_id($link);
        $sql2="insert into Person (name, surname, email, password, phone, id_adres, id_P_Type) values (?, ?, ?, ?, ?, $insertID,2);";
        $stmt2=mysqli_stmt_init($link);
        if(!mysqli_stmt_prepare($stmt2, $sql2)){
            echo "sql error2";
        } else {
            mysqli_stmt_bind_param($stmt2, "sssss", $name, $surname, $email, $pass_hash, $phone);
            mysqli_stmt_execute($stmt2);
        }
    }

    // mysqli_query($link, "INSERT INTO adres (street, code, city) VALUES ('$street', '$code', '$city');");
    // $insertID=mysqli_insert_id($link);
    // mysqli_query($link, "insert into Person (name, surname, email, password, phone, id_adres, id_P_Type) values ('$name','$surname','$email','$pass_hash','$phone',$insertID,2);");
    // echo "Konto zostało utworzone!";
     header('location: index.php?error=2');
} 
    else 
    {
        header("Location: rejestracja.php");
        $_SESSION['e_email']="Email jest zajęty";
    exit;
    }
} 
else 
{
    header("Location: rejestracja.php?error=0");
    exit;
}
?>